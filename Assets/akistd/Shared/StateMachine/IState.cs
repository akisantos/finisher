using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    // interface các hàm mà State cần có
    public interface IState
    {

        // Xử lý khi vào state
        public void Enter();

        // Xử lý khi thoát state
        public void Exit();

        //Xử lý input
        public void HandleInput();
        
        public void Update();
        public void PhysicsUpdate();

        public void OnTriggerEnter(Collider collider);

        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitionEvent();
    }
 
}
