using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace akistd
{

    public class Interactor : MonoBehaviour
    {
        
        [SerializeField]
        private Effector[] effectors;

        [SerializeField]
        private GameAudioData gameAudioData;

        [Serializable]
        public struct Effector
        {
            public GameObject target;
            public Vector3 centerPos;
            public float RoundAboutAmount;
        }

        private bool shouldRotate = false;

        private bool touchOnce =false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (shouldRotate)
            {

                foreach (var item in effectors)
                {

                    StartCoroutine(WaitForRotating(1f, item));
                }

            }

            
        }


        private void OnTriggerEnter(Collider other)
        {
           if (!touchOnce)
            {
                if (other.gameObject.tag == "Weapon")
                {
                    shouldRotate = true;
                    Material mat = this.GetComponent<Renderer>().material;
                    mat.color = Color.green;

                    touchOnce = true;
                    AudioManager.Instance.Play(gameAudioData.audioData[0].clip);
                }
            }
        }

        IEnumerator WaitForRotating(float sec, Effector item)
        {

            item.target.transform.RotateAround(item.centerPos, Vector3.up, item.RoundAboutAmount * Time.deltaTime);
            yield return new WaitForSeconds(sec);
            AudioManager.Instance.Play(gameAudioData.audioData[1].clip);
            shouldRotate = false;
            Destroy(gameObject);
        }
    }
}
