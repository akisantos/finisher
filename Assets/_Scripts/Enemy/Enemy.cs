using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace akistd
{
    public class Enemy : MonoBehaviour
    {

        private EnemyHealth enemyHealth;

        [SerializeField]
        private EnemyData enemyData;

        //AI
        public NavMeshAgent agent;

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;

        //Canh gac
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks = 4f;
        bool alreadyAttacked;
        public GameObject projectile;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange, alreadyKnowPlayer;

        [SerializeField]
        public GameObject detEffect;

        public EnemyHealth EnemyHealth { get => enemyHealth; private set => enemyHealth = value; }

        [SerializeField]
        public ObjectPool<GameObject> _pool;

        private Material bodyMat;
        public virtual void Awake()
        {
            EnemyHealth = new EnemyHealth();

            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            _pool = new ObjectPool<GameObject>(createFunc: () => Instantiate(projectile,transform), actionOnGet: (obj) => obj.SetActive(true), actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: false, defaultCapacity: 10, maxSize: 10);

            bodyMat = GetComponent<Renderer>().material;

        }
        void Start()
        {

        }


        // Update is called once per frame
        public virtual void Update()
        {
            
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && alreadyKnowPlayer) ChasePlayer();
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();

            
        }

        public virtual void Patroling()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }
        public virtual void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        public virtual void ChasePlayer()
        {
     
            agent.SetDestination(player.position);
            bodyMat.color = Color.red;
        }

        public virtual void AttackPlayer()
        {
            //Make sure enemy doesn't move
            agent.SetDestination(transform.position);

            transform.LookAt(player.transform.Find("Head"));

            if (!alreadyAttacked)
            {
                Vector3 dir = transform.forward;

                ///Attack code here

                Rigidbody rb;
                GameObject bl = _pool.Get();
                rb = bl.GetComponent<Rigidbody>();
                bl.transform.position = transform.position;
                bl.transform.localRotation = Quaternion.Euler(0, 0, 90);
               
                bl.transform.parent = null;
                rb.AddForce(bl.transform.forward * 8f, ForceMode.Impulse);

                StartCoroutine(RestoreBullet(bl, 3f));

                ///End of attack code

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
        }
        public virtual void ResetAttack()
        {
            alreadyAttacked = false;
        }


        private IEnumerator RestoreBullet(GameObject bullet, float sec)
        {
            yield return new WaitForSeconds(sec);
            if (bullet.activeSelf)
            {
                _pool.Release(bullet);
            }
        }

        public virtual void DestroyEnemy()
        {
            Instantiate(detEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            AudioManager.Instance.Play(enemyData.enemyDET[0]);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

        public virtual void TakeDamage(float amount)
        {
            EnemyHealth.TakeDamage(amount);

            StartCoroutine("takeDamageColorEffect");

            if (EnemyHealth.CurrentHealth <= 0)
            {
                UIManager.Instance.TriggerKillText();
                Invoke(nameof(DestroyEnemy), 0.025f);
            }
            else
            {
                AudioManager.Instance.Play(enemyData.enemyDET[1]);
            }
        }

        IEnumerator takeDamageColorEffect()
        {
            Color defColor = GetComponent<Renderer>().material.color;
            Material damageColor = GetComponent<Renderer>().material;
            damageColor.color = new Color(1f, .5f, 0.31f);

            yield return new WaitForSeconds(0.5f);

            damageColor.color = defColor;
        }
    }
}
