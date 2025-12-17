using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        private Player player;
        private void Awake()
        {
            player = transform.parent.GetComponent<Player>();
        }

        public void TriggerOnMovementStateAnimEnterEvent()
        {
            player.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnMovementStateAnimExitEvent()
        {
            player.OnMovementStateAnimationExitEvent();
        }

        public void TriggerOnMovementStateAnimTransitionEvent()
        {
            
            player.OnMovementStateAnimationTransitionEvent();
        }
    }
}
