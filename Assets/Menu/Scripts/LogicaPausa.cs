using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LogicaPausa : MonoBehaviour
{
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void pausa(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
