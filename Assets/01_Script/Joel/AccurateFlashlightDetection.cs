using UnityEngine;

public class AccurateFlashlightDetection : MonoBehaviour
{
    public LayerMask detectionLayer; // Capa para detectar al jugador
    public Light spotlight; // Referencia al Spotlight
    public Transform player; // Transform del jugador
    public float detectionDelay = 0.1f; // Tiempo entre verificaciones

    private bool isGameOverTriggered = false; // Evita m�ltiples detecciones

    void Start()
    {
        // Verifica la detecci�n cada cierto tiempo
        InvokeRepeating(nameof(CheckDetection), 0f, detectionDelay);
    }

    void CheckDetection()
    {
        if (isGameOverTriggered) return;

        // Calcula la direcci�n hacia el jugador
        Vector3 directionToPlayer = (player.position - spotlight.transform.position).normalized;

        // Verifica si el jugador est� dentro del rango del Spotlight
        float distanceToPlayer = Vector3.Distance(spotlight.transform.position, player.position);
        if (distanceToPlayer > spotlight.range) return; // Fuera del rango

        // Verifica si el jugador est� dentro del �ngulo del Spotlight
        float angleToPlayer = Vector3.Angle(spotlight.transform.forward, directionToPlayer);
        if (angleToPlayer > spotlight.spotAngle / 2f) return; // Fuera del cono de luz

        // Realiza un Raycast para verificar si hay obst�culos entre el jugador y el enemigo
        if (Physics.Raycast(spotlight.transform.position, directionToPlayer, out RaycastHit hit, spotlight.range, detectionLayer))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Jugador detectado por el Spotlight!");
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        if (isGameOverTriggered) return;

        isGameOverTriggered = true;

        // Encuentra el GameOverManager y activa el Game Over
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("No se encontr� un GameOverManager en la escena.");
        }
    }
}
