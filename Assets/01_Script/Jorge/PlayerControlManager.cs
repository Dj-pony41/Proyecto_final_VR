using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager : MonoBehaviour
{
    [Header("Componentes a gestionar")]
    public List<MonoBehaviour> componentes; // Lista de componentes (ej: controladores, scripts)
    public GameObject camara; // C�mara del cuerpo

    // M�todo para activar los componentes

    void Start()
    {
        // Si este GameObject no es el cuerpo actual del jugador, desactiva sus componentes
        if (!IsControlledByPlayer())
        {
            DesactivarControl();
        }
    }

    // M�todo para verificar si este GameObject est� siendo controlado
    private bool IsControlledByPlayer()
    {
        // Puedes ajustar esta l�gica para determinar si este GameObject es el currentPlayer inicial
        CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
        return checkpointManager != null && checkpointManager.currentPlayer == gameObject;
    }

    public void ActivarControl()
    {
        foreach (MonoBehaviour componente in componentes)
        {
            if (componente != null)
            {
                componente.enabled = true;
            }
        }

        if (camara != null)
        {
            camara.SetActive(true);
        }
    }

    // M�todo para desactivar los componentes
    public void DesactivarControl()
    {
        foreach (MonoBehaviour componente in componentes)
        {
            if (componente != null)
            {
                componente.enabled = false;
            }
        }

        if (camara != null)
        {
            camara.SetActive(false);
        }
    }
}
