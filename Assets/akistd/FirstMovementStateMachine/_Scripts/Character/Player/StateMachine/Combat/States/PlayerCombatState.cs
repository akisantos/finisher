using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerCombatState : IState
    {
        protected PlayerCombatStateMachine stateMachine;
        
        public PlayerCombatState(PlayerCombatStateMachine combatStateMachine)
        {
            stateMachine = combatStateMachine;
        }

        #region IState

        public virtual void Enter()
        {
            //Debug.Log("state: " + GetType().Name);
            AddInputActionCallback();
        }

        public virtual void Exit()
        {
           RemoveInputActionCallback();
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void OnAnimationEnterEvent()
        {
            
        }

        public virtual void OnAnimationExitEvent()
        {
            
        }

        public virtual void OnAnimationTransitionEvent()
        {
           
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
           
        }

        public virtual void PhysicsUpdate()
        {
           
        }

        public virtual void Update()
        {
            
        }

        #endregion

        #region main methods


        #endregion

        #region Reusable Methods

        protected void StartAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.Player.Animator.SetBool(animationHash, false);
        }

        protected virtual void AddInputActionCallback()
        {
           
        }

        protected virtual void RemoveInputActionCallback()
        {

        }  

        
            
        #endregion
    }
}
