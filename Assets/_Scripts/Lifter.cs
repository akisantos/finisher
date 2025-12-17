using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class Lifter : MonoBehaviour
    {

        private Vector3 holder;

        private void Start()
        {
            holder = GameObject.Find("Player").GetComponent<FirstPerson.Player>().Data.AirboneData.JumpData.MediumForce;
        }

        private void OnTriggerEnter(Collider collider)
        {

            if (collider.gameObject.tag == "Player")
            {
                Debug.Log(collider.gameObject.name);
                GameObject player = GameObject.Find("Player");
                player.GetComponent<Rigidbody>().AddForce(player.transform.forward * 1000f * Time.deltaTime, ForceMode.Force);
            }
        }

    


    }
}
