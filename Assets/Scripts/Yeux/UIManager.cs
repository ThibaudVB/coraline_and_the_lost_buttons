using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text artefactCountText;   // Référence au TMP_Text pour afficher le nombre d'artefacts
    public DoorsLocked doorLocked;  // Référence à DoorsLocked
    public TMP_Text messageText;         // Référence au TMP_Text pour afficher le message

    private Vector3 initialScale;  // Pour garder la taille initiale du texte

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
        // Sauvegarder la taille initiale du texte
        initialScale = artefactCountText.transform.localScale;

        // Affiche 0/5 au début
        artefactCountText.text = Artefact.artefactsCollected + "/5";  // Affichage initial du compteur d'artefacts
    }

    // Met à jour l'affichage du nombre d'artefacts collectés
    void UpdateArtefactCount(int count)
    {
        artefactCountText.text = count + "/5";  // Affiche le nombre d'artefacts collectés

        // Animation du compteur
        StartCoroutine(AnimateArtefactCount());

        // Si le joueur a collecté tous les artefacts (5), on déverrouille la porte
        if (count >= 5)
        {
            doorLocked.UnlockDoor();  // Déverrouille la porte
            ShowMessage("Porte déverrouillée !"); // Affiche le message
        }
    }

    // Coroutine pour animer la taille du texte
    IEnumerator AnimateArtefactCount()
    {
        // Faire grandir rapidement le texte
        Vector3 targetScale = initialScale * 1.5f;
        float duration = 0.2f;  // Temps pour l'agrandissement
        float elapsedTime = 0f;

        // Agrandir le texte
        while (elapsedTime < duration)
        {
            artefactCountText.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Revenir à la taille d'origine
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            artefactCountText.transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurer que la taille finale soit exactement la taille initiale
        artefactCountText.transform.localScale = initialScale;
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

    // Réinitialiser le compteur d'artefacts lorsque le joueur meurt
    public void ResetArtefactCountOnDeath()
    {
        Artefact.ResetArtefactsCollected();  // Réinitialiser le compteur d'artefacts
    }
}
