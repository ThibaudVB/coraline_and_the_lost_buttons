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
    public Button creditsButton;  // Nouveau bouton pour les crédits
    public Button quitCreditsButton;  // Nouveau bouton pour quitter les crédits

    public GameObject menuPanel;    // Référence au panel Menu
    public GameObject optionsPanel; // Référence au panel Options
    public GameObject creditsPanel; // Référence au panel Crédits

    void Start()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        optionButton.onClick.AddListener(OptionMenu);
        creditsButton.onClick.AddListener(OpenCredits);  // Ajout du listener pour le bouton crédits
        quitCreditsButton.onClick.AddListener(CloseCredits);  // Ajout du listener pour quitter le panel Crédits

        menuPanel.SetActive(true);  // On affiche le menu au début
        optionsPanel.SetActive(false); // On cache le panel Options au début
        creditsPanel.SetActive(false); // On cache le panel Crédits au début
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

    public void OpenCredits()  // Méthode pour afficher les crédits
    {
        Debug.Log("Ouverture des crédits...");
        menuPanel.SetActive(false); // Cache le panel Menu
        creditsPanel.SetActive(true); // Affiche le panel Crédits
    }

    public void CloseCredits()  // Méthode pour fermer les crédits et revenir au menu principal
    {
        Debug.Log("Fermeture des crédits...");
        creditsPanel.SetActive(false); // Cache le panel Crédits
        menuPanel.SetActive(true); // Réaffiche le panel Menu principal
    }
}
