using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text artefactCountText;   // Référence au Text dans l'UI pour afficher le nombre d'artefacts
    public GameObject door;          // La porte à déverrouiller une fois les 5 artefacts collectés
    public Text messageText;         // Référence au Text pour afficher le message ("Porte déverrouillée")

    void OnEnable()
    {
        // Abonnement à l'événement OnArtefactCollected
        Artefact.OnArtefactCollected += UpdateArtefactCount;
    }

    void OnDisable()
    {
        // Désabonnement de l'événement lors de la destruction du script ou du changement d'objet
        Artefact.OnArtefactCollected -= UpdateArtefactCount;
    }

    // Met à jour l'affichage du nombre d'artefacts collectés
    void UpdateArtefactCount(int count)
    {
        artefactCountText.text = "Artefacts: " + count + "/5";  // Affiche le nombre d'artefacts collectés

        // Si le joueur a collecté tous les artefacts (5), on appelle la méthode de déverrouillage
        if (count >= 5)
        {
            UnlockDoor();
            ShowMessage("Porte déverrouillée !");
        }
    }

    // Déverrouille la porte (la rend visible)
    void UnlockDoor()
    {
        door.SetActive(true);  // La porte devient visible
    }

    // Afficher un message
    void ShowMessage(string message)
    {
        messageText.text = message;
        Invoke("ClearMessage", 2f); // Efface le message après 2 secondes
    }

    // Efface le message
    void ClearMessage()
    {
        messageText.text = "";
    }
}
