using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door;  // La porte à déverrouiller

    // Déverrouille la porte
    void UnlockDoor()
    {
        door.SetActive(true);  // La porte devient visible
        Debug.Log("Porte déverrouillée !");
    }
}
