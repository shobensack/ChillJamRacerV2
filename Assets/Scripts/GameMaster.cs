using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject victoryMenu; // Reference to the Victory Menu UI canvas
    public string nextSceneName = "NextLevel"; // Change this to the name of the next scene or level

    void Start()
    {
        // Ensure Victory Menu is initially disabled
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(false);
        }
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
            ReloadScene();
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
