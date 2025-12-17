using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class speedup : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine("speedUpCoolDown", 5f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine("speedUpCoolDown", 5f);
            }
        }

        IEnumerator speedUpCoolDown(float time)
        {
            float oldSpeed = GameObject.Find("Player").GetComponent<FirstPerson.Player>().Data.GroundedData.BaseSpeed;
            GameObject.Find("Player").GetComponent<FirstPerson.Player>().Data.GroundedData.BaseSpeed = 15f;
            UIManager.Instance.SpeedUpEffect(time);
            yield return new WaitForSeconds(time);
            GameObject.Find("Player").GetComponent<FirstPerson.Player>().Data.GroundedData.BaseSpeed = oldSpeed;
        }
    }
}
