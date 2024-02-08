using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesImageManager : MonoBehaviour
{
    public Image livesImage; // Reference to the UI Image for Lives Display
    public Text livesText; // Reference to the UI Text for displaying lives count
    public Health playerHealth; // Reference to the Health script

    void Start()
    {
        // Find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Get the Health script attached to the player
            playerHealth = player.GetComponent<Health>();
        }

        if (livesImage == null)
        {
            // If the reference is not set, try finding the Image component in children
            livesImage = GetComponentInChildren<Image>();
        }

        if (livesText == null)
        {
            // If the reference is not set, try finding the Text component in children
            livesText = GetComponentInChildren<Text>();
        }

        UpdateLivesDisplay();
    }

    void UpdateLivesDisplay()
    {
        if (playerHealth != null && livesImage != null && livesText != null)
        {
            // Set the sprite for the lives image (assuming you have a sprite assigned)
            // livesImage.sprite = yourSprite;

            // Update the text with the number of lives remaining
            livesText.text = "X " + playerHealth.currentLives.ToString();
        }
    }

    // Call this method whenever lives are updated
    public void OnLivesUpdated()
    {
        UpdateLivesDisplay();
    }
}
