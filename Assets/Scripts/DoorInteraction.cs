using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isClosed = false;

    public AudioClip openDoorSound;  // Référence au son à jouer lors de l'ouverture de la porte
    private AudioSource audioSource; // Référence à l'AudioSource attachée à la porte

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Open");
            isOpen = true;  
            animator.SetBool("Open", isOpen); // Active l'animation d'ouverture

            // Joue le son d'ouverture de la porte
            if (openDoorSound != null)
            {
                audioSource.PlayOneShot(openDoorSound);  // Joue le son d'ouverture de porte
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
        // Attends la fin de l'animation Door_Closed avant de réinitialiser le booléen
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("Open", false); // Réinitialise le booléen après la fin de l'animation
        isOpen = false;
        isClosed = false; // Remet l'état fermé à false pour éviter qu'il ne soit réinitialisé de nouveau
    }
}
