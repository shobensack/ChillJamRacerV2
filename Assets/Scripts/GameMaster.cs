using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChillRacer
{
    public class GameMaster : MonoBehaviour
    {
        public GameObject victoryMenu; // Reference to the Victory Menu UI canvas
        public GameObject gameOverMenu; // Reference to the Game Over Menu UI canvas
        public string nextSceneName = "NextLevel"; // Change this to the name of the next scene or level

        // Assuming you have a reference to the player's Health component
        public Health playerHealth;

        void Start()
        {
            if (playerHealth == null)
            {
                Debug.LogError("Player's Health component not assigned to GameMaster.");
            }

            victoryMenu.SetActive(false);
            gameOverMenu.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Player crossed the finish line, so it's a win
                Debug.Log("You Win!");
                ShowVictoryMenu();
            }
            else if (other.CompareTag("Enemy"))
            {
                // Enemy crossed the finish line, so it's a loss
                Debug.Log("You Lose!");
                GameOver();
            }
        }

        void GameOver()
        {
            if (playerHealth != null && playerHealth.isDead && playerHealth.currentLives < 0)
            {
                Debug.Log("Game Over");
                ShowGameOverMenu();
            }
        }

        void ShowVictoryMenu()
        {
            // Activate the Victory Menu
            if (victoryMenu != null)
            {
                victoryMenu.SetActive(true);
            }

            // You can add additional logic here, like pausing the game or showing dialogue
        }

        void ShowGameOverMenu()
        {
            // Activate the Game Over Menu
            if (gameOverMenu != null)
            {
                gameOverMenu.SetActive(true);
            }

            // You can add additional logic here, like pausing the game or showing dialogue
        }

        void ReloadScene()
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextScene()
        {
            // Load the next scene or level
            SceneManager.LoadScene(nextSceneName);
        }
    }
}