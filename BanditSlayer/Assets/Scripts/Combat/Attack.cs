using System;
using Common;
using UnityEngine;

namespace Combat
{
    public class Attack : MonoBehaviour
    {
        private float _attackRange;
        private int _damage;

        public event Action OnAttack;

        public void SetAttackRange(float attackRange)
        {
            _attackRange = attackRange;
        }

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void PerformAttack()
        {
            OnAttack?.Invoke();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange);

            foreach (Collider2D hitCollider in hitEnemies)
            {
                Debug.Log("Hit: " + hitCollider.gameObject.name);
                if (CompareTag("Player") && !hitCollider.CompareTag("Enemy"))
                {
                    continue;
                }
                
                if (CompareTag("Enemy") && !hitCollider.CompareTag("Player"))
                {
                    continue;
                }
                
                var health = hitCollider.GetComponent<Health>();
                if (health)
                {
                    Debug.Log(gameObject.name + " attacked " + hitCollider.gameObject.name);
                    health.TakeDamage(_damage);
                }
            }
        }
    }
}