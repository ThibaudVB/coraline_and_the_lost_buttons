using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;  // Référence au joueur que la caméra suit
    public float distance = 5f;  // Distance entre la caméra et le joueur
    public float smooth = 10f;  // Lissage pour un mouvement plus fluide

    private Vector3 velocity = Vector3.zero;  // Vitesse pour le lissage

    void Update()
    {
        // Position désirée de la caméra derrière le joueur à une certaine distance
        Vector3 desiredPosition = player.position - player.forward * distance;

        // Utilisation d'un Raycast pour vérifier si un mur ou un obstacle est devant la caméra
        RaycastHit hit;
        if (Physics.Raycast(player.position, -player.forward, out hit, distance))
        {
            // Si un obstacle est détecté, la caméra se place à l'endroit où le raycast a frappé
            desiredPosition = hit.point;
        }

        // Lissage du mouvement de la caméra pour éviter des déplacements brusques
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smooth * Time.deltaTime);
    }
}
