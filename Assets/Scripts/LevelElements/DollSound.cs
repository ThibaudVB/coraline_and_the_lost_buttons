using UnityEngine;

public class DollScream : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Récupère l'AudioSource
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            if (!audioSource.isPlaying) // Évite de rejouer en boucle
            {
                audioSource.Play();
            }
        }
    }
}
