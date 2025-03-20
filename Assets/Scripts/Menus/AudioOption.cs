using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    
    private void Start()
    {
        // Charger la valeur sauvegardée du volume (si existante)
        if (PlayerPrefs.HasKey("GameVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("GameVolume");
            AudioListener.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            volumeSlider.value = 1f; // Volume par défaut
        }

        // Ajouter un écouteur pour détecter les changements de volume
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("GameVolume", value); // Sauvegarde du volume
    }
}
