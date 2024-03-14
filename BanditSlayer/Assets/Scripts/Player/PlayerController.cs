using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputActions playerControls;
        [SerializeField] private Movement.Movement movement; 
        public InputAction movementInput;

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
    }
}
