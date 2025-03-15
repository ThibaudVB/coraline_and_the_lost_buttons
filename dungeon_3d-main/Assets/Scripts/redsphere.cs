using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redsphere : MonoBehaviour
{
    public int degats = 20;
    private bool active = true; 
    // Start is called before the first frame update
    void OnTriggerEntery(Collider col)
    {
        if(col.gameObject.tag == "Player" && active){
            HudManager hud = HudManager.instance;
            active= false;
            hud.subPV(degats);
            Debug.Log("Touché"); 
            Debug.Log(degats); 
            Destroy(this.gameObject);
        }
    }

}
