using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isClosed = false;

    public AudioClip openDoorSound;  // Référence au son à jouer lors de l'ouverture de la porte
    private AudioSource audioSource; // Référence à l'AudioSource attachée à la porte
    private bool isSoundPlaying = false; // Booléen pour vérifier si le son est en cours de lecture

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Si l'AudioSource n'est pas déjà attaché, on l'ajoute
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSoundPlaying) // Vérifie si le son n'est pas déjà en cours
        {
            Debug.Log("Open");
            isOpen = true;  
            animator.SetBool("Open", isOpen); // Active l'animation d'ouverture

            // Joue le son d'ouverture de la porte
            if (openDoorSound != null)
            {
                audioSource.PlayOneShot(openDoorSound);  // Joue le son d'ouverture de porte
                isSoundPlaying = true; // Marque que le son est en cours de lecture
            }

            if (!isClosed)
            {
                StartCoroutine(ResetDoorState()); // Démarre la coroutine
                isClosed = true; // S'assure que la porte ne se ferme pas à nouveau par erreur
            }
        }
    }

    private IEnumerator ResetDoorState()
    {
        // Attends que le son soit fini
        yield return new WaitForSeconds(openDoorSound.length); // Attends la durée du son

        // Réinitialiser l'animation après le délai
        animator.SetBool("Open", false); // Réinitialise le booléen après la fin de l'animation
        isOpen = false;
        isClosed = false; // Remet l'état fermé à false pour éviter qu'il ne soit réinitialisé de nouveau

        isSoundPlaying = false; // Permet de rejouer le son lorsque l'on entre dans le trigger à nouveau
    }
}
