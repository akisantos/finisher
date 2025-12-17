using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]
        private float weaponLength;
        private List<GameObject> damagedList = new List<GameObject>();

        private bool isAttacking;

        private void Start()
        {
            isAttacking = GameObject.Find("CharacterModel").GetComponent<Animator>().GetBool("Attacking");
        }
        private void Update()
        {
            isAttacking = GameObject.Find("CharacterModel").GetComponent<Animator>().GetBool("Attacking");

            if (transform.parent.parent != null && isAttacking)
            {
                RaycastHit hit;

                int layerMask = 1 << 9;
                if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
                {
                    Debug.Log("Attacking" + hit.transform.gameObject.name);
                    DealDamage(30f);
                    ClearHit();

                }
            }
        }

        

        public void DealDamage(float amount)
        {

            RaycastHit hit;
            int layerMask = 1 << 9;

            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (!damagedList.Contains(hit.transform.gameObject))
                {
                    Debug.Log("Attacking" + hit.transform.gameObject.name);
                    hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(amount);
                    damagedList.Add(hit.transform.gameObject);
                }

            }

        }

        public void ClearHit()
        {
            damagedList.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position -  transform.up * weaponLength);
        }
    }


}
