using UnityEngine;
using UnityEngine.SceneManagement; // Permet de charger une nouvelle scène

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // Référence au Panel de pause
    public static bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused); // Active ou désactive le panel pause

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Met le jeu en pause
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Relance le jeu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // S'assurer que le temps reprend
        SceneManager.LoadScene("MainMenu"); // Charge la scène du menu principal
    }
}
