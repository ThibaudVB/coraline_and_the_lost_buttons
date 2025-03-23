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

    public float scareDistance = 0.01f;
    private bool isScaring = false;
    private float stop_distance = 30f;

    // Ajout d'une référence publique à la flashlight
    public Light flashlight;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        screamerObject?.SetActive(false);
    }

    void Update()
    {
        if (!isScaring)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < stop_distance && !HudManager.pause)
            {
                Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= scareDistance)
            {
                TriggerJumpscare();
            }
        }
    }

    void TriggerJumpscare()
    {
        isScaring = true;

        // Éteindre la flashlight lors du jumpscare
        if (flashlight != null)
            flashlight.enabled = false;

        GameObject.FindWithTag("MainCamera").GetComponent<CameraShaker>().ShakeCamera(5f);

        player.GetComponent<PlayerController>().StopMoving(true);
        screamerObject?.SetActive(true);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(Wait(screamerTime));
    }

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
