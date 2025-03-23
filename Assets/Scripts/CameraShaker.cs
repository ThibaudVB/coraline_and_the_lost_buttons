using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private bool isShaking = false;
    private float shakingTimer = 0;
    private Transform camTransform;
    private Vector3 initPos;
    public float shakeAmount = 0.7f;

    // Suppression de la gestion du son du screamer
    // public AudioClip shakeSound;  // Référence au son à jouer pendant le shake
    // private bool hasPlayedSound = false;  // Variable pour vérifier si le son a déjà été joué

    // Start is called before the first frame update
    void Start()
    {
        camTransform = GetComponent(typeof(Transform)) as Transform;
        initPos = camTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking && shakingTimer > 0)
        {
            shakingTimer -= Time.deltaTime;
            camTransform.localPosition = initPos + Random.insideUnitSphere * shakeAmount;

            // Si le son n'est plus géré ici, on n'a plus besoin de vérifier s'il a été joué
        }
        else if (isShaking && shakingTimer <= 0)
        {
            isShaking = false;
            // Réinitialisation de la gestion du son, mais plus nécessaire ici
            // hasPlayedSound = false;
        }
    }

    public void ShakeCamera(float timer)
    {
        isShaking = true;
        shakingTimer = timer;

        // Pas besoin de jouer de son ici, cette fonction ne gère que le tremblement de la caméra
        // hasPlayedSound = false; // Réinitialisation de la variable pour permettre de jouer le son lors du prochain shake
    }
}
