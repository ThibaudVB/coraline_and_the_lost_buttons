using System.Collections;
using UnityEngine;
using TMPro; // Pour utiliser TextMeshPro

public class SoundTrigger : MonoBehaviour
{
    public AudioClip soundClip;  // Son à jouer
    public string subtitleText;  // Sous-titre à afficher
    public float subtitleDuration = 5f;  // Durée d'affichage du sous-titre
    public TMP_Text subtitleUI;  // Référence au TMP_Text pour afficher le sous-titre

    private bool hasPlayed = false;  // Pour vérifier si le son a déjà été joué

    private void OnTriggerEnter(Collider other)
    {
        // Si c'est le joueur qui entre dans la zone et que le son n'a pas encore été joué
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;  // Marque l'objet comme joué

            // Joue le son
            if (soundClip != null)
            {
                AudioSource.PlayClipAtPoint(soundClip, transform.position);
            }

            // Affiche le sous-titre
            if (subtitleUI != null)
            {
                subtitleUI.text = subtitleText;
            }

            // Démarre la coroutine pour gérer la durée du sous-titre
            StartCoroutine(PlaySubtitleForDuration());

            // Détruit l'objet après l'activation
            Destroy(gameObject, soundClip.length);  // L'objet sera détruit après la durée du son
        }
    }

    private IEnumerator PlaySubtitleForDuration()
    {
        // Attends que le son soit joué pendant la durée donnée
        yield return new WaitForSeconds(soundClip.length);  // Attend la durée du son
        subtitleUI.text = "";  // Efface le sous-titre après la durée du son
    }
}
