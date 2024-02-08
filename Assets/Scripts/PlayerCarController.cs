using UnityEngine;

public class PlayerCarController : MonoBehaviour
{

    // The input manager to read input from
    private InputManager inputManager;
    // the character controller used for player motion
    private CharacterController characterController;
    private Health playerHealth;

    // Enum to handle the player's state
    public enum PlayerState { Idle, Driving, Dead };

    [Header("State Information")]
    [Tooltip("The state the player controller is currently in")]
    public PlayerState playerState = PlayerState.Idle;

    public float speed = 5.5f;
    public float sensitivity = 2f;
    public float turnSpeed = 5.0f; // Adjust the turn speed as needed
    public float driveSpeedThreshold = 1.0f;
    private float currentSpeed;
    private float speedBoostEndTime;
    CharacterController player;

    public GameObject mainCam;

    float rightCharacterMovement;
    float leftCharacterMovement;

    float rotX;
    float rotY;
    bool canMove = true; // A flag to control player movement

    // Use this for initialization
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerHealth = GetComponent<Health>();
        currentSpeed = speed;

        // Get the InputManager instance
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }

        rightCharacterMovement = inputManager.VerticalAxis * speed;
        leftCharacterMovement = inputManager.HorizontalAxis * speed;

        float horizontalInput = inputManager.HorizontalAxis;
        float rotationAngle = horizontalInput * turnSpeed;
        transform.Rotate(Vector3.up * rotationAngle);

        Vector3 movement = new Vector3(leftCharacterMovement, 0, rightCharacterMovement);
        mainCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        movement.y -= 10f * Time.deltaTime;

        if (playerHealth.isDead)
        {
            playerState = PlayerState.Dead;
            player.Move(Vector3.zero);
        }
        else if (movement.magnitude >= driveSpeedThreshold)
        {
            playerState = PlayerState.Driving;
        }
        else if (movement.magnitude <= driveSpeedThreshold)
        {
            playerState = PlayerState.Idle;
        }
    }
}
