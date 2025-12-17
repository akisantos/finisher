using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class swordFunc : MonoBehaviour
    {


        private void OnCollisionEnter(Collision collision)
        {

            if (transform.parent == null && collision.gameObject.tag == "Enemy")
            {

                collision.gameObject.GetComponent<Enemy>().TakeDamage(110f);
            }
        }
    }
}
