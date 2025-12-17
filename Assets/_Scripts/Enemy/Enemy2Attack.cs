using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    [RequireComponent(typeof(SphereCollider))]
    public class Enemy2Attack : Enemy
    {
        [SerializeField]
        private SphereCollider sphereCollider;

        private bool causeDamage;


        [SerializeField]
        private AudioClip detSoundEffect;
        [SerializeField]
        private AudioClip activeBombSoundEffect;

        bool isAttacked=false;


        public override void Awake()
        {
            base.Awake();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = 4.68f;
        }
        

        public override void Update()
        {
            base.Update();
            if (causeDamage)
            {
                Debug.Log("Im causing damage");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                causeDamage = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                causeDamage = false;
            }
        }

        public override void AttackPlayer()
        {
            transform.LookAt(player.transform.Find("Head"));
            agent.speed = 1000f;
            ChasePlayer();

            if (!isAttacked)
            {
                StartCoroutine(Boom(1.5f));
                isAttacked = true;
            }
            

        }

        IEnumerator Boom(float sec)
        {
            AudioManager.Instance.Play(activeBombSoundEffect);
            yield return new WaitForSeconds(sec);
            //causeDamage = Physics.CheckSphere(transform.position, sphereCollider.radius, whatIsPlayer);
            if (causeDamage)
            {
                player.gameObject.GetComponent<akistd.FirstPerson.Player>().PlayerHealth.TakeDamage(50f);
            }
            Die();
        }

        private void Die()
        {
            Instantiate(detEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            AudioManager.Instance.Play(detSoundEffect);

        }

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
        }

        public override void DestroyEnemy()
        {
            Die();
        }


    }
}
