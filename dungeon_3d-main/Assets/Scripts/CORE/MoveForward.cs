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

    public GameObject screamerObject;

  //private Collider Mcollider;
  //public Vector3 offset;

  public float scareDistance = 0.01f;
  //public Transform jumpscarePosition;
  private bool isScaring = false;

    
    private float stop_distance = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player").transform;
      //recupère component collider
      //Mcollider = GetComponent<Collider>();

      screamerObject?.SetActive(false);
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
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer <= scareDistance)
        {
            TriggerJumpscare();
        }
      }

    }
    //Pour faire le jumscare
    void TriggerJumpscare()
    {
        isScaring = true;
        GameObject.FindWithTag("MainCamera").GetComponent<CameraShaker>().ShakeCamera(5f);
        //Mcollider.enabled = false;
        //Pour arrêter les mouvements du joueur

        player.GetComponent<PlayerController>().StopMoving(true);
        screamerObject?.SetActive(true);
        StartCoroutine(Wait(screamerTime));
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        //Destroy(GetComponent<Rigidbody>());
        //Pour faire regarder le monstre  le joueur
        //Pour déplacer le monstre devant le joueur
        //Vector3 newPosition = Camera.main.transform.position + (Camera.main.transform.forward * distanceFactor + offset);
        //transform.position = newPosition;
      //Pour attendre avant d'envoyer le joueur à l'écran de game over
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