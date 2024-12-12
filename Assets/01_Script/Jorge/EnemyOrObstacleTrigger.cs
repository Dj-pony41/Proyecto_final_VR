using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrObstacleTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra es el jugador
        CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
        if (checkpointManager != null && other.gameObject == checkpointManager.currentPlayer)
        {
            Debug.Log("Jugador atrapado por enemigo o trampa.");
            checkpointManager.PlayerCaught(); // Notificar al CheckpointManager que el jugador fue atrapado
        }
    }
}
