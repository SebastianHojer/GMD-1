using System;
using Common;
using UnityEngine;

namespace Combat
{
    public class Attack : MonoBehaviour
    {
        private Collider2D _weaponCollider;
        private int _damage;

        public event Action OnAttack;

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
            OnAttack?.Invoke();
            
            if (_weaponCollider)
            {
                var bounds = _weaponCollider.bounds;
                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);

                foreach (Collider2D hitCollider in hitEnemies)
                {
                    if (CompareTag("Enemy") && !hitCollider.CompareTag("Player"))
                    {
                        continue;
                    }
                    
                    var health = hitCollider.GetComponent<Health>();
                    if (health != null)
                    {
                        health.TakeDamage(_damage);
                    }
                }
            }
        }
    }
}