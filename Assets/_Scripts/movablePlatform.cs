using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class movablePlatform : MonoBehaviour
    {
        public Vector3 position1;
        public Vector3 position2;
        public float speed;
        public float waitTime;

        void Start()
        {
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            while (true)
            {
                yield return StartCoroutine(MoveObject(transform, position1, position2, speed));
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(MoveObject(transform, position2, position1, speed));
                yield return new WaitForSeconds(waitTime);
            }
        }

        IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
        {
            var i = 0.0f;
            var rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trgefgef");
            if (other.gameObject.tag == "Player")
            {
                GameObject.Find("Player").transform.SetParent(this.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GameObject.Find("Player").transform.SetParent(null);
            }
        }
    }
}
