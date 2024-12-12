using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLigth : MonoBehaviour
{
    public Light miluz;
    void Update()
    {
        if(Input.GetKeyDown("f")){
            miluz.enabled = !miluz.enabled;
        }
    }
}
