using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class AutoDestroyAfterSec : MonoBehaviour
    {

        [SerializeField]
        private float sec;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(autoDestroy(sec));
        }

        IEnumerator autoDestroy(float sec)
        {
            yield return new WaitForSeconds(sec);
            Destroy(gameObject);
        }
    }
}
