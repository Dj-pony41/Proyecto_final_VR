using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]  //en desarrollo para opciones

    [Header("panels")]
    public GameObject mainPanel;
    public GameObject creditos;


    public void abrirPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        creditos.SetActive(false);

        panel.SetActive(true);
    }


    public void continuar()
    {
        Scene currentScene = SceneManager.GetActiveScene();     //carga ka ultima scena
        SceneManager.LoadScene(currentScene.name);
    }


    public void inciar(string scena)  //Revisar las configuraciobnes antes de iniciar la prueba
    {
        SceneManager.LoadScene(scena);

    }


    //salir del juego
    public void salir()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }

    }
}
