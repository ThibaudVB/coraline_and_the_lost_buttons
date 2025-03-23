using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitToMenu : MonoBehaviour
{
    public Button boutonQuitter;

    void Start()
    {
        boutonQuitter.onClick.AddListener(QuitterVersMenuPrincipal);
    }

    void QuitterVersMenuPrincipal()
    {
        // Charge la scène du Menu Principal (remplacer "MenuPrincipal" par le nom exact de ta scène)
        SceneManager.LoadScene("MenuPrincipal");

        // Rend le curseur visible et déverrouillé
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}