using UnityEngine;

public class Artefact : MonoBehaviour
{
    public static int artefactsCollected = 0;  // Compteur d'artefacts collectés

    public static event System.Action<int> OnArtefactCollected; // Événement pour notifier la collecte d'artefacts

    // Lorsque le joueur entre en collision avec l'artefact
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))  // Si le joueur entre en collision avec l'artefact
        {
            artefactsCollected++;  // Augmenter le compteur d'artefacts collectés

            // Déclenche l'événement pour notifier les autres scripts
            OnArtefactCollected?.Invoke(artefactsCollected);

            Destroy(gameObject);  // Supprimer l'artefact après sa collecte
        }
    }
}
