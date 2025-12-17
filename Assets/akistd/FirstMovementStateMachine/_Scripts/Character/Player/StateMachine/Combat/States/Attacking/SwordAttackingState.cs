using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class SwordAttackingState : AttackingState
    {
        public SwordAttackingState(PlayerCombatStateMachine combatStateMachine) : base(combatStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            int randomNumber = Random.Range(1, 3);
            GameObject trail = GameObject.FindWithTag("Weapon").transform.Find("Trail").gameObject;
            trail.SetActive(true);
            if (randomNumber== 1)
            {
                
                StartAnimation(stateMachine.Player.AnimationData.attackingSword1ParameterHash);
            }
            else
            {
                StartAnimation(stateMachine.Player.AnimationData.attackingSword2ParameterHash);
            }


        }


        public override void Exit()
        {

            GameObject trail = GameObject.FindWithTag("Weapon").transform.Find("Trail").gameObject;
            trail.SetActive(false);
            base.Exit();
            /*stateMachine.Player.PlayerHand.GetComponentInChildren<DamageDealer>().ClearHit();*/
            StopAnimation(stateMachine.Player.AnimationData.attackingSword1ParameterHash);
            StopAnimation(stateMachine.Player.AnimationData.attackingSword2ParameterHash);

        }


        public override void OnAnimationTransitionEvent()
        {
            StopAnimation(stateMachine.Player.AnimationData.combatAttackingParameterHash);
            StopAnimation(stateMachine.Player.AnimationData.attackingSword1ParameterHash);
            StopAnimation(stateMachine.Player.AnimationData.attackingSword2ParameterHash);
            stateMachine.ChangeState(stateMachine.HoldingState);
        }

        private void playSound(akistd.FirstPerson.PlayerMovementAudioData.AudioList audio)
        {
            AudioManager.Instance.RandomSoundEffect(audio.audioList,0.95f,audio.Cate);
        }


    }
}
