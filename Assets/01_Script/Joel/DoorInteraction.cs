using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public Animator doorAnimator; // Asigna el Animator de la puerta
    private bool isOpen = false;

    private Collider physicalCollider; // Collider f�sico de la puerta
    private Collider interactionCollider; // Collider de interacci�n

    void Start()
    {
        // Identifica los colliders en el objeto puerta
        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            if (col.isTrigger)
            {
                interactionCollider = col; // Asigna el collider de interacci�n
            }
            else
            {
                physicalCollider = col; // Asigna el collider f�sico
            }
        }

        // Aseg�rate de que el collider f�sico est� activo al inicio
        if (physicalCollider != null)
        {
            physicalCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el player entra en el �rea de interacci�n
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.HasKey && !isOpen)
            {
                Debug.Log("Llave detectada, abriendo puerta.");
                doorAnimator.SetTrigger("Open"); // Activa la animaci�n de apertura
                isOpen = true;

                // Desactiva el collider f�sico para permitir el paso
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
