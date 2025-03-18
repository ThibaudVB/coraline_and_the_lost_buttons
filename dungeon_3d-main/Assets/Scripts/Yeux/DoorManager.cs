using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door;  // La porte à déverrouiller

    // Méthode pour déverrouiller la porte
    void UnlockDoor()
    {
        // Désactiver la porte pour la déverrouiller
        door.SetActive(false);  // Cela désactive la porte dans la scène
        Debug.Log("Porte déverrouillée !");
    }

    void OnEnable()
    {
        // Abonnement à l'événement OnArtefactCollected
        Artefact.OnArtefactCollected += CheckArtefactsForUnlock;
    }

    void OnDisable()
    {
        // Désabonnement de l'événement lors de la destruction du script ou du changement d'objet
        Artefact.OnArtefactCollected -= CheckArtefactsForUnlock;
    }

    // Vérifie si le joueur a collecté tous les artefacts et déverrouille la porte
    void CheckArtefactsForUnlock(int artefactsCount)
    {
        if (artefactsCount >= 10)
        {
            UnlockDoor();  // Appelle la méthode pour déverrouiller la porte
        }
    }
}
