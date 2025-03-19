using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveForward : MonoBehaviour
{
    private Transform player;
    public float speed = 0.1f;
    public float screamerTime = 1.5f;
    public float distanceFactor = 0.5f;

  private Collider Mcollider;
  public Vector3 offset;

  public float scareDistance = 1.5f;
  public Transform jumpscarePosition;
  private bool isScaring = false;

    
    private float stop_distance = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player").transform;
      //recupère component collider
      Mcollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

      //Si le jumpscare est pas activé on calcule la distance entre le monstre et le player et on fait le jumscare si la distance est inférieur à scareDistance
      if (!isScaring)
      {
        //Si la distance entre le joueur et l'ennemi est inférieur à stop_distance et qu'on n'est pas en pause
        if(Vector3.Distance(player.transform.position, transform.position) < stop_distance && !HudManager.pause){
          Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
          transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        }

        //Calculer la distance entre le monstre et le joueur pour lancer le jumpscare
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= scareDistance)
        {
            TriggerJumpscare();
        }
      }
      else{
        
        transform.LookAt(jumpscarePosition);

      }

    }
    //Pour faire le jumscare
    void TriggerJumpscare()
    {
        isScaring = true;
        Mcollider.enabled = false;
        //Pour arrêter les mouvements du joueur
        player.GetComponent<PlayerController>().StopMoving(true);
        Destroy(GetComponent<Rigidbody>());
                //Pour déplacer le monstre devant le joueur
        Vector3 newPosition = Camera.main.transform.position + (Camera.main.transform.forward * distanceFactor + offset);
        transform.position = newPosition;
        //Pour faire regarder le monstre vers le joueur
      //Pour attendre avant d'envoyer le joueur à l'écran de game over
        StartCoroutine(Wait(screamerTime));
    }

  //Pour finir la partie avec l'écran de game over
    void GameOver()
    {
        Debug.Log("Le monstre t'a attrapé !");
        SceneManager.LoadScene("GameOver");

    }

  IEnumerator Wait(float time)
  {
    yield return new WaitForSeconds(time);
    GameOver();
  }

}