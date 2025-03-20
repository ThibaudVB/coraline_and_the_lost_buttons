using UnityEngine;
using UnityEngine.SceneManagement; // Importer le système de gestion de scène

public class Victory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            SceneManager.LoadScene("Victory"); // Charger la scène de victoire
        }
    }
}
