using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator; // Asigna el Animator de la puerta
    private bool isOpen = false;

    private Collider physicalCollider; // Collider físico de la puerta
    private Collider interactionCollider; // Collider de interacción

    void Start()
    {
        // Identifica los colliders en el objeto puerta
        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            if (col.isTrigger)
            {
                interactionCollider = col; // Asigna el collider de interacción
            }
            else
            {
                physicalCollider = col; // Asigna el collider físico
            }
        }

        // Asegúrate de que el collider físico esté activo al inicio
        if (physicalCollider != null)
        {
            physicalCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el player entra en el área de interacción
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.HasKey && !isOpen)
            {
                Debug.Log("Llave detectada, abriendo puerta.");
                doorAnimator.SetTrigger("Open"); // Activa la animación de apertura
                isOpen = true;

                // Desactiva el collider físico para permitir el paso
                if (physicalCollider != null)
                {
                    physicalCollider.enabled = false;
                }
            }
            else if (!PlayerInventory.HasKey)
            {
                Debug.Log("Necesitas una llave para abrir esta puerta.");
            }
        }
    }
}
