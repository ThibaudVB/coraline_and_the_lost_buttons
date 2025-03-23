using System.Collections;
using UnityEngine;

public class IntroCinematicController : MonoBehaviour
{
    [Header("Caméras")]
    public Camera playerCamera;  // Caméra du joueur
    public Camera cinematicCamera;  // Caméra cinématique

    [Header("Paramètres Cinématique")]
    public Transform sprite3DPosition;  // Position où la caméra s'arrête devant le modèle
    public float cameraMoveDuration = 5f;  // Temps que la caméra prend pour se déplacer
    public float cinematicTotalDuration = 8f;  // Durée totale de la cinématique (avec animation)

    [Header("Sprite 3D")]
    public GameObject sprite3DModel;  // Modèle 3D à animer et faire disparaître
    public Animator spriteAnimator;  // Animator pour l'animation du modèle 3D

    [Header("UI Panels")]
    public GameObject pausePanel;    // Référence au Panel Pause
    public GameObject compteurPanel; // Référence au Panel Compteur

    void Start()
    {
        // Initialisation des caméras
        playerCamera.gameObject.SetActive(false);
        cinematicCamera.gameObject.SetActive(true);

        // Désactivation des panels Pause et Compteur au début de la cinématique
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (compteurPanel != null)
            compteurPanel.SetActive(false);

        // Lancer la cinématique
        StartCoroutine(PlayIntroCinematic());
    }

    IEnumerator PlayIntroCinematic()
    {
        // Déplacement de la caméra cinématique vers la position du modèle 3D
        Vector3 startPos = cinematicCamera.transform.position;
        Vector3 endPos = sprite3DPosition.position;

        float elapsed = 0f;
        while (elapsed < cameraMoveDuration)
        {
            cinematicCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsed / cameraMoveDuration);
            elapsed += Time.deltaTime;
            Debug.Log($"Cinematic {cinematicCamera.transform.position}");
            yield return null;
        }

        // S'assurer que la caméra est à la bonne position à la fin du mouvement
        cinematicCamera.transform.position = endPos;

        // Déclenche l'animation du sprite 3D à 5 secondes
        spriteAnimator.SetTrigger("PlayAnimation");

        // Attends jusqu'à la fin de la cinématique
        yield return new WaitForSeconds(cinematicTotalDuration - cameraMoveDuration);

        // Désactive le modèle 3D à la fin de la cinématique
        sprite3DModel.SetActive(false);

        // Réactive le panel Compteur à la fin de la cinématique
        if (compteurPanel != null)
            compteurPanel.SetActive(true);

        // Passe à la caméra du joueur
        cinematicCamera.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }
}
