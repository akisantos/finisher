using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class causeDamage : MonoBehaviour
    {

        private bool isCausedDamage = false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && !isCausedDamage)
            {
                StartCoroutine("causeDamageResetCoolDown",0.5f);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && !isCausedDamage)
            {
                StartCoroutine("causeDamageResetCoolDown", 0.5f);
            }
        }

        IEnumerator causeDamageResetCoolDown(float time)
        {
            isCausedDamage = true;
            GameObject.Find("Player").GetComponent<FirstPerson.Player>().PlayerHealth.TakeDamage(20f);
            yield return new WaitForSeconds(time);
            isCausedDamage = false;
        }
    }
}
