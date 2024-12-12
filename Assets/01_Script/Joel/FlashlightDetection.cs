using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    public LayerMask detectionLayer; // Capa del Player u otros objetos detectables
    public Light spotlight; // Referencia a la luz Spotlight
    public Transform player; // Referencia al Player
    private bool gameOverTriggered = false; // Para evitar múltiples activaciones

    void Update()
    {
        DetectPlayerInLight();
    }

    private void DetectPlayerInLight()
    {
        if (gameOverTriggered) return; // Evita múltiples ejecuciones

        // Calcula la dirección hacia el Player desde la linterna
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Verifica si el Player está dentro del alcance de la luz
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > spotlight.range * 2f) return; // Fuera del rango de la luz

        // Calcula el ángulo entre la dirección de la linterna y el Player
        // Tenemos que considerar la rotación de la cámara
        Vector3 forwardDirection = transform.forward;
        float angleToPlayer = Vector3.Angle(forwardDirection, directionToPlayer);
        if (angleToPlayer > spotlight.spotAngle / 2f * 2f) return; // Fuera del cono de luz

        // Realiza un Raycast para asegurarte de que no hay obstáculos entre la linterna y el Player
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, spotlight.range * 2f, detectionLayer))
        {
            CheckpointManager checkpointManager = FindObjectOfType<CheckpointManager>();
            if (hit.collider.CompareTag("Player"))
            {
                checkpointManager.PlayerCaught();
                Debug.Log("Player detectado dentro del cono de luz!");
                
            }
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverTriggered) return; // Evita que el GameOver se dispare más de una vez

        gameOverTriggered = true;

        // Congela la imagen
        Time.timeScale = 0f;

        // Llama al script de Game Over Manager
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver(); // Activa el Game Over
        }
        else
        {
            Debug.LogError("No se encontró un GameOverManager en la escena.");
        }

        // Espera un segundo antes de reiniciar el juego
        Invoke("ReiniciarJuego", 1f);
    }

}