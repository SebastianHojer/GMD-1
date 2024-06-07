using System.Collections;
using System.Collections.Generic;
using Common;
using GameLogic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour, ICustomer
    {
        public static PlayerController Instance { get; private set; }
        
        public float attackCooldown;
        
        private PlayerInputActions _playerControls;

        private InputAction _interactionInput;
        private InputAction _movementInput;
        private InputAction _attackInput;
        
        [SerializeField] private Transform weapon;
        private SpriteRenderer _weaponSpriteRenderer;
        private Collider2D _weaponCollider;
        private Item.ItemType _currentWeapon = Item.ItemType.Dagger;
        
        private Vector3 _initialPosition;
        private Movement.Movement _movement;
        private Interaction.Interaction _interaction;
        private Combat.Attack _attack;
        private Health _health;
        private Animator _animator;
        private float _lastAttackTime;
        private bool _isDead;
        
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                _playerControls = new PlayerInputActions();
                SceneManager.sceneLoaded += OnSceneLoaded;
                var gm = GameManager.Instance;
                _initialPosition = transform.position;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            SetupInput();
        }
        
        private void Start()
        {
            _movement = GetComponent<Movement.Movement>();
            _interaction = GetComponentInChildren<Interaction.Interaction>();
            _attack = GetComponent<Combat.Attack>();
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            
            InitializeWeapon();
            SubscribeToHealthEvents();
        }
    
        private void OnEnable()
        {
            _movementInput?.Enable();
            _interactionInput?.Enable();
            _attackInput?.Enable();
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        private void OnDisable()
        {
            _movementInput?.Disable();
            _interactionInput?.Disable();
            _attackInput?.Disable();
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
            SetupInput();
        }

        private void SetupInput()
        {
            _movementInput = _playerControls.Player.Move;
            _interactionInput = _playerControls.Player.Interact;
            _attackInput = _playerControls.Player.Fire;
        }

        private void Update()
        {
            Vector2 moveInput = _movementInput.ReadValue<Vector2>(); 
            _movement.Move(moveInput); 
            
            _interaction.CheckForInteractions();
            
            if (_interactionInput.triggered)
            {
                _interaction.InteractWithClosest();
            }

            if (_attackInput.triggered)
            {
                Attack();
            }
        }
        
        private void Attack()
        {
            if (Time.time - _lastAttackTime > attackCooldown)
            {
                _lastAttackTime = Time.time;
                StartCoroutine(PerformAttackAfterAnimation());
            }
        }

        private IEnumerator PerformAttackAfterAnimation()
        {
            _animator.SetTrigger(IsAttacking);
            // Wait for 80% of the animation length before performing the attack
            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 0.8f);
            _attack.PerformAttack();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Stop player movement when triggering border
            if (other.CompareTag("Border"))
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else if (other.CompareTag("Coin"))
            {
                Destroy(other.GameObject());
                CoinManager.Instance.AddToBalance(5);
                AudioManager.Instance.Play("Coin");
            }
            else if (other.CompareTag("Healing"))
            {
                Destroy(other.GameObject());
                _health.AddHealth(25);
                Debug.Log("Healed");
            }
        }
        
        private void InitializeWeapon()
        {
            GameObject weaponPrefab = Item.GetWeaponPrefab(_currentWeapon);
            GameObject weaponInstance = Instantiate(weaponPrefab, weapon.position, weapon.rotation, weapon);
            _weaponSpriteRenderer = weaponInstance.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer.sortingLayerName = "Foreground";
            _weaponSpriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
            
            _attack.SetAttackRange(Item.GetAttackRange(_currentWeapon));
            _attack.SetDamage(Item.GetDamage(_currentWeapon));
        }
        
        public void ChangeWeapon(Item.ItemType itemType)
        {
            if (_weaponSpriteRenderer != null)
            {
                Destroy(_weaponSpriteRenderer.gameObject);
            }
            
            _currentWeapon = itemType;
            InitializeWeapon();
        }

        public void BuyItem(Item.ItemType itemType)
        {
            ChangeWeapon(itemType);
            AudioManager.Instance.Play("Buy");
        }
        
        private void SubscribeToHealthEvents()
        {
            if (_health != null)
            {
                _health.OnDie += HandleDie;
            }
        }
        
        public bool PlayerIsDead()
        {
            return _isDead;
        }

        private void HandleDie()
        {
            if (!_isDead)
            {
                _isDead = true;
                _animator.SetTrigger(IsDead);
                OnDisable();
                GameManager.Instance.CheckPlayerDeath();
            }
        }
        
        public void ResetPlayer()
        {
            _isDead = false;
            ChangeWeapon(Item.ItemType.Dagger);
            _health.Reset();
            _animator.Rebind();
            OnEnable();
        }
    }
}
