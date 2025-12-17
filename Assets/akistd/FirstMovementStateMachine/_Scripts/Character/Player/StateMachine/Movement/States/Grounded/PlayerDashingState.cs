using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashingData dashingData;
        public PlayerDashingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            dashingData = movementData.DashData;
        }

        public override void Enter()
        {
            base.Enter();
            stateMachine.ResuableData.MovementSpeedModifier = dashingData.SpeedModifier;
            AddForceOnTransitionFromStationaryState();
        }

        #region Main Methods
        private void AddForceOnTransitionFromStationaryState()
        {
            if  (stateMachine.ResuableData.MovementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterRotationDir = stateMachine.Player.transform.forward;
            characterRotationDir.y = 0;
            stateMachine.Player.Rigidbody.velocity = characterRotationDir * GetMovementSpeed();
        }

        #endregion
    }
}
