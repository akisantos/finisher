using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerSlidingState : PlayerGroundedState
    {

        private PlayerSlidingData slidingData;

        private float startTime;
        private int slideUsedCount;

        public PlayerSlidingState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
            slidingData = movementData.SlidingData;
        }


        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            if (stateMachine.ResuableData.ShouldWalk)
            {
                stateMachine.ResuableData.MovementSpeedModifier = slidingData.SpeedWalkModifier;
            }
            else
            {
                stateMachine.ResuableData.MovementSpeedModifier = slidingData.SpeedRunModifier;
            }
            
            stateMachine.ResuableData.CurrentJumpForce = airboneData.JumpData.StrongForce;
            StopAnimation(stateMachine.Player.AnimationData.MovingParameterHash);
            StartAnimation(stateMachine.Player.AnimationData.SlideParameterHash);
            AddForceFromBaseState();
            TriggerAnimeSlash();
            UpdateConsecutiveSlide();
            startTime = Time.time;
        }

        

        public override void Exit()
        {
            base.Exit();
            stateMachine.Player.GetComponent<CapsuleCollider>().height = 1.35f;
            stateMachine.Player.GetComponent<CapsuleCollider>().center = new Vector3(0f, 1.115f, 0f);
            TriggerAnimeSlash();
            StopAnimation(stateMachine.Player.AnimationData.SlideParameterHash);
            
            
        }
        public override void PhysicsUpdate()
        {

        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();  
            if (stateMachine.ResuableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                return;
            }


            if (stateMachine.ResuableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkkingState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.RunningState);
            }
        }

        #endregion



        #region Main Methods

        

        private void UpdateConsecutiveSlide()
        {
            if (!isConsecutive())
            {
                slideUsedCount = 0;
            }

            ++slideUsedCount;

            if (slideUsedCount == slidingData.ConsecutiveSlidingAmount)
            {
                slideUsedCount = 0;

                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Sliding, slidingData.CoolDown);
            }
        }

        private bool isConsecutive()
        {
            return Time.time < startTime + slidingData.TimeToBeConsecutive;
        }

        private void AddForceFromBaseState()
        {
            if (stateMachine.ResuableData.MovementInput == Vector2.zero)
            {
                return;
            }

            stateMachine.Player.GetComponent<CapsuleCollider>().height = 1.35f * 0.45f;
            stateMachine.Player.GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.74f, 0f);

            Vector3 playerForward = stateMachine.Player.transform.forward;
            stateMachine.Player.Rigidbody.AddForce(playerForward * GetMovementSpeed(), ForceMode.VelocityChange);
            
            AudioManager.Instance.Play(stateMachine.Player.Data.AudioData.PlayerMovementAudioData.SwordAttackHit.audioList[0]);
        }

        #endregion

        #region Input Methods

        protected override void OnSlidingStarted(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        protected override void OnWalktoggleStarted(InputAction.CallbackContext context)
        {
            
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            base.OnJumpStarted(context);
        }

        protected override void AddInputActionCallback()
        {
            stateMachine.Player.Input.PlayerActions.Jump.started += OnJumpStarted;
        }

        private void TriggerAnimeSlash()
        {
            GameObject vfx = GameObject.Find("Main Camera").transform.Find("AnimeWind").gameObject;

            vfx.transform.localRotation = Quaternion.Euler(25f, 180f, 0);

            ParticleSystem par = vfx.GetComponent<ParticleSystem>();

            var main = par.main;
            main.startSpeedMultiplier = 7f;

            ParticleSystem.EmissionModule emission = par.emission;
            emission.rateOverTime = 100;
            emission.rateOverTime = Mathf.Lerp(50, 100, 2f);

            GameObject.Find("Main Camera").transform.Find("AnimeWind").gameObject.SetActive(!vfx.activeSelf);
        }

        #endregion
    }
}
