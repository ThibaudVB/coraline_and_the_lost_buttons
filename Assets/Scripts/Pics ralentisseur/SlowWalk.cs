using UnityEngine;

public class SlowDownPlate : MonoBehaviour
{
    private bool isPlayerOnPlate = false;  // Vérifie si le joueur est sur la plaque
    private float originalSpeed;           // Vitesse originale du joueur
    private float slowdownDuration = 10f;  // Durée totale de l'effet
    private float recoveryDuration = 5f;   // Durée de récupération progressive
    private float timeSpentSlowed = 0f;   // Temps passé sous l'effet de ralentissement
    private bool isRecovering = false;     // Vérifie si la vitesse doit être récupérée progressivement

    public float slowDownFactor = 0.5f;    // Facteur de ralentissement à 90% de réduction
    
    private PlayerController playerController;

    private bool hasSlowedDown = false;  // Flag pour éviter les appels multiples
    private bool hasExited = false;  // Flag pour éviter les doubles appels de OnTriggerExit

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            playerController.SetFactorSpeed(slowDownFactor);
            hasExited = false;
            timeSpentSlowed = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            hasExited = true;
        }
    }

    void Start(){
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }


    void Update(){
        if(hasExited){
            timeSpentSlowed += Time.deltaTime;
            if(timeSpentSlowed > recoveryDuration){
                playerController.SetFactorSpeed(Mathf.Lerp(0.5f, 1, (timeSpentSlowed - recoveryDuration) / (slowdownDuration - recoveryDuration) ));
            } else if (timeSpentSlowed > slowdownDuration){
                hasExited = false;
                 playerController.SetFactorSpeed(1);
            }
        }
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSlowedDown)
        {
            isPlayerOnPlate = true;
            playerController = other.GetComponent<PlayerController>();
            originalSpeed = playerController.Speed;
            playerController.Speed *= slowDownFactor;

            // Log pour vérifier que la vitesse a bien changé
            Debug.Log("Le joueur est entré dans la zone de ralentissement !");
            Debug.Log("Vitesse après ralentissement : " + playerController.Speed);
            
            // Marque que le ralentissement a été appliqué
            hasSlowedDown = true;  

            timeSpentSlowed = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isPlayerOnPlate && other.CompareTag("Player"))
        {
            timeSpentSlowed += Time.deltaTime;

            if (timeSpentSlowed >= slowdownDuration && !isRecovering)
            {
                isRecovering = true;  // Commence à récupérer la vitesse
            }

            if (isRecovering)
            {
                float recoverySpeed = Mathf.Lerp(playerController.Speed, originalSpeed, (timeSpentSlowed - slowdownDuration) / recoveryDuration);
                playerController.Speed = recoverySpeed;

                // Log pour vérifier la progression de la récupération
                Debug.Log("Vitesse pendant la récupération : " + playerController.Speed);

                if (playerController.Speed >= originalSpeed)
                {
                    playerController.Speed = originalSpeed;
                    isRecovering = false;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !hasExited)
        {
            isPlayerOnPlate = false;

            // Marquer la sortie du joueur
            hasExited = true;

            // Permet de réinitialiser le flag après la sortie de la plaque
            hasSlowedDown = false; // Réinitialise le flag
            // Log pour vérifier la sortie de la zone
            Debug.Log("Le joueur a quitté la zone de ralentissement. Vitesse restante : " + playerController.Speed);
        }
    }

    // Reset le flag hasExited après un certain délai ou lors de certains événements, selon tes besoins
    public void ResetExitFlag()
    {
        hasExited = false;
    }
    */
}
