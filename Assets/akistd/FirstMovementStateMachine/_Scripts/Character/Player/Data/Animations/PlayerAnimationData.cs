using System;

using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerAnimationData
    {
        [Header("State group parameter names")]
        [SerializeField] private string movingParameterName = "Moving";
        [SerializeField] private string stoppingParameterName = "Stopping";

        [Header("Grounded Parameter Names")]
        [SerializeField] private string idleParameterName = "isIdling";
        [SerializeField] private string walkParameterName = "isWalking";
        [SerializeField] private string runParameterName = "isRunning";
        [SerializeField] private string mediumStopParameterName = "isMediumStopping";
        [SerializeField] private string hardStopParameterName = "isHardStopping";
        [SerializeField] private string slideParameterName = "isSliding";

        [Header("Airbone Parameter Names")]
        [SerializeField] private string jumpParameterName = "isJumping";


        [Header("Combat State group parameter names")]
        [SerializeField] private string combatHoldingParameterName = "Holding";
        [SerializeField] private string combatAttackingParameterName = "Attacking";

        [Header("Combat Holding parameter names")]
        [SerializeField] private string attackingSword1ParameterName = "isAttackingSword1";
        [SerializeField] private string attackingSword2ParameterName = "isAttackingSword2";




        public int MovingParameterHash { get; private set; } 
        public int StoppingParameterHash { get; private set; } 
        public int IdleParameterHash { get; private set; } 
        public int WalkParameterHash { get; private set; } 
        public int RunParameterHash { get; private set; } 
        public int MediumStopParameterHash { get; private set; } 
        public int HardStopParameterHash { get; private set; }
        public int JumpParameterName { get ; private set; }
        public int SlideParameterHash { get ; private set; }

        public int combatAttackingParameterHash { get; private set; }
        public int attackingSword1ParameterHash { get; private set; }
        public int attackingSword2ParameterHash { get; private set; }

        public int combatHoldingParameterHash { get; private set; }

        public void Initialize()
        {
            MovingParameterHash = Animator.StringToHash(movingParameterName);
            StoppingParameterHash = Animator.StringToHash(stoppingParameterName);
            IdleParameterHash = Animator.StringToHash(idleParameterName);
            WalkParameterHash = Animator.StringToHash(walkParameterName);
            RunParameterHash = Animator.StringToHash(runParameterName);
            MediumStopParameterHash = Animator.StringToHash(mediumStopParameterName);
            HardStopParameterHash = Animator.StringToHash(hardStopParameterName);

            JumpParameterName = Animator.StringToHash(jumpParameterName);
            SlideParameterHash = Animator.StringToHash(slideParameterName);

            combatHoldingParameterHash = Animator.StringToHash(combatHoldingParameterName);
            //Combat Sword
            combatAttackingParameterHash = Animator.StringToHash(combatAttackingParameterName);
            attackingSword1ParameterHash = Animator.StringToHash(attackingSword1ParameterName);
            attackingSword2ParameterHash = Animator.StringToHash(attackingSword2ParameterName);

        }

    }
}
