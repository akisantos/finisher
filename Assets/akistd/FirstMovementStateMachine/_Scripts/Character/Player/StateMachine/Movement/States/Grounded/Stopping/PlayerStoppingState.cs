using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ResuableData.MovementSpeedModifier = 0f;
            StartAnimation(stateMachine.Player.AnimationData.StoppingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.StoppingParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }



        public override void OnAnimationTransitionEvent()
        {
            
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        #endregion

        #region Resuable Methods

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
        }



        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            base.OnMovementCanceled(context);
        }

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }
        #endregion
    }
}
