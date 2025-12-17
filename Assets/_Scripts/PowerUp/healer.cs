using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class healer : MonoBehaviour
    {
        [SerializeField]
        private float amount;

        [SerializeField]
        private AudioClip healSound;

        [SerializeField]
        private Material screenMat;

        private bool once =true;



        private void Start()
        {
            screenMat.SetFloat("_Intensity",0f);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up,Space.Self);


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && once)
            {

                once = false;

                AudioManager.Instance.Play(healSound);
                GameObject.Find("Player").GetComponent<akistd.FirstPerson.Player>().PlayerHealth.Heal(amount);
                UIManager.Instance.healingEffect();
                Destroy(gameObject);
            }
        }

       /* IEnumerator healEffect()
        {
            screenMat.SetFloat("_Intensity", 0.332f);
            AudioManager.Instance.Play(healSound);
            GameObject.Find("Player").GetComponent<akistd.FirstPerson.Player>().PlayerHealth.Heal(amount);
            yield return new WaitForSeconds(1f);

            screenMat.SetFloat("_Intensity", 0);

            Destroy(gameObject);
        }*/
    }
}
