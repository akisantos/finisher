
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class AttackingState : PlayerCombatState
    {
 
        public AttackingState(PlayerCombatStateMachine combatStateMachine) : base(combatStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.Player.AnimationData.combatAttackingParameterHash);
            
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.combatAttackingParameterHash);

        }

        protected override void AddInputActionCallback()
        {
            
            stateMachine.Player.Input.PlayerActions.Attack.performed += OnAttack;
        }

        protected override void RemoveInputActionCallback()
        {
            
            stateMachine.Player.Input.PlayerActions.Attack.performed -= OnAttack;
        }

        protected virtual void OnAttack(InputAction.CallbackContext context)
        {
            if (!stateMachine.Player.Animator.GetBool(stateMachine.Player.AnimationData.combatAttackingParameterHash))
            {
                Debug.LogError(stateMachine.Player.Animator.GetBool(stateMachine.Player.AnimationData.combatAttackingParameterHash));
                stateMachine.ChangeState(stateMachine.SwordAttackingState); stateMachine.ChangeState(stateMachine.SwordAttackingState);
            }
            
        }

        
    }
}
