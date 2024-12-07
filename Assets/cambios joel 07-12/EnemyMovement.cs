using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA; // Punto de partida
    public Transform pointB; // Punto de destino
    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentTarget; // El destino actual

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
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Cambiar el destino al otro punto
            if (currentTarget == pointB)
            {
                currentTarget = pointA;
            }
            else
            {
                currentTarget = pointB;
            }

            // Moverse al nuevo destino
            agent.SetDestination(currentTarget.position);
        }
    }
}
