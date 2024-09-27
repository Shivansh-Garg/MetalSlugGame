using Assets.Scripts.Player.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;
        public Slider healthBar;  // Reference to the UI Slider

        private bool _dead;

 
        Animator anim;

        void Start()
        {
            // Initialize the player's health and the health bar's max value
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;

            anim = GetComponent<Animator>();
        }

        // Call this method to apply damage to the player
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensures health never goes below 0
            if (currentHealth > 0)
            {
            }

            UpdateHealthBar();
   
        }

        // Call this method to heal the player
        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

            UpdateHealthBar();
        }




        // Update the health bar to reflect the current health
        void UpdateHealthBar()
        {


            healthBar.value = currentHealth;
        }
    }
}
