using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class PlayerJumpState : PlayerAirboneState
    {
        private int jumpCount=0;
        public PlayerJumpState(PlayerMovementStateMachine movementStateMachine) : base(movementStateMachine)
        {
           
        }
        

        #region IState
        public override void Enter()
        {
            base.Enter();
            stateMachine.ResuableData.MovementSpeedModifier = 0f;
            StartAnimation(stateMachine.Player.AnimationData.JumpParameterName);
            Jump();
            TriggerAnimeSlash();
            playSound();
        }


        public override void Exit()
        {
            base.Exit();
            TriggerAnimeSlash();
            StopAnimation(stateMachine.Player.AnimationData.JumpParameterName);
        }

        private void Jump()
        {

            Vector3 jumpForce = stateMachine.ResuableData.CurrentJumpForce;

            Vector3 playerForward = stateMachine.Player.transform.forward;

            jumpForce.x *= playerForward.x;
            jumpForce.z *= playerForward.z;
            jumpForce = CalculateJumpForceOnSlope(jumpForce);

            stateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
            jumpCount++;
        }

        private Vector3 CalculateJumpForceOnSlope(Vector3 jumpForce)
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.CapsuleColliderUtils.CapsuleColliderData.Collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, airboneData.JumpData.JumpToGroundRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                if (IsMovingUp())
                {
                    float forceModifier = airboneData.JumpData.JumpForceUpdateUpwards.Evaluate(groundAngle);
                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown())
                {
                    float forceModifier = airboneData.JumpData.JumpForceUpdateDownwards.Evaluate(groundAngle);
                    jumpForce.y *= forceModifier;
                }
            }


            return jumpForce;
        }

        public override void OnTriggerEnter(Collider collider)
        {
            base.OnTriggerEnter(collider);
            
            if (collider.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                stateMachine.ChangeState(stateMachine.IdlingState);
                stateMachine.Player.Input.PlayerActions.Jump.Enable();
            }
        }

        private void playSound()
        {
            AudioManager.Instance.Play(stateMachine.Player.Data.AudioData.PlayerMovementAudioData.Jump);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            
        }

        private void TriggerAnimeSlash()
        {
            GameObject vfx = GameObject.Find("Main Camera").transform.Find("AnimeWind").gameObject;
            
            vfx.transform.localRotation = Quaternion.Euler(40f, 180f, 0);

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
