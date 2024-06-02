using Common;
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
            _animator.SetTrigger(IsAttacking);
            
            if (_weaponCollider)
            {
                var bounds = _weaponCollider.bounds;
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);

                foreach (Collider2D hitCollider in hitEnemies)
                {
                    Enemy.Enemy enemy = hitCollider.GetComponent<Enemy.Enemy>();
                    if (enemy.CompareTag("Enemy"))
                    {
                        enemy.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}