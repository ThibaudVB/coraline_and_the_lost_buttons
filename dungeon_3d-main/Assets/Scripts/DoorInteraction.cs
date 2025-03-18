using System.Collections;  // Ajoute cette ligne
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isClosed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Open");
            isOpen = true;  
            animator.SetBool("Open", isOpen); // Active l'animation d'ouverture
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
