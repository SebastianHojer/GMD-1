using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        private float currentHealth;
        public Slider healthBar;
        
        private Animator animator;

        private static readonly int IsHurt = Animator.StringToHash("isHurt");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private bool isDead = false;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
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
                animator.SetTrigger(IsHurt);
            }
            else
            {
                isDead = true;
                animator.SetTrigger(IsDead);
                Die();
            }
        }
        
        public float GetCurrentHealth()
        {
            return currentHealth;
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
            Destroy(gameObject, 2f);
        }
    }
}