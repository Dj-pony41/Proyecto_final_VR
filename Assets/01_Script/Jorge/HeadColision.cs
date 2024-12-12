using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadColision : MonoBehaviour
{
    public int contadorDeColision = 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "NPC") 
        { 
            contadorDeColision++;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "NPC")
        {
            contadorDeColision--;
        }
    }
}
