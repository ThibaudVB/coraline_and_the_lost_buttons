using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale; // Stocke la taille de base
    public float scaleFactor = 1.1f; // Facteur d'agrandissement
    public float scaleSpeed = 5f; // Vitesse d'agrandissement et de réduction

    private void Start()
    {
        originalScale = transform.localScale; // Enregistre la taille de base actuelle du bouton
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer enter {gameObject.name} ");
        StopAllCoroutines(); // Arrêter toute animation de mise à l'échelle en cours
        StartCoroutine(ScaleButton(transform.localScale, originalScale * scaleFactor)); // Commence l'agrandissement fluide
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer eexit {gameObject.name} ");

        StopAllCoroutines(); // Arrêter toute animation de mise à l'échelle en cours
        StartCoroutine(ScaleButton(transform.localScale, originalScale)); // Commence la réduction fluide
    }

    // Coroutine pour animer l'échelle
    private IEnumerator ScaleButton(Vector3 fromScale, Vector3 toScale)
    {
        float elapsedTime = 0f; // Temps écoulé depuis le début de l'animation

        while (elapsedTime < 1f)
        {

            transform.localScale = Vector3.Lerp(fromScale, toScale, elapsedTime); // Interpolation linéaire de l'échelle
            elapsedTime += Time.deltaTime * scaleSpeed; // Au gmenter le temps écoulé en fonction de la vitesse
            Debug.Log($"scale {gameObject.name} {elapsedTime} {Time.deltaTime}");
            yield return null; // Attendre la prochaine frame
        }

        transform.localScale = toScale; // S'assurer que l'échelle finale est exactement la valeur désirée
    }
}
