using UnityEngine;

namespace Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 15f;
        private Rigidbody2D _rb;
        private bool _isFacingRight = false;
        private Animator _animator;
        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector2 moveInput)
        {
            // Calculate the velocity and set it on rb
            var velocity = new Vector2(moveInput.x * moveSpeed, _rb.velocity.y);
            _rb.velocity = velocity;

            // Flip the player if moving direction and not already facing direction
            if (moveInput.x > 0 && !_isFacingRight)
            {
                Flip();
            }
            else if (moveInput.x < 0 && _isFacingRight)
            {
                Flip();
            }
    
            UpdateAnimator(moveInput);
        }

        private void Flip()
        {
            _isFacingRight = !_isFacingRight;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        
        private void UpdateAnimator(Vector2 moveInput)
        {
            // Determine if player is moving
            var isMoving = Mathf.Abs(moveInput.x) > 0.1f;

            // Set the "isMoving" parameter in the animator if moving
            _animator.SetBool(IsMoving, isMoving);
        }
    }
}
