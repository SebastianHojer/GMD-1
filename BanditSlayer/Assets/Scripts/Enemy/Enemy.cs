using System.Collections;
using Common;
using Combat;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public float attackCooldown;
        public float _attackRange;
        
        private bool _isFacingRight;
        private float damageModifier;
        private Transform _player;
        private Animator _animator;
        private Health _health;
        private Attack _attack;
        private float _lastAttackTime;
        private Collider2D _weaponCollider;
        private bool _isDead;
        private bool _isAttacking;
        
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject healingItemPrefab;
        [SerializeField] private Item.ItemType currentWeapon;
        
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsHurt = Animator.StringToHash("isHurt");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _attack = GetComponent<Attack>();
        }

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;

            InitializeWeapon();
            SubscribeToHealthEvents();
        }
        
        public void SetDamageModifier(float modifier)
        {
            damageModifier = modifier;
        }

        private void InitializeWeapon()
        {
            _attackRange = Item.GetAttackRange(currentWeapon);
            int baseDamage = Item.GetDamage(currentWeapon);
            _attack.SetAttackRange(_attackRange);
            _attack.SetDamage((int)(baseDamage * damageModifier));
        }

        private void Update()
        {
            if (_isDead) return;
            if (!_player) return;
            if (_isAttacking) return;

            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);
            
            if (distanceToPlayer > _attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }

        private void MoveTowardsPlayer()
        {
            _animator.SetBool(IsMoving, true);

            Vector2 direction = (_player.position - transform.position).normalized;
            if (direction.x > 0 && !_isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && _isFacingRight)
            {
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
        }
        
        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void AttackPlayer()
        {
            if (Time.time - _lastAttackTime > attackCooldown && !_isAttacking)
            {
                _lastAttackTime = Time.time;
                StartCoroutine(PerformAttackAfterAnimation());
            }
        }

        private IEnumerator PerformAttackAfterAnimation()
        {
            _isAttacking = true;
            _animator.SetTrigger(IsAttacking);
            // Wait for 80% of the animation length before performing the attack SUSPICIOUS
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 0.8f);
            _attack.PerformAttack();
            _isAttacking = false;
        }

        private void SubscribeToHealthEvents()
        {
            if (_health != null)
            {
                _health.OnDie += HandleDie;
            }
        }

        private void HandleDie()
        {
            _isDead = true;
            _animator.SetTrigger(IsDead);
            
            float yOffset = 2f;
            
            // Drop coins
            int numCoins = Random.Range(1, 6);
            for (int i = 0; i < numCoins; i++)
            {
                float xOffset = Random.Range(-5f, 5f);
                Vector3 position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
                var coin = Instantiate(coinPrefab, position, Quaternion.identity);
                coin.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
            }

            // Drop healing item
            if (Random.value < 0.5f)
            {
                float xOffset = Random.Range(-5f, 5f);
                Vector3 position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);
                var healingItem = Instantiate(healingItemPrefab, position, Quaternion.identity);
                healingItem.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
            }
            Destroy(gameObject, 0.5f);
        }
    }
}
