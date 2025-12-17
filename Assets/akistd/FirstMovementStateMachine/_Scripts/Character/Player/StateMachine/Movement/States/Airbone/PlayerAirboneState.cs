using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerAirboneState : PlayerMovementState
    {
        public PlayerAirboneState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }



        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Jump.started -= OnJumpStarted;

        }

        #region Resuable Methods
        protected override void IsOnGround(Collider collider)
        {
            stateMachine.ChangeState(stateMachine.IdlingState);
        }

        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.PlayerJumpState);
        }
        #endregion
    }
}
