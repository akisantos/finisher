using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class GCore : Enemy
    {
        [SerializeField]
        private AudioClip destroySound;

        private bool touchOnce= true;

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            if (EnemyHealth.CurrentHealth <= 0)
            {
                if (touchOnce)
                {
                    AudioManager.Instance.Play(destroySound);
                    GameManager.Instance.GameWin();
                }

                touchOnce=false;

            }
        }


    }
}
