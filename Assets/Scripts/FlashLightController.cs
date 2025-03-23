using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;       // Référence à la lampe torche
    private bool isOn = false;     // Éteinte par défaut

    void Start()
    {
        // Assure que la lampe torche soit éteinte dès le lancement du jeu
        flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Touche F pour activer/désactiver
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
