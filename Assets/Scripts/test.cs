using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int age=5; 
    public string test_name="chaise";
    public float speed=5.2f; 
    public bool isVrai= true; 
    public GameObject cube; 
    // Start is called before the first frame update
    void Start()
    {
        cube.transform.position = new Vector3(3,3,3); 
        cube.transform.localScale = new Vector3(3,3,3);

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
