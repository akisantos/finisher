using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public interface IHealth
    {
        public void TakeDamage(float amount);
        public void Heal(float amount);
        public void DealDamage();
    }
}
