using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale; // Stocke la taille de base
    public float scaleFactor = 1.1f; // Facteur d'agrandissement

    void Start()
    {
        originalScale = new Vector3(2.5f, 2.5f, 2.5f); // Définit la taille de base
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * scaleFactor; // Agrandir le bouton
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // Revenir à la taille initiale (1.5)
    }
}
