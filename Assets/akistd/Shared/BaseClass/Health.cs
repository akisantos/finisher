using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public abstract class Health
    {
        private float currentHealth;
        private float maxHealth = 100f;

        public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
        protected float MaxHealth { get => maxHealth; }
    }
}
