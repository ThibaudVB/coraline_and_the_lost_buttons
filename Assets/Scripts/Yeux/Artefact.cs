using UnityEngine;

public class Artefact : MonoBehaviour
{
    public static int artefactsCollected = 0;  // Compteur d'artefacts collectés
    private bool isCollected = false;          // Flag pour vérifier si l'artefact a déjà été collecté

    public static event System.Action<int> OnArtefactCollected; // Événement pour notifier la collecte d'artefacts

    // Lorsque le joueur entre en collision avec l'artefact
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isCollected)  // Vérifie si c'est le joueur et si l'artefact n'a pas déjà été collecté
        {
            isCollected = true;  // Marque l'artefact comme collecté

            artefactsCollected++;  // Augmente le compteur d'artefacts collectés

            // Déclenche l'événement pour notifier les autres scripts
            OnArtefactCollected?.Invoke(artefactsCollected);

            Destroy(gameObject);  // Supprime l'artefact après sa collecte
        }
    }
}
