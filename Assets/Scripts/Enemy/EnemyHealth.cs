using Assets.Scripts.Player.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    internal class EnemyHealth:MonoBehaviour
    {
        public float maxHealth;
        public float currentHealth;


        private bool _dead;

        Animator anim;

        public void SetHealth(float healthValue)
        {
            maxHealth = healthValue;
        }


        void Start()
        {
            // Initialize the player's health and the health bar's max value
            currentHealth = maxHealth;
            anim = GetComponent<Animator>();
        }

        // Call this method to apply damage to the player
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensures health never goes below 0



        }

        public float GeCurrentHealth()
        {
            return currentHealth;
        }


        // Call this method to heal the player
        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        }

    }
}
