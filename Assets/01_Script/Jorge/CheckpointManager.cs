using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints; // Lista de checkpoints
    private int currentCheckpointIndex = 0; // Índice del checkpoint actual
    public GameObject currentPlayer; // Cuerpo que el jugador está controlando

    public bool enPuntoInicial = true; // Indica si estás en el punto inicial
    public Transform puntoInicial; // Posición del punto inicial

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
        Debug.Log($"Jugador atrapado. enPuntoInicial: {enPuntoInicial}, Checkpoint Actual: {currentCheckpointIndex}");

        if (enPuntoInicial)
        {
            Debug.Log("Volviendo al punto inicial.");
            currentPlayer.transform.position = puntoInicial.position;
            currentPlayer.transform.rotation = puntoInicial.rotation;

            // Restaura los maniquíes del primer checkpoint solo si fue activado
            if (currentCheckpointIndex == 0 && checkpoints.Count > 0 && checkpoints[0].fueActivado)
            {
                checkpoints[0].RestoreManiquies();
                Debug.Log("Maniquíes del punto inicial restaurados.");
            }
        }
        else
        {
            Debug.Log("Intentando usar checkpoint...");
            Checkpoint currentCheckpoint = checkpoints[currentCheckpointIndex];
            GameObject nextManiqui = currentCheckpoint.GetNextManiqui();

            if (nextManiqui != null)
            {
                Debug.Log($"Cambiando al siguiente maniquí: {nextManiqui.name}");
                ChangePlayerControl(nextManiqui);
            }
            else if (currentCheckpointIndex > 0)
            {
                Debug.Log("Sin maniquíes. Retrocediendo al checkpoint anterior.");
                RestoreCheckpoint(currentCheckpointIndex);
                currentCheckpointIndex--;
                ActivateCheckpoint(currentCheckpointIndex);
            }
            else
            {
                Debug.Log("No quedan vidas. Volviendo al punto inicial.");
                enPuntoInicial = true;
                PlayerCaught(); // Llama nuevamente para manejar el punto inicial
            }
        }
    }







    public void ActivateNewCheckpoint(Checkpoint newCheckpoint)
    {
        int newIndex = checkpoints.IndexOf(newCheckpoint);
        Debug.Log($"newIndex: {newIndex}");

        // Forzar la activación incluso si es el primer checkpoint
        if (newIndex >= 0)
        {
            currentCheckpointIndex = newIndex;
            enPuntoInicial = false; // Siempre cambiar a falso al activar un checkpoint
            Debug.Log($"Nuevo checkpoint activado. Índice: {currentCheckpointIndex}, enPuntoInicial: {enPuntoInicial}");
        }
        else
        {
            Debug.LogWarning("No se pudo activar el nuevo checkpoint.");
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
        if (currentPlayer != null)
        {
            Debug.Log($"Desactivando control del jugador actual: {currentPlayer.name}");
            currentPlayer.GetComponent<PlayerControlManager>().DesactivarControl();
            Destroy(currentPlayer);
        }

        currentPlayer = newPlayer;

        if (currentPlayer != null)
        {
            Debug.Log($"Activando control del nuevo jugador: {currentPlayer.name}");
            currentPlayer.GetComponent<PlayerControlManager>().ActivarControl();
        }
    }

}
