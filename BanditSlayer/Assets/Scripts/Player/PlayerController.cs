using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions playerControls;
        [SerializeField] private Movement.Movement movement; 
        public InputAction movementInput;
        public Transform weaponSlot;
        public GameObject currentWeapon;

        private void Awake()
        {
            playerControls = new PlayerInputActions();
        }
    
        private void Start()
        {
            movement = GetComponent<Movement.Movement>();
        }
    
        private void OnEnable()
        {
            movementInput = playerControls.Player.Move;
            movementInput.Enable();
        }

        private void OnDisable()
        {
            movementInput.Disable();
        }

        private void Update()
        {
            Vector2 moveInput = movementInput.ReadValue<Vector2>(); 
            movement.Move(moveInput); 
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
            GameObject newWeapon = Instantiate(newWeaponPrefab, weaponSlot.position, weaponSlot.rotation, weaponSlot);

            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentWeapon = newWeapon;
        }
    }
}
