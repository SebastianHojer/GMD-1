using Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Health _health;
        public Slider healthBar;

        private void Start()
        {
            _health = GetComponent<Health>();
            if (_health != null)
            {
                _health.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(_health.maxHealth, _health.maxHealth);
            }
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }
}