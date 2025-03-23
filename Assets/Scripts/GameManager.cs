using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Coucou()
    {
        Debug.Log("Coucou");
    }

    public void HidePlafond()
    {
        // Ajoute ici le comportement de cette fonction
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
