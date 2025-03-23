using UnityEngine;

public class Artefact : MonoBehaviour
{
    public static int artefactsCollected = 0;  // Compteur d'artefacts collectés
    private bool isCollected = false;          // Flag pour vérifier si l'artefact a déjà été collecté

    public static event System.Action<int> OnArtefactCollected; // Événement pour notifier la collecte d'artefacts

    public AudioClip collectSound;  // Référence au son à jouer lors de la collecte
    private AudioSource audioSource; // Référence à l'AudioSource attachée à l'artefact

    public AudioClip proximitySound;  // Référence au son de proximité
    private AudioSource proximityAudioSource; // Référence à l'AudioSource pour le son de proximité

    public float maxDistance = 10f; // Distance maximale pour entendre le son de proximité
    public float minVolume = 0.1f;  // Volume minimum du son de proximité
    public float maxVolume = 1f;    // Volume maximum du son de proximité

    private Transform player;  // Référence au joueur pour calculer la distance

    void Start()
    {
        // Si l'artefact n'a pas déjà une AudioSource, on en ajoute une
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Si l'artefact n'a pas déjà une AudioSource pour la proximité, on en ajoute une
        if (GetComponent<AudioSource>() == null)
        {
            proximityAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            proximityAudioSource = GetComponent<AudioSource>();
        }

        // Assurez-vous que les AudioSources sont configurées correctement
        audioSource.playOnAwake = false; // Ne pas jouer au démarrage
        proximityAudioSource.playOnAwake = false; // Ne pas jouer au démarrage
        proximityAudioSource.loop = true;  // Le son de proximité doit être en boucle
        proximityAudioSource.clip = proximitySound;  // Affecte le clip de son de proximité à l'AudioSource
        proximityAudioSource.volume = 0;  // Commence avec un volume nul

        // Référence au joueur
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (proximityAudioSource != null && player != null)
        {
            // Calcul de la distance entre l'artefact et le joueur
            float distance = Vector3.Distance(player.position, transform.position);

            // Ajuster le volume du son de proximité en fonction de la distance
            float volume = Mathf.Clamp01((maxDistance - distance) / maxDistance);
            volume = Mathf.Lerp(minVolume, maxVolume, volume); // Ajuste le volume entre minVolume et maxVolume

            proximityAudioSource.volume = volume; // Applique le volume calculé

            // Si la distance est inférieure à la distance maximale et que le son n'est pas déjà joué, on le lance
            if (volume > 0 && !proximityAudioSource.isPlaying)
            {
                proximityAudioSource.Play();  // Joue le son de proximité
            }
            // Si le joueur est trop loin, arrête le son de proximité
            else if (volume == 0 && proximityAudioSource.isPlaying)
            {
                proximityAudioSource.Stop(); // Arrête le son de proximité
            }
        }
    }

    // Lorsque le joueur entre en collision avec l'artefact
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isCollected)  // Vérifie si c'est le joueur et si l'artefact n'a pas déjà été collecté
        {
            isCollected = true;  // Marque l'artefact comme collecté

            artefactsCollected++;  // Augmente le compteur d'artefacts collectés

            // Joue le son de collecte
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);  // Joue le son de collecte
            }

            // Déclenche l'événement pour notifier les autres scripts
            OnArtefactCollected?.Invoke(artefactsCollected);

            Destroy(gameObject, collectSound.length);  // Supprime l'artefact après la durée du son
        }
    }

    // Méthode pour réinitialiser le compteur d'artefacts
    public static void ResetArtefactsCollected()
    {
        artefactsCollected = 0;  // Réinitialiser le compteur d'artefacts collectés
        OnArtefactCollected?.Invoke(artefactsCollected);  // Notifier les autres scripts que le compteur a été réinitialisé
    }
}
