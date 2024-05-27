using UnityEngine;

namespace Combat
{
    public class Attack : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        private Collider2D _weaponCollider;
        private int _damage;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetWeaponCollider(Collider2D weaponCollider)
        {
            _weaponCollider = weaponCollider;
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void PerformAttack()
        {
            // Play the attack animation
            _animator.SetTrigger(IsAttacking);
            
            // Check for enemies within the weapon's collider
            if (_weaponCollider != null)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(_weaponCollider.bounds.center, _weaponCollider.bounds.size, 0f);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        // enemy.GetComponent<Enemy>().TakeDamage(_damage);
                    }
                }
            }
        }
    }
}