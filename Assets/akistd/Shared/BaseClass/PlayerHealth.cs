
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class PlayerHealth : Health, IHealth
    {
     
        public PlayerHealth()
        {
            CurrentHealth = MaxHealth;

        }

        public void DealDamage()
        {
            throw new System.NotImplementedException();
        }

        public void Heal(float amount)
        {
  
            if (CurrentHealth + amount > 100)
            {
                
                CurrentHealth = 100;
            }
            else
            {
                CurrentHealth += amount;
            }
        }

        public void TakeDamage(float amount)
        {
            UIManager.Instance.takeDamageEffect();

            if (CurrentHealth - amount <= 0)
            {
                CurrentHealth = 0;
                GameManager.Instance.GameLose();
            }
            else
            {
                
                CurrentHealth -= amount;
            }
        }
    }
}
