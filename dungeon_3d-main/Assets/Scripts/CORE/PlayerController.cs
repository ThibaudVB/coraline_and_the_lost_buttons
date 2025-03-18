using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Vitesse en marchant, courant et accroupi
    [SerializeField] private float walk, run, crouchSpeed;

    // Sensibilité de la souris
    private float sensitivity = 500;
    private float speed;

    private bool isMoving = false;
    private bool isRunning = false;
    private bool isCrouching = false;

    private CharacterController cc;

    private float X, Y;

    // Pour les bruits de pas
    [SerializeField] private AudioClip[] sfx_steps;
    private int num_step = 0;
    private float step_timer = 0.0f;
    private float max_step_timer = 0.5f;
    private AudioSource audio_steps;

    // Référence à la caméra
    [SerializeField] private Transform cameraTransform;

    // Hauteur normale et accroupie du joueur
    private float normalHeight;
    private float crouchHeight = 1.0f; // Hauteur du CharacterController lorsqu'accroupi

    private void Start()
    {
        speed = walk;
        cc = GetComponent<CharacterController>();
        audio_steps = GetComponent<AudioSource>();
        cc.enabled = true;

        // Stocker la hauteur normale du CharacterController
        normalHeight = cc.height;

        // Verrouillage du curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!HudManager.pause)
        {
            // Gestion de la rotation de la caméra
            const float MIN_Y = -60.0f;
            const float MAX_Y = 70.0f;

            X += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime);
            Y -= Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime);
            Y = Mathf.Clamp(Y, MIN_Y, MAX_Y);

            transform.rotation = Quaternion.Euler(0, X, 0);
            cameraTransform.localRotation = Quaternion.Euler(Y, 0, 0);

            // Déplacement du joueur
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = transform.forward * vertical;
            Vector3 right = transform.right * horizontal;
            cc.SimpleMove((forward + right) * speed);

            // Gestion de la course
            if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            {
                speed = run;
                isRunning = true;
            }
            else
            {
                isRunning = false;
                speed = walk;
            }

            // Gestion de l'accroupissement (Ctrl)
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Crouch();
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                StandUp();
            }

            // Gestion des bruits de pas
            if (horizontal != 0 || vertical != 0)
            {
                if (step_timer <= 0)
                {
                    audio_steps.clip = sfx_steps[num_step];
                    audio_steps.Play();

                    step_timer = max_step_timer;
                    if (isRunning)
                    {
                        step_timer /= 2;
                    }
                    if (isCrouching)
                    {
                        step_timer *= 1.5f; // Pas plus lents en mode accroupi
                    }

                    num_step = (num_step + 1) % sfx_steps.Length;
                }
                else
                {
                    step_timer -= Time.deltaTime;
                }
            }
            else
            {
                step_timer = 0.1f;
            }
        }
    }

    private void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            cc.height = crouchHeight; // Réduit la hauteur du CharacterController
            speed = crouchSpeed; // Réduit la vitesse
        }
    }

    private void StandUp()
    {
        if (isCrouching)
        {
            isCrouching = false;
            cc.height = normalHeight; // Rétablit la hauteur normale
            speed = walk; // Revient à la vitesse normale
        }
    }
}
