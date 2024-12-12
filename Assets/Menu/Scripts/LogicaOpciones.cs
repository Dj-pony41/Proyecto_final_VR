using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    public Controladoropciones panelOpciones;
    
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<Controladoropciones>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            MostrarOpciones();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void MostrarOpciones(){
        panelOpciones.pantallaOpciones.SetActive(true);
    }

    public void PausarJuego(){
        if(Time.timeScale == 1){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }

    public void pausa(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
