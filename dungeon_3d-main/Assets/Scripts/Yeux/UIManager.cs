using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text artefactCountText;   // Référence au Text dans l'UI pour afficher le nombre d'artefacts
    public GameObject door;          // La porte à déverrouiller une fois les 10 artefacts collectés

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
        artefactCountText.text = "Artefacts: " + count + "/10";  // Affiche le nombre d'artefacts collectés

        // Si le joueur a collecté tous les artefacts (10), on appelle la méthode de déverrouillage
        if (count >= 10)
        {
            UnlockDoor();
        }
    }

    // Déverrouille la porte (ou effectue toute autre action nécessaire)
    void UnlockDoor()
    {
        door.SetActive(false);  // Désactive la porte pour qu'elle se déverrouille
        Debug.Log("Porte déverrouillée !");
    }
}
