using UnityEngine;

namespace Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveAcceleration = 5f;
        [SerializeField] private float maxMoveSpeed = 10f;
        private float currentMoveSpeed = 0f;
        private Rigidbody2D rb;
        private bool isFacingRight = false;
        private Animator animator;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentMoveSpeed = 0f;
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 moveInput)
        {
            // Calculate and apply acceleration and make sure it stays under max
            float acceleration = moveInput.x * moveAcceleration;
            currentMoveSpeed += acceleration * Time.deltaTime;
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, -maxMoveSpeed, maxMoveSpeed);

            // If there's no input, apply deceleration
            if (moveInput.x == 0)
            {
                currentMoveSpeed = 0f;
            }

            // Calculate the velocity and set it on rb
            Vector2 velocity = new Vector2(currentMoveSpeed, rb.velocity.y);
            rb.velocity = velocity;

            // Flip the player character if moving right and not already facing right
            if (moveInput.x > 0 && !isFacingRight)
            {
                Flip();
            }
            // Flip the player character if moving left and not already facing left
            else if (moveInput.x < 0 && isFacingRight)
            {
                Flip();
            }
            
            UpdateAnimator(moveInput);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
        private void UpdateAnimator(Vector2 moveInput)
        {
            // Determine if player is moving
            bool isMoving = Mathf.Abs(moveInput.x) > 0.1f;

            // Set the "isMoving" parameter in the animator if moving
            animator.SetBool(IsMoving, isMoving);
        }
    }
}
