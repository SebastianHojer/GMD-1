using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        private float currentHealth;
        public Slider healthBar;
        
        public event Action OnHurt;
        public event Action OnDie;
        private bool isDead = false;
        
        private void Start()
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }

        public void TakeDamage(float amount)
        {
            if (isDead) return;
            
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthBar();

            if (currentHealth > 0)
            {
                OnHurt?.Invoke();
            }
            else
            {
                isDead = true;
                OnDie?.Invoke();
                Die();
            }
        }

        private void UpdateHealthBar()
        {
            if (healthBar)
            {
                healthBar.value = currentHealth / maxHealth;
            }
        }

        private void Die()
        {
            Destroy(gameObject, 3f);
        }
    }
}