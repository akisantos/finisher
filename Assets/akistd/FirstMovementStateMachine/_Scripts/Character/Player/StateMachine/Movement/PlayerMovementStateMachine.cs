using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get;}
        public PlayerStateResuableData ResuableData { get;}
        public PlayerIdlingState IdlingState { get; }
        public PlayerWalkingState WalkkingState { get; }
        public PlayerRunningState RunningState { get; }
        public PlayerSprintingState SprintingState { get; }

        public PlayerSlidingState SlidingState { get; }

        public PlayerLightStoppingState PlayerLightStoppingState { get; }
        public PlayerMediumStoppingState PlayerMediumStoppingState { get; }
        public PlayerHardStoppingState PlayerHardStoppingState { get; }

        public PlayerJumpState PlayerJumpState { get; }

        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ResuableData = new PlayerStateResuableData();
            
            IdlingState = new PlayerIdlingState(this);
            WalkkingState = new PlayerWalkingState(this);
            RunningState = new PlayerRunningState(this);
            SprintingState = new PlayerSprintingState(this);
            SlidingState = new PlayerSlidingState(this);

            PlayerLightStoppingState = new PlayerLightStoppingState(this);
            PlayerMediumStoppingState = new PlayerMediumStoppingState(this);
            PlayerHardStoppingState = new PlayerHardStoppingState(this);
            PlayerJumpState = new PlayerJumpState(this);
        }

      

    }
}
