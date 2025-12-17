using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData slopeData;
        public PlayerGroundedState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {

            slopeData = stateMachine.Player.CapsuleColliderUtils.SlopeData;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Float();
        }

        #endregion


        #region Main Functions
        private void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.CapsuleColliderUtils.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);


                if (slopeSpeedModifier == 0f)
                {
                    return;
                }

                float distanceToFloatingPoint = stateMachine.Player.CapsuleColliderUtils.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;
                if (distanceToFloatingPoint == 0f)
                {
                    return;
                }

                float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

                stateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedModifierOnAngle(float groundAngle)
        {
            float slopeSpeedModifier = movementData.SlopeSpeedAngles.Evaluate(groundAngle);

            stateMachine.ResuableData.MovementOnSlopeSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }

        #endregion

        #region Resuable Methods

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
            stateMachine.Player.Input.PlayerActions.Sliding.started += OnSlidingStarted;

        }

        

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;
            stateMachine.Player.Input.PlayerActions.Sliding.started -= OnSlidingStarted;
        }

        protected virtual void OnMove()
        {

            if (stateMachine.ResuableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkkingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }



        #endregion

        #region Input Methods


        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.PlayerJumpState);
            stateMachine.Player.Input.PlayerActions.Jump.Disable();
        }

        protected virtual void OnSlidingStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SlidingState);
        }

        protected virtual void OnSlidingCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        #endregion
    }
}
