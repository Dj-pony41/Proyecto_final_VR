using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA; // Punto de partida
    public Transform pointB; // Punto de destino
    public float waitTime = 2f; // Tiempo de espera al llegar a un punto
    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentTarget; // El destino actual
    private bool isWaiting = false; // Control de tiempo de espera

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Establecer el primer destino
        currentTarget = pointB;
        agent.SetDestination(currentTarget.position);
    }

    void Update()
    {
        // Actualizar la animación según la velocidad
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        // Comprobar si el agente ha llegado al destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaiting)
        {
            // Iniciar la espera y rotación hacia el próximo destino
            StartCoroutine(WaitAndTurnTowardsTarget());
        }
    }

    private IEnumerator WaitAndTurnTowardsTarget()
    {
        isWaiting = true;
        agent.isStopped = true; // Detener el movimiento del agente

        // Determinar el próximo destino
        Transform nextTarget = (currentTarget == pointB) ? pointA : pointB;

        // Girar hacia el próximo destino
        Vector3 directionToNextTarget = (nextTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToNextTarget.x, 0, directionToNextTarget.z));
        float rotationSpeed = 2f; // Velocidad de rotación ajustable
        float elapsedTime = 0f;

        while (elapsedTime < 1f) // Tiempo de rotación ajustable
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        // Esperar un tiempo antes de moverse
        yield return new WaitForSeconds(waitTime);

        // Cambiar el destino al otro punto
        currentTarget = nextTarget;
        agent.SetDestination(currentTarget.position);
        agent.isStopped = false; // Reanudar el movimiento

        isWaiting = false;
    }
}
