using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ChillRacer
{
    public class GameOverManager : MonoBehaviour
    {
        public GameObject gameOverMenu;
        public GameObject victoryMenu;
        public Text gameOverText;
        public Text victoryText;

        private void Start()
        {
            // Disable the Game Over and Victory menus initially
            gameOverMenu.SetActive(false);
            victoryMenu.SetActive(false);
        }

        public void ShowGameOverMenu(bool victory)
        {
            // Set the Game Over text based on whether it's a victory or defeat
            gameOverText.text = victory ? "Victory!" : "Game Over!";

            // Enable the Game Over menu
            gameOverMenu.SetActive(true);

            // Pause the game when the Game Over menu is active
            Time.timeScale = 0f;
        }

        public void ShowVictoryMenu(bool victory)
        {
            // Set the Victory text based on whether it's a victory or defeat
            victoryText.text = victory ? "Victory!" : "Game Over!";

            // Enable the Victory menu
            victoryMenu.SetActive(true);

            // Pause the game when the Victory menu is active
            Time.timeScale = 0f;
        }

        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextSceneIndex);
        }

        public void RestartGame()
        {
            // Unpause the game before reloading the scene
            Time.timeScale = 1f;

            // Retrieve the current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Reload the current scene
            SceneManager.LoadScene(currentSceneIndex);
        }

        public void MainMenu()
        {
            // Unpause the game before loading the main menu
            Time.timeScale = 1f;

            // Load the main menu scene
            SceneManager.LoadScene("Menu");
        }
    }
}
