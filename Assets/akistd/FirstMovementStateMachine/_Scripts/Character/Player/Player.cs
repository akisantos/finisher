using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace akistd.FirstPerson
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("Tham chieu")]
        [field:SerializeField] public PlayerSO Data { get; private set; }
        [field: Header("Collisions")]
        [field: SerializeField] public CapsuleColliderUtils CapsuleColliderUtils { get; private set; }
        [field:SerializeField] public PlayerLayerData LayerData { get; private set; }

        [field:Header("Animation")]
        [field:SerializeField] public PlayerAnimationData AnimationData { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public Animator Animator { get; private set; }  
        public PlayerInput Input { get; private set; }

        
        public Transform MainCameraTransform { get; private set; }
        public PlayerHealth PlayerHealth { get => playerHealth; private set => playerHealth = value; }
        public GameObject PlayerHand { get => playerHand; private set => playerHand = value; }

        [SerializeField]
        private Transform cameraTransform;

        private PlayerMovementStateMachine movementStateMachine;
        private PlayerCombatStateMachine combatStateMachine;

        private PlayerHealth playerHealth;

        private GameObject playerHand;

        public GameObject sword;
        


        #region Unity Functions

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
            Input = GetComponent<PlayerInput>();

            CapsuleColliderUtils.Initialize(gameObject);
            CapsuleColliderUtils.CalculateCapsuleColliderDimensions();
            AnimationData.Initialize();


            MainCameraTransform = Camera.main.transform;
            movementStateMachine = new PlayerMovementStateMachine(this);

            combatStateMachine = new PlayerCombatStateMachine(this);
            PlayerHealth = new PlayerHealth();

            PlayerHand = GameObject.Find("Weapon");
            Instantiate(sword,PlayerHand.transform);

        }

        private void OnValidate()
        {
            CapsuleColliderUtils.Initialize(gameObject);
            CapsuleColliderUtils.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            
            movementStateMachine.ResuableData.ShouldWalk = true;
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);

            combatStateMachine.ChangeState(combatStateMachine.HoldingState);

            

        }

        private void Update()
        {
            movementStateMachine.HandleInput();
            movementStateMachine.Update();

            combatStateMachine.Update();

        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
            combatStateMachine.PhysicsUpdate();


        }

        /*private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 20, 250, 120));
            GUILayout.Label("Current Speed: " + movementStateMachine.Player.Rigidbody.velocity.magnitude);
            GUILayout.Label("Walking?: " + movementStateMachine.ResuableData.ShouldWalk);
            GUILayout.Label("Current HP: " + playerHealth.CurrentHealth);
            GUILayout.EndArea();
        }*/

        private void OnTriggerEnter(Collider other)
        {
            movementStateMachine.OnTriggerEnter(other);
            combatStateMachine.OnTriggerEnter(other);

            if (other.gameObject.tag == "DetGround")
            {
                GameManager.Instance.GameLose();
                AudioManager.Instance.lofi();
            }

            if (other.gameObject.tag == "Bullet")
            {
               
                playerHealth.TakeDamage(30f);
                AudioManager.Instance.Play(movementStateMachine.Player.Data.AudioData.PlayerMovementAudioData.HurtAudioList.audioList[0]);
            }
        }

        #endregion

        #region Aki Implement Functions 

        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
            combatStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
            combatStateMachine.OnAnimationExitEvent();
        }
        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
            combatStateMachine.OnAnimationTransitionEvent();
        }

        #endregion
    }
}
