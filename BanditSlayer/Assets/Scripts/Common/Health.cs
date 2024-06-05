using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Health : MonoBehaviour
    {
        public float maxHealth;
        private float _currentHealth;
        
        public event Action<float, float> OnHealthChanged;
        public event Action OnDie;
        private bool _isDead = false;
        
        private void Start()
        {
            _currentHealth = maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }

        public void TakeDamage(float amount)
        {
            if (_isDead) return;
            
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);

            if(_currentHealth <= 0)
            {
                _isDead = true;
                OnDie?.Invoke();
            }
        }

        public void AddHealth(float amount)
        {
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        }
    }
}