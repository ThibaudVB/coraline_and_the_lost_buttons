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

    public AudioClip shakeSound;  // Référence au son à jouer pendant le shake

    // Start is called before the first frame update
    void Start()
    {
        camTransform = GetComponent(typeof(Transform)) as Transform;
        initPos = camTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShaking && shakingTimer > 0){
            shakingTimer -= Time.deltaTime;
            camTransform.localPosition = initPos + Random.insideUnitSphere * shakeAmount;
        } else if (isShaking && shakingTimer <= 0){
            isShaking = false;
        }
    }

    public void ShakeCamera(float timer){
        isShaking = true;
        shakingTimer = timer;

        // Appeler la fonction pour jouer le son
        PlayShakeSound();
    }

    private void PlayShakeSound()
    {
        if (shakeSound != null)
        {
            // Joue le son à la position de la caméra
            AudioSource.PlayClipAtPoint(shakeSound, camTransform.position);
        }
    }
}
