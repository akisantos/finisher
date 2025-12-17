using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ResuableData.MovementDecelerationForce = movementData.StopData.HardDecelerationForce;
            StartAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.HardStopParameterHash);
        }
        #endregion

        #region Resuable Methods
        protected override void OnMove()
        {
            if (stateMachine.ResuableData.ShouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.RunningState);
        }



        #endregion
    }
}
