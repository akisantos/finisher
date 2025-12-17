using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class BoostUp : MonoBehaviour
    {
        [SerializeField]
        private float amount;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trgger");
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(Vector3.up * amount, ForceMode.VelocityChange);
        }
    }
}
