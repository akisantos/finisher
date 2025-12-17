using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
        }


        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            stateMachine.ResuableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
            stateMachine.ResuableData.CurrentJumpForce = airboneData.JumpData.MediumForce;
            StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
            TriggerAnimeSlash();
        }

        public override void Exit()
        {
            base.Exit();
            TriggerAnimeSlash();
            StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
            RemoveInputActionCallback();
  
        }

        #endregion



        #region Resuable Methods

        public override void Update()
        {
            base.Update();
            playSound();
        }
        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();

        }

        private void playSound()
        {
            AudioManager.Instance.RandomSoundEffect(stateMachine.Player.Data.AudioData.PlayerMovementAudioData.Footstep.audioList, 1.1f);
        }


        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.PlayerMediumStoppingState);
        }
        protected override void OnWalktoggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalktoggleStarted(context);
            stateMachine.ChangeState(stateMachine.WalkkingState);
        }


        #endregion

        #region Aki Method

        private void TriggerAnimeSlash()
        {
            GameObject vfx = GameObject.Find("Main Camera").transform.Find("AnimeWind").gameObject;

            Vector2 inputDir = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
            if (inputDir.y >= 0)
            {
                vfx.transform.localRotation = Quaternion.Euler(0, 180f, 0);
                ParticleSystem par = vfx.GetComponent<ParticleSystem>();

                var main = par.main;
                main.startSpeedMultiplier = 2f;
                ParticleSystem.EmissionModule emission = par.emission;
                emission.rateOverTime = 50;

                emission.rateOverTime = Mathf.Lerp(100, 50, 2f);
               
            }
            GameObject.Find("Main Camera").transform.Find("AnimeWind").gameObject.SetActive(!vfx.activeSelf);



        }


        #endregion
    }
}
