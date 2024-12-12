using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que colisiona es el player
        {
            Debug.Log("Llave recogida!");
            PlayerInventory.HasKey = true; // Actualiza el inventario del player
            Destroy(gameObject); // Elimina la llave de la escena
        }
    }



}
