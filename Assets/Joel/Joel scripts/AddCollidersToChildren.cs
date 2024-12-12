using UnityEngine;

public class AddCollidersToChildren : MonoBehaviour
{
    public bool useMeshCollider = false; // Usa Mesh Collider si es true, sino Box Collider

    void Start()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            // Verificar si ya tiene un collider
            if (child.GetComponent<Collider>() == null)
            {
                if (useMeshCollider)
                {
                    child.gameObject.AddComponent<MeshCollider>();
                }
                else
                {
                    child.gameObject.AddComponent<BoxCollider>();
                }

                // Forzar a Unity a recalcular las físicas
                Physics.SyncTransforms();
                Debug.Log($"Collider añadido a: {child.name}");
            }
        }
        Debug.Log("Colliders añadidos a todos los hijos.");
    }
}
