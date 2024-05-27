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
        private static PlayerController instance;
        
        private PlayerInputActions _playerControls;
        
        [SerializeField] private Movement.Movement movement;
        [SerializeField] private Interaction.Interaction interaction;
        [SerializeField] private Combat.Attack attack;

        private InputAction _interactionInput;
        private InputAction _movementInput;
        private InputAction _attackInput;
        
        [SerializeField] private Transform weapon;
        private SpriteRenderer _weaponSpriteRenderer;
        private Collider2D _weaponCollider;
        private Item.ItemType _currentWeapon = Item.ItemType.Dagger;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                _playerControls = new PlayerInputActions();
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            SetupInput();
        }
        
        private void Start()
        {
            movement = GetComponent<Movement.Movement>();
            interaction = GetComponentInChildren<Interaction.Interaction>();
            attack = GetComponent<Combat.Attack>();
            
            InitializeWeapon();
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
            movement.Move(moveInput); 
            
            interaction.CheckForInteractions();
            
            if (_interactionInput.triggered)
            {
                interaction.InteractWithClosest();
            }

            if (_attackInput.triggered)
            {
                Debug.Log("Attacking");
                attack.PerformAttack();
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
            Debug.Log("Current weapon: " + _currentWeapon);
            Debug.Log("Sprite renderer: " + _weaponSpriteRenderer);
            Debug.Log(_weaponCollider == null);
            
            attack.SetWeaponCollider(_weaponCollider);
            attack.SetDamage(Item.GetDamage(_currentWeapon));
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
    }
}
