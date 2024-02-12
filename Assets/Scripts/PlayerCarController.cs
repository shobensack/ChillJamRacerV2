using UnityEngine;

namespace ChillRacer
{
    public class PlayerCarController : MonoBehaviour
    {
        private InputManager inputManager;
        private Health playerHealth;
        private Rigidbody2D rb2d; // Rigidbody2D for 2D physics

        // Enum to handle the player's state
        public enum PlayerState { Idle, Driving, Dead };

        [Header("State Information")]
        [Tooltip("The state the player controller is currently in")]
        public PlayerState playerState = PlayerState.Idle;

        public float speed = 5.5f;
        public float turnSpeed = 5.0f;
        public float driveSpeedThreshold = 1.0f;

        private bool canMove = true;

        void Start()
        {
            // Get the InputManager instance
            inputManager = InputManager.Instance;
            playerHealth = GetComponent<Health>();

            // Get the Rigidbody2D component
            rb2d = GetComponent<Rigidbody2D>();

            // Ensure that the Rigidbody2D is attached to the GameObject
            if (rb2d == null)
            {
                Debug.LogError("Rigidbody2D not found on the player GameObject.");
                return;
            }
        }

        void Update()
        {
            if (!canMove)
            {
                return;
            }

            float horizontalInput = inputManager.HorizontalAxis;
            float rotation = -horizontalInput * turnSpeed;

            // Apply rotation to the player's GameObject only when there's horizontal input
            if (!Mathf.Approximately(horizontalInput, 0.0f))
            {
                transform.Rotate(Vector3.forward * rotation);
            }

            float verticalInput = inputManager.VerticalAxis;
            Vector2 movement = new Vector2(0, verticalInput * speed);

            // Convert local rotation to world space before applying movement
            Vector2 rotatedMovement = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * movement;

            // Use Rigidbody2D to move
            rb2d.velocity = rotatedMovement;

            if (playerHealth.isDead)
            {
                playerState = PlayerState.Dead;
                // Additional logic for when the player is dead
                // For example, you might want to disable further movement
            }
            else if (movement.magnitude >= driveSpeedThreshold)
            {
                playerState = PlayerState.Driving;
                // Logic for when the player is driving
            }
            else
            {
                playerState = PlayerState.Idle;
                // Logic for when the player is idle
            }
        }
    }
}
