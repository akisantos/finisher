using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerSprintingState : PlayerMovingState
    {
        public PlayerSprintingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.PlayerHardStoppingState);
        }
    }
}
