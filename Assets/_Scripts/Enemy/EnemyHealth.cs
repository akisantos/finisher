using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class EnemyHealth : Health, IHealth
    {
        public EnemyHealth()
        {
            CurrentHealth = MaxHealth;

        }
        public void DealDamage()
        {
            
        }

        public void TakeDamage(float amount)
        {

            if (CurrentHealth - amount < 0)
            {
                CurrentHealth = 0;
                
            }
            else
            {
                CurrentHealth -= amount;
            }
        }

        public void Heal(float amount)
        {
            
        }

        
    }
}
