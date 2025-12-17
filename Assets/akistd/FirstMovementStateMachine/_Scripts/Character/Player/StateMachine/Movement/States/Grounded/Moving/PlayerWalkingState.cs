using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
  
            stateMachine.ResuableData.MovementSpeedModifier = movementData.WalkData.SpeedModifier;
            stateMachine.ResuableData.CurrentJumpForce = airboneData.JumpData.WeakForce;
            StartAnimation(stateMachine.Player.AnimationData.WalkParameterHash);

        }

        public override void Update()
        {
            
            playSound();
            
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.Player.AnimationData.WalkParameterHash);
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.PlayerLightStoppingState);
        }
        protected override void OnWalktoggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalktoggleStarted(context);
            stateMachine.ChangeState(stateMachine.RunningState);
        }

        private void playSound()
        {
            AudioManager.Instance.RandomSoundEffect(stateMachine.Player.Data.AudioData.PlayerMovementAudioData.Footstep.audioList, 0.65f, stateMachine.Player.Data.AudioData.PlayerMovementAudioData.Footstep.Cate);
        }

        #endregion

    }
}
