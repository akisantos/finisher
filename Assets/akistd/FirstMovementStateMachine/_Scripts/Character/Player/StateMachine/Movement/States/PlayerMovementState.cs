using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;
        protected PlayerGroundedData movementData;
        protected PlayerAirboneData airboneData;

        private float MoveDampVel;
        public PlayerMovementState(PlayerMovementStateMachine movementStateMachine)
        {
            stateMachine = movementStateMachine;
            movementData = stateMachine.Player.Data.GroundedData;
            airboneData = stateMachine.Player.Data.AirboneData;

            InitializeData();
        }
        
        private void InitializeData()
        {
            stateMachine.ResuableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {

        }

        public virtual void Enter()
        {
            //Debug.Log("state: " + GetType().Name);
            AddInputActionCallback();
            

        }

        public virtual void Exit()
        {
            RemoveInputActionCallback();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            Move();
            
        }

        public virtual void Update()
        {
            Rotate(stateMachine.ResuableData.CurrentTargetRotation);
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
            {
                IsOnGround(collider);
            }
        }

        

        #region Main Methods

        private void ReadMovementInput()
        {
            stateMachine.ResuableData.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            
            if (stateMachine.ResuableData.MovementInput == Vector2.zero || stateMachine.ResuableData.MovementSpeedModifier == 0f)
            {

                return;
            }

  
            Vector3 moveDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(moveDirection);

            Vector3 targetRotationDir = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerVelocity = GetPlayerHorizontalVelocity();

            float dampledMovementSpeed = Mathf.SmoothDamp(stateMachine.Player.Rigidbody.velocity.magnitude, movementSpeed,ref movementSpeed, 0.02f);

            stateMachine.Player.Rigidbody.AddForce(targetRotationDir * dampledMovementSpeed - currentPlayerVelocity, ForceMode.VelocityChange);

        }
        
        private float Rotate(Vector3 direction)
        {
            //float dirAngle = UpdateTargetRotation(direction);
            float dirAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            
            return dirAngle;
        }



        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.ResuableData.CurrentTargetRotation.y = targetAngle;
            stateMachine.ResuableData.DampedTargetRotationPassedTime.y = 0f;
        }

        private float AddCameraRotationToAngle(float dirAngle)
        {
            dirAngle += stateMachine.Player.MainCameraTransform.eulerAngles.y;

            if (dirAngle > 360f)
            {
                dirAngle -= 360f;
            }

            return dirAngle;
        }

        private float GetDirectionAngle(Vector3 direction)
        {
            float dirAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (dirAngle < 0f)
            {
                dirAngle += 360f;
            }

            return dirAngle;
        }

        #endregion

        #region Reusable Methods
        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }

        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.Player.Rigidbody.AddForce(-playerHorizontalVelocity * stateMachine.ResuableData.MovementDecelerationForce, ForceMode.Acceleration);


        }

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);
            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }



        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.ResuableData.MovementInput.x, 0f, stateMachine.ResuableData.MovementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return movementData.BaseSpeed * stateMachine.ResuableData.MovementSpeedModifier * stateMachine.ResuableData.MovementOnSlopeSpeedModifier;
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, stateMachine.Player.Rigidbody.velocity.y, 0f);

        }

        protected void RotateTowardsTargetRotation()
        {
            float currentAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;

            if (currentAngle == stateMachine.ResuableData.CurrentTargetRotation.y)
            {
                return;
            }

            float smoothYAngle = Mathf.SmoothDampAngle(currentAngle, stateMachine.ResuableData.CurrentTargetRotation.y, ref stateMachine.ResuableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ResuableData.TimeToReachTargetRotation.y - stateMachine.ResuableData.DampedTargetRotationPassedTime.y);
            stateMachine.ResuableData.DampedTargetRotationPassedTime.y += Time.deltaTime * 0.01f;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothYAngle, 0f);

            if (stateMachine.ResuableData.MovementInput.y >= 0)
            {
                stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
            }



        }


        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float dirAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                dirAngle = AddCameraRotationToAngle(dirAngle);

            }

            if (dirAngle != stateMachine.ResuableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(dirAngle);
            }

            return dirAngle;
        }
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
            

        }

        protected virtual void AddInputActionCallback()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalktoggleStarted;
        }

        protected virtual void RemoveInputActionCallback()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalktoggleStarted;
        }


        protected virtual void OnWalktoggleStarted(InputAction.CallbackContext context)
        {

            stateMachine.ResuableData.ShouldWalk = !stateMachine.ResuableData.ShouldWalk;
            Debug.Log("ShouldWalk? " + stateMachine.ResuableData.ShouldWalk + " from " + GetType().Name);

        }

        protected virtual void IsOnGround(Collider collider)
        {

        }

        protected bool IsMovingUp(float minimumVel = 0.1f)
        {
            return GetPlayerHorizontalVelocity().y < minimumVel;
        }

        protected bool IsMovingDown(float minimumVel = 0.1f)
        {
            return GetPlayerHorizontalVelocity().y < -minimumVel;
        }




        #endregion
    }
}
