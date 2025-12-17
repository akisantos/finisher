using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
        public PlayerMediumStoppingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ResuableData.MovementDecelerationForce = movementData.StopData.MediumDecelerationForce;
            StartAnimation(stateMachine.Player.AnimationData.MediumStopParameterHash);
        }
        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.MediumStopParameterHash);
        }
        #endregion
    }
}
