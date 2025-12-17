using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd.FirstPerson
{
    public class HoldingState : PlayerCombatState
    {
        public HoldingState(PlayerCombatStateMachine combatStateMachine) : base(combatStateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            StartAnimation(stateMachine.Player.AnimationData.combatHoldingParameterHash);

            
        }

        public override void Exit()
        {
            base.Exit();
            StopAnimation(stateMachine.Player.AnimationData.combatHoldingParameterHash);

        }

        protected override void AddInputActionCallback()
        {
            base.AddInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Attack.performed += changeToSwordAttack;
        }

        protected override void RemoveInputActionCallback()
        {
            base.RemoveInputActionCallback();
            stateMachine.Player.Input.PlayerActions.Attack.performed -= changeToSwordAttack;

        }

        private void changeToSwordAttack(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.SwordAttackingState);
        }
    }
}
