using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("InputManager");
                    instance = obj.AddComponent<InputManager>();
                }
            }
            return instance;
        }
    }

    // Car movement axes
    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; } // Added VerticalAxis property


    // Brake and accelerate buttons
    public bool BrakeButton { get; private set; }
    public bool AccelerateButton { get; private set; }

    void Update()
    {
        // Read car movement axis
        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");
    }
}
