using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu UI Panel
    [SerializeField] private GameObject pauseMenuUI;

    MySceneManager sceneManager;
    // To track the game's paused state
    private bool isPaused = false;

    private void Update()
    {
        // Toggle pause on pressing the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Debug.Log("esc key pressed");
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Function to resume the game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f;          // Resume normal time
        isPaused = false;
    }

    // Function to pause the game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f;         // Freeze time (pause game)
        isPaused = true;
    }

    // Function to restart the current scene
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Reset time scale to default before restarting
        MySceneManager.Instance.GetCurrentScene();
    }

    // Function to quit to the main menu (or exit game)
    public void QuitGame()
    {
        Time.timeScale = 1f;  // Reset time scale to default before quitting
        // If you have a main menu scene, load it here
        MySceneManager.Instance.OpenMenuScene();

        // If you want to quit the application directly:
        // Application.Quit(); 
    }
}
