using Collectables;
using Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour, ICustomer
    {
        private PlayerInputActions _playerControls;
        
        
        [SerializeField] private Movement.Movement movement;
        [SerializeField] private Interaction.Interaction interaction;

        private InputAction _interactionInput;
        private InputAction _movementInput;
        
        private Transform _weaponSlot;
        private GameObject _currentWeapon;

        private void Awake()
        {
            _playerControls = new PlayerInputActions();
        }
    
        private void Start()
        {
            movement = GetComponent<Movement.Movement>();
            interaction = GetComponentInChildren<Interaction.Interaction>();
        }
    
        private void OnEnable()
        {
            _movementInput = _playerControls.Player.Move;
            _movementInput.Enable();
            
            _interactionInput = _playerControls.Player.Interact;
            _interactionInput.Enable();
        }

        private void OnDisable()
        {
            _movementInput.Disable();
        }

        private void Update()
        {
            Vector2 moveInput = _movementInput.ReadValue<Vector2>(); 
            movement.Move(moveInput); 
            
            // Check for interactions continuously
            interaction.CheckForInteractions();
            
            if (_interactionInput.triggered)
            {
                // Perform interaction when the interact button is pressed
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
        
        public void ChangeWeapon(GameObject newWeaponPrefab)
        {
            GameObject newWeapon = Instantiate(newWeaponPrefab, _weaponSlot.position, _weaponSlot.rotation, _weaponSlot);

            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon);
            }

            _currentWeapon = newWeapon;
        }

        public void BuyItem(Item.ItemType itemType)
        {
            Debug.Log("Bought item " + itemType);
        }
    }
}
