using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

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
            animator.SetBool("Open", isOpen); 
        }
    }
}
