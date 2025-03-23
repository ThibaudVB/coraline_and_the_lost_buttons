using UnityEngine;

public class SnowFinal : MonoBehaviour
{
    public GameObject FINAL; // Le deuxième cube à faire apparaître

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est bien le joueur
        {
            gameObject.SetActive(false); // Cache le premier cube
            if (FINAL != null)
            {
                FINAL.SetActive(true); // Affiche le deuxième cube
            }
        }
    }
}
