using Collectables;
using Common;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour, ICustomer
    {
        private static PlayerController _instance;
        
        private PlayerInputActions _playerControls;

        private InputAction _interactionInput;
        private InputAction _movementInput;
        private InputAction _attackInput;
        
        [SerializeField] private Transform weapon;
        private SpriteRenderer _weaponSpriteRenderer;
        private Collider2D _weaponCollider;
        private Item.ItemType _currentWeapon = Item.ItemType.Dagger;
        
        private Movement.Movement _movement;
        private Interaction.Interaction _interaction;
        private Combat.Attack _attack;
        private Health _health;
        private Animator _animator;
        
        private static readonly int IsHurt = Animator.StringToHash("isHurt");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                _playerControls = new PlayerInputActions();
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else if (_instance != this)
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
            SubscribeToAttackEvents();
        }
    
        private void OnEnable()
        {
            _movementInput?.Enable();
            _interactionInput?.Enable();
            _attackInput?.Enable();
        }

        private void OnDisable()
        {
            _movementInput?.Disable();
            _interactionInput?.Disable();
            _attackInput?.Disable();
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
                Debug.Log("Attacking");
                _attack.PerformAttack();
            }
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
                CoinManager.Instance.AddToBalance(1);
            }
        }
        
        private void InitializeWeapon()
        {
            GameObject weaponPrefab = Item.GetWeaponPrefab(_currentWeapon);
            _weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
            _weaponCollider = weaponPrefab.GetComponent<Collider2D>();
            
            _attack.SetWeaponCollider(_weaponCollider);
            _attack.SetDamage(Item.GetDamage(_currentWeapon));
        }
        
        public void ChangeWeapon(Item.ItemType itemType)
        {
            _currentWeapon = itemType;
            
            InitializeWeapon();
            
            if (_weaponSpriteRenderer != null)
            {
                _weaponSpriteRenderer.sprite = Item.GetSprite(itemType);
            }
        }

        public void BuyItem(Item.ItemType itemType)
        {
            ChangeWeapon(itemType);
        }
        
        private void SubscribeToHealthEvents()
        {
            if (_health != null)
            {
                _health.OnHurt += HandleHurt;
                _health.OnDie += HandleDie;
            }
        }

        private void HandleHurt()
        {
            _animator.SetTrigger(IsHurt);
        }

        private void HandleDie()
        {
            _animator.SetTrigger(IsDead);
        }
        
        private void SubscribeToAttackEvents()
        {
            if (_attack != null)
            {
                _attack.OnAttack += HandleAttack;
            }
        }

        private void HandleAttack()
        {
            _animator.SetTrigger(IsAttacking);
        }
    }
}
