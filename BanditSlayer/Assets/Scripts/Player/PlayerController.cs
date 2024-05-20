using Collectables;
using Common;
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

        private InputAction _interactionInput;
        private InputAction _movementInput;
        
        [SerializeField] private Transform weapon;
        private SpriteRenderer _weaponSpriteRenderer;
        private Item.ItemType _currentWeapon;

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
            _movementInput = _playerControls.Player.Move;
            _interactionInput = _playerControls.Player.Interact; 
        }
        
        private void Start()
        {
            movement = GetComponent<Movement.Movement>();
            interaction = GetComponentInChildren<Interaction.Interaction>();
        }
    
        private void OnEnable()
        {
            _movementInput?.Enable();
            _interactionInput?.Enable();
        }

        private void OnDisable()
        {
            _movementInput?.Disable();
            _interactionInput?.Disable();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
            }
            _movementInput = _playerControls.Player.Move;
            _interactionInput = _playerControls.Player.Interact;
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
        
        public void ChangeWeapon(Item.ItemType itemType)
        {
            _weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
            if (_weaponSpriteRenderer != null)
            {
                _weaponSpriteRenderer.sprite = Item.GetSprite(itemType);
            }
            Debug.Log(_weaponSpriteRenderer);
            _currentWeapon = itemType;
            Debug.Log(_currentWeapon);
        }

        public void BuyItem(Item.ItemType itemType)
        {
            ChangeWeapon(itemType);
        }
    }
}
