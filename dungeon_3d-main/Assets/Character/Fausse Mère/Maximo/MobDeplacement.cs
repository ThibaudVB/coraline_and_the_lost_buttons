using UnityEngine;

public class MobMovement : MonoBehaviour
{
    private Animator animator;         // Référence à l'Animator pour contrôler les animations
    private Rigidbody rb;              // Référence au Rigidbody pour le mouvement physique du mob
    public float moveSpeed = 3.0f;    // Vitesse de déplacement du mob

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Récupérer la référence du Rigidbody
    }

    void Update()
    {
        // Déplacer le mob en avant en fonction de la direction de son forward
        Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;

        // Appliquer le mouvement au Rigidbody
        rb.MovePosition(transform.position + moveDirection);

        // Gérer l'animation de marche
        if (moveDirection.magnitude > 0)  // Si le mob est en mouvement
        {
            animator.SetBool("IsWalking", true);  // Lancer l'animation de marche
        }
        else
        {
            animator.SetBool("IsWalking", false); // Si le mob est immobile
        }
    }
}
