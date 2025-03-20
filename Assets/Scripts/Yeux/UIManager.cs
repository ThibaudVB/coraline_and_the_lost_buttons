using UnityEngine;
using TMPro;  // Ajout du namespace pour TextMeshPro

public class UIManager : MonoBehaviour
{
    public TMP_Text artefactCountText;   // Référence au TMP_Text pour afficher le nombre d'artefacts
    public DoorsLocked doorLocked;  // Référence à DoorsLocked
    public TMP_Text messageText;         // Référence au TMP_Text pour afficher le message

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

    // Méthode Start pour initialiser l'affichage
    void Start()
    {
        // Affiche 0/5 au début
        artefactCountText.text = "0/5";  // Affichage initial du compteur d'artefacts
    }

    // Met à jour l'affichage du nombre d'artefacts collectés
    void UpdateArtefactCount(int count)
    {
        artefactCountText.text = count + "/5";  // Affiche le nombre d'artefacts collectés

        // Si le joueur a collecté tous les artefacts (5), on déverrouille la porte
        if (count >= 5)
        {
            doorLocked.UnlockDoor();  // Déverrouille la porte
            ShowMessage("Porte déverrouillée !"); // Affiche le message
        }
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
