using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;  // Référence au Panel Pause
    [SerializeField] private GameObject optionsPanel; // Référence au Panel Options

    private void Start()
    {
        // S'assurer que seul le Panel Pause est actif au démarrage
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);  // Masquer le Panel Pause
        optionsPanel.SetActive(true); // Afficher le Panel Options
    }

    public void BackToPauseMenu()
    {
        optionsPanel.SetActive(false); // Masquer le Panel Options
        pausePanel.SetActive(true);  // Réafficher le Panel Pause
    }
}
