using System.Collections;
using UnityEngine;
using TMPro;  // Assure-toi d'utiliser TextMeshPro pour une meilleure gestion du texte

public class DoorsLocked : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isClosed = false;
    public bool isLocked = true; // Variable pour vérifier si la porte est verrouillée (true = verrouillée, false = déverrouillée)
    public string lockedMessage = "Retrouvez tous les artefacts pour ouvrir la porte."; // Message affiché lorsque la porte est verrouillée
    public string openMessage = "La porte est déverrouillée, vous pouvez l'ouvrir."; // Message affiché lorsque la porte est déverrouillée
    public TMP_Text messageText;  // Référence au TMP_Text pour afficher le message dans l'UI

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLocked)
            {
                // Si la porte est verrouillée, affiche le message et démarre la coroutine pour le masquer après 3 secondes
                ShowMessage(lockedMessage);
                StartCoroutine(HideMessageAfterDelay(3f)); // 3 secondes de délai
            }
            else
            {
                // Si la porte est déverrouillée, elle peut s'ouvrir
                ShowMessage(openMessage);
                isOpen = true;
                animator.SetBool("Open", isOpen); // Active l'animation d'ouverture

                // Si la porte n'est pas déjà fermée, commence la coroutine pour la fermer
                if (!isClosed)
                {
                    StartCoroutine(ResetDoorState()); // Démarre la coroutine
                    isClosed = true; // S'assure que la porte ne se ferme pas à nouveau par erreur
                }
            }
        }
    }

    private IEnumerator ResetDoorState()
    {
        // Attend la fin de l'animation Door_Closed avant de réinitialiser le booléen
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("Open", false); // Réinitialise le booléen après la fin de l'animation
        isOpen = false;
        isClosed = false; // Remet l'état fermé à false pour éviter qu'il ne soit réinitialisé de nouveau
    }

    // Méthode pour déverrouiller la porte après la collecte des artefacts
    public void UnlockDoor()
    {
        isLocked = false;  // Déverrouille la porte
        Debug.Log("La porte est déverrouillée, vous pouvez l'ouvrir.");
    }

    // Méthode pour afficher un message dans l'UI
    public void ShowMessage(string message)
    {
        messageText.text = message; // Affiche le message dans l'UI
    }

    // Coroutine pour masquer le message après un certain délai
    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attend le délai spécifié
        messageText.text = ""; // Efface le message
    }
}
