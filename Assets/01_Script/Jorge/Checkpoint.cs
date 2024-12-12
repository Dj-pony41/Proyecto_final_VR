using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<GameObject> maniquies; // Lista de maniquíes asociados a este checkpoint
    public Transform spawnPoint; // Punto donde aparecerá el jugador al activar este checkpoint
    private List<GameObject> maniquiesOriginales; // Copia para restaurar los maniquíes al retroceder

    void Start()
    {
        // Crear una copia de los maniquíes originales para restaurarlos más tarde
        maniquiesOriginales = new List<GameObject>();
        foreach (GameObject maniqui in maniquies)
        {
            if (maniqui != null)
            {
                maniquiesOriginales.Add(Instantiate(maniqui, maniqui.transform.position, maniqui.transform.rotation, transform));
                maniquiesOriginales[maniquiesOriginales.Count - 1].SetActive(false); // Desactivar los originales
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
        if (checkpointManager != null && other.gameObject == checkpointManager.currentPlayer)
        {
            checkpointManager.ActivateNewCheckpoint(this);
        }
    }


    public GameObject GetNextManiqui()
    {
        // Devuelve el siguiente maniquí disponible y lo elimina de la lista
        if (maniquies.Count > 0)
        {
            GameObject nextManiqui = maniquies[0];
            maniquies.RemoveAt(0);
            return nextManiqui;
        }
        return null; // No quedan maniquíes
    }

    public void RestoreManiquies()
    {
        // Restaurar los maniquíes originales
        foreach (GameObject maniqui in maniquiesOriginales)
        {
            if (maniqui != null)
            {
                GameObject restored = Instantiate(maniqui, maniqui.transform.position, maniqui.transform.rotation, transform);
                restored.SetActive(true);
                maniquies.Add(restored);
            }
        }
    }
}