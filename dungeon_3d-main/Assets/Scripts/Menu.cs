using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button optionButton;

    public GameObject menuPanel;    // Référence au panel Menu
    public GameObject optionsPanel; // Référence au panel Options

    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        optionButton.onClick.AddListener(OptionMenu);

        menuPanel.SetActive(true);  // On affiche le menu au début
        optionsPanel.SetActive(false); // On cache le panel Options au début
    }

    public void StartGame()
    {
        Debug.Log("Démarrer le jeu...");
        SceneManager.LoadScene("Level1"); // Remplace par le vrai nom de ta scène
    }

    public void QuitGame()
    {
        Debug.Log("Arrêter le jeu...");
        Application.Quit();
    }

    public void OptionMenu()
    {
        Debug.Log("Affichage du menu des options...");
        menuPanel.SetActive(false); // Cache le panel Menu
        optionsPanel.SetActive(true); // Affiche le panel Options
    }

    public void CloseOptions()
    {
        Debug.Log("Fermeture du menu des options...");
        optionsPanel.SetActive(false); // Cache le panel Options
        menuPanel.SetActive(true); // Réaffiche le panel Menu
    }
}
