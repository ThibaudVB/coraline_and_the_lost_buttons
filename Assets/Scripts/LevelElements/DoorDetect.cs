using UnityEngine;

public class DoorDetect : MonoBehaviour
{
    public GameObject monster; // Référence au monstre à activer
    public MusicManager musicManager; // Référence au MusicManager pour changer la musique

    private void Start()
    {
        if (monster != null)
        {
            monster.SetActive(false); // Désactive le monstre au début
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            if (monster != null)
            {
                monster.SetActive(true); // Active le monstre quand le joueur entre dans la hitbox de la porte
            }

            // Appel à MusicManager pour changer la musique
            if (musicManager != null)
            {
                musicManager.ChangeMusicToC(); // Appelle la méthode pour changer la musique
            }
        }
    }
}
