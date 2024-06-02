using Common;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public int damage = 10;

    private bool _isFacingRight;
    private Transform player;
    private Animator animator;
    private Health playerHealth;
    private Health health;
    private float lastAttackTime;
    private bool isDead;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();
    }

    private void Update()
    {
        if (isDead) return;

        if (!player) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
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
        animator.SetBool(IsMoving, true);

        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && _isFacingRight)
        {
            Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
        if (Time.time - lastAttackTime > attackCooldown)
        {
            animator.SetTrigger(IsAttacking);
            lastAttackTime = Time.time;

            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health.TakeDamage(amount);

        if (health.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger(IsDead);
        Destroy(gameObject, 2f); 
    }
}
}