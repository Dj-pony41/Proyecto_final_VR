using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints; // Lista de checkpoints
    private int currentCheckpointIndex = 0; // �ndice del checkpoint actual
    public GameObject currentPlayer; // Cuerpo que el jugador est� controlando

    public bool enPuntoInicial = true; // Indica si est�s en el punto inicial
    public Transform puntoInicial; // Posici�n del punto inicial

    void Start()
    {
        if (enPuntoInicial)
        {
            // Teletransportar al punto inicial
            currentPlayer.transform.position = puntoInicial.position;
            currentPlayer.transform.rotation = puntoInicial.rotation;
        }
        else
        {
            // Activar el primer checkpoint
            ActivateCheckpoint(currentCheckpointIndex);
        }

        // Activar control del jugador principal
        currentPlayer.GetComponent<PlayerControlManager>().ActivarControl();
    }

    public void PlayerCaught()
    {
        if (enPuntoInicial)
        {
            // En el punto inicial, vidas ilimitadas
            Debug.Log("Volviendo al punto inicial.");
            currentPlayer.transform.position = puntoInicial.position;
            currentPlayer.transform.rotation = puntoInicial.rotation;
        }
        else
        {
            // Si est�s en un checkpoint, gestionar las vidas
            Checkpoint currentCheckpoint = checkpoints[currentCheckpointIndex];
            GameObject nextManiqui = currentCheckpoint.GetNextManiqui();

            if (nextManiqui != null)
            {
                // Cambiar al siguiente maniqu�
                ChangePlayerControl(nextManiqui);
            }
            else if (currentCheckpointIndex > 0)
            {
                // Retroceder al checkpoint anterior
                RestoreCheckpoint(currentCheckpointIndex);
                currentCheckpointIndex--;
                ActivateCheckpoint(currentCheckpointIndex);
            }
            else
            {
                Debug.Log("No quedan vidas. Volviendo al punto inicial.");
                enPuntoInicial = true;
                PlayerCaught(); // Teletransportar al punto inicial
            }
        }
    }

    public void ActivateNewCheckpoint(Checkpoint newCheckpoint)
    {
        int newIndex = checkpoints.IndexOf(newCheckpoint);
        if (newIndex >= 0 && newIndex != currentCheckpointIndex)
        {
            currentCheckpointIndex = newIndex;
            enPuntoInicial = false; // Ya no est�s en el punto inicial
            Debug.Log($"Nuevo checkpoint activado: {currentCheckpointIndex}");
        }
    }

    private void ActivateCheckpoint(int index)
    {
        Checkpoint checkpoint = checkpoints[index];
        currentPlayer.transform.position = checkpoint.spawnPoint.position;
        currentPlayer.transform.rotation = checkpoint.spawnPoint.rotation;
    }

    private void RestoreCheckpoint(int index)
    {
        Checkpoint checkpoint = checkpoints[index];
        checkpoint.RestoreManiquies();
    }

    private void ChangePlayerControl(GameObject newPlayer)
    {
        currentPlayer.GetComponent<PlayerControlManager>().DesactivarControl();
        currentPlayer = newPlayer;
        currentPlayer.GetComponent<PlayerControlManager>().ActivarControl();
    }
}
