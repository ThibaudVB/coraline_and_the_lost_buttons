using UnityEngine;
using UnityEngine.SceneManagement; // Pour charger les scènes
using UnityEngine.EventSystems; // Pour interagir avec l'UI

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Le panel du menu de pause
    public GameObject optionsMenu; // Le panel du menu d'options
    private bool isPaused = false;

    // Référence à ton script PlayerController
    public PlayerController playerController; // Assure-toi de lier ce script dans l'inspecteur

    void Update()
    {
        // Activation/Désactivation du menu avec Escape
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
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

    public void PauseGame()
    {
        // Active le menu de pause
        pauseMenu.SetActive(true);
        
        // Désactive la possibilité de déplacer la souris
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Désactive les interactions avec l'UI du jeu en arrière-plan
        EventSystem.current.SetSelectedGameObject(null);

        // Désactive le contrôleur du joueur
        if (playerController != null)
        {
            playerController.enabled = false; // Désactive le PlayerController pendant la pause
        }

        // Marque que le jeu est en pause
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Désactive le menu de pause
        pauseMenu.SetActive(false);

        // Reprend le temps du jeu
        Time.timeScale = 1f;

        // Verrouille la souris dans le jeu à nouveau
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Réactive le contrôleur du joueur
        if (playerController != null)
        {
            playerController.enabled = true; // Réactive le PlayerController lorsque le jeu reprend
        }

        // Marque que le jeu n'est plus en pause
        isPaused = false;
    }

    public void QuitGame()
    {
        // Charge la scène MainMenu lorsque le joueur clique sur Quitter
        SceneManager.LoadScene("MainMenu");
    }

    // Fonction pour afficher le menu Options
    public void OpenOptions()
    {
        pauseMenu.SetActive(false); // Désactive le menu de pause
        optionsMenu.SetActive(true); // Active le menu des options
    }

    // Fonction pour revenir au menu de pause
    public void BackToPauseMenu()
    {
        optionsMenu.SetActive(false); // Désactive le menu d'options
        pauseMenu.SetActive(true); // Réactive le menu de pause
    }
}

public class ButtonLogger : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Log lorsque la souris passe sur le bouton
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Le bouton est survolé.");
    }

    // Log lorsque le bouton est cliqué
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Le bouton a été cliqué.");
    }
}
