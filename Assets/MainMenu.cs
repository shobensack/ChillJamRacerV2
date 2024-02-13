using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChillRacer
{
    public class MainMenu : MonoBehaviour
    {
        // Public method to be called when the player clicks the "Start Game" button
        public void StartGame()
        {
            // Load the game scene
            SceneManager.LoadScene("DialogueScene");
        }

        // Public method to be called when the player clicks the "Quit Game" button
        public void QuitGame()
        {
            // Quit the application (works in standalone builds)
            Application.Quit();
        }
    }
}