using UnityEngine;

public class FlashlightFlicker : MonoBehaviour
{
    public Light flashlight;

    [Range(0f, 1f)]
    public float intensityVariation = 0.2f; // Variation par rapport à l'intensité initiale
    public float flickerSpeed = 0.07f;

    private float initialIntensity;
    private float minIntensity;
    private float maxIntensity;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();

        // Récupération de l'intensité initiale directement depuis la lampe torche
        initialIntensity = flashlight.intensity;

        // Calcul automatique des valeurs min et max à partir de l'intensité initiale
        minIntensity = initialIntensity - intensityVariation;
        maxIntensity = initialIntensity + intensityVariation;

        InvokeRepeating(nameof(Flicker), 0, flickerSpeed);
    }

    void Flicker()
    {
        flashlight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
