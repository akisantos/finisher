using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            
            if (other.gameObject.layer == 6  || other.gameObject.layer == 10)
            {
                Destroy(gameObject);
            }
        }
    }
}
