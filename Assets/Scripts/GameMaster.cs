using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChillRacer
{
    public class GameMaster : MonoBehaviour
    {
        public GameObject victoryMenu; // Reference to the Victory Menu UI canvas
        public GameObject gameOverMenu; // Reference to the Game Over Menu UI canvas

        // Assuming you have a reference to the player's Health component
        public Health playerHealth;
        public GameOverManager gameOverManager;

        void Start()
        {
            if (playerHealth == null)
            {
                Debug.LogError("Player's Health component not assigned to GameMaster.");
            }

            victoryMenu.SetActive(false);
            gameOverMenu.SetActive(false);
        }

        void Update()
        {
            // Check if the player is dead
            if (playerHealth != null && playerHealth.isDead)
            {
                // Player is dead, trigger Game Over
                Debug.Log("Player is Dead!");
                gameOverManager.ShowGameOverMenu(false); // Pass 'false' for defeat
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Player crossed the finish line, so it's a win
                Debug.Log("You Win!");
                gameOverManager.ShowVictoryMenu(true); // Pass 'true' for victory
            }
            else if (other.CompareTag("Enemy"))
            {
                // Enemy crossed the finish line, so it's a loss
                Debug.Log("You Lose!");
                gameOverManager.ShowGameOverMenu(false);
            }
        }
    }
}
