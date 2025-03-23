using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [Header("Musique Clips")]
    public AudioClip musicA; // Musique pour le MainMenu
    public AudioClip musicB; // Musique pour le Level1
    public AudioClip musicC; // Musique C pour après avoir traversé le trigger

    private AudioSource audioSource; // Le composant AudioSource pour jouer la musique

    void Start()
    {
        // Récupérer le composant AudioSource attaché à l'objet
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Aucun AudioSource trouvé sur l'objet MusicManager");
        }

        // Vérifier la scène active et jouer la musique appropriée
        PlayMusicForCurrentScene();
    }

    void PlayMusicForCurrentScene()
    {
        // Vérifier la scène active
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainMenu")
        {
            // Jouer la musique A (pour MainMenu)
            Debug.Log("Lecture de la musique A");
            PlayMusic(musicA);
        }
        else if (currentScene == "Level1")
        {
            // Jouer la musique B (pour Level1)
            Debug.Log("Lecture de la musique B");
            PlayMusic(musicB);
        }
        else
        {
            // Si la scène n'est ni MainMenu ni Level1, arrêter la musique
            audioSource.Stop();
            Debug.Log("Musique arrêtée");
        }
    }

    void PlayMusic(AudioClip music)
    {
        // Si la musique n'est pas déjà en train de jouer, on la lance
        if (audioSource.clip != music)
        {
            audioSource.clip = music;
            audioSource.Play();
            Debug.Log("Musique jouée : " + music.name);
        }
    }

    // Fonction pour changer la musique lorsqu'un trigger est activé
    public void ChangeMusicToC()
    {
        if (audioSource != null)
        {
            // Arrêter la musique B si elle est en cours de lecture
            if (audioSource.clip == musicB)
            {
                audioSource.Stop();
                Debug.Log("Musique B arrêtée");
            }

            // Vérifier si la musique C est assignée
            if (musicC != null)
            {
                // Jouer la musique C
                audioSource.clip = musicC;
                audioSource.Play();
                Debug.Log("Musique C jouée");
            }
            else
            {
                Debug.LogError("Le clip musicC n'est pas assigné");
            }
        }
    }
}
