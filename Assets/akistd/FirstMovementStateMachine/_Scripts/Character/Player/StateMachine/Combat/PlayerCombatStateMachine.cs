using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class PlayerCombatStateMachine : StateMachine
    {
        public Player Player { get;}
        public PlayerStateResuableData ResuableData { get;}

        public AttackingState AttackingState { get;}
        public SwordAttackingState SwordAttackingState { get;}

        public HoldingState HoldingState { get;}


        public PlayerCombatStateMachine(Player player)
        {
            Player = player;
            ResuableData = new PlayerStateResuableData();


            AttackingState = new AttackingState(this);

            SwordAttackingState = new SwordAttackingState(this);

            HoldingState = new HoldingState(this);
        }

    }
}

