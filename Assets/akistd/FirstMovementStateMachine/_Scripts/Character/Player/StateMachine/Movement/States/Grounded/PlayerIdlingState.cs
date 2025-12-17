using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {

        }


        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.ResuableData.MovementSpeedModifier = 0f;
            stateMachine.ResuableData.CurrentJumpForce = airboneData.JumpData.RootForce;
            ResetVelocity();
            StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
            
            
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.ResuableData.MovementInput == Vector2.zero)
            {
                ResetVelocity();
                return;
            }

            OnMove();
        }



        #endregion
    }
}
