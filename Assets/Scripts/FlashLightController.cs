using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;       // Référence à la lampe torche
    private bool isOn = false;     // Éteinte par défaut

    public AudioClip flashlightOnSound;  // Son à jouer lorsque la lampe torche est allumée
    public AudioClip flashlightOffSound; // Son à jouer lorsque la lampe torche est éteinte
    private AudioSource audioSource;     // Référence à l'AudioSource pour jouer le son

    void Start()
    {
        // Assure que la lampe torche soit éteinte dès le lancement du jeu
        flashlight.enabled = isOn;

        // Si l'AudioSource n'est pas déjà attachée, on l'ajoute
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Assurez-vous que l'AudioSource est configurée correctement
        audioSource.playOnAwake = false; // Ne pas jouer au démarrage
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Touche F pour activer/désactiver
        {
            isOn = !isOn;
            flashlight.enabled = isOn;

            // Joue le son d'allumage ou d'extinction de la lampe torche
            if (isOn && flashlightOnSound != null)
            {
                audioSource.PlayOneShot(flashlightOnSound); // Joue le son d'allumage
            }
            else if (!isOn && flashlightOffSound != null)
            {
                audioSource.PlayOneShot(flashlightOffSound); // Joue le son d'extinction
            }
        }
    }
}
