using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public GameObject menuPausa;  // Referencia al GameObject del Menu Principal
    public GameObject menuControl;   // Referencia al GameObject del Menu Opciones

    public void continuarJuego(string scena)
    {
        SceneManager.LoadScene(scena);
    }

    public void verControlMando()
    {
        // Desactiva el Menu Pausa (ya es un GameObject)
        menuPausa.SetActive(false);

        // Activa el Menu Opciones (menuControl también es un GameObject)
        menuControl.SetActive(true);
    }

    public void volverMenuPausa(GameObject menuControl)
    {
        // Desactiva el Menu Control
        menuControl.SetActive(false);

        // Activa el Menu Pausa
        menuPausa.SetActive(true);
    }
}
