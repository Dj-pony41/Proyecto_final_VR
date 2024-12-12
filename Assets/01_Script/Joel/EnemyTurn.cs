using UnityEngine;

public class EnemyTurn : MonoBehaviour
{
    public Transform spotlight; // Arrastra el objeto Spotlight aqu�
    public float rotationSpeed = 2f; // Velocidad de rotaci�n
    private bool isRotatingRight = true; // Controla la direcci�n del giro
    private Quaternion leftRotation; // Rotaci�n hacia la izquierda
    private Quaternion rightRotation; // Rotaci�n hacia la derecha

    void Start()
    {
        // Calcula las rotaciones iniciales
        leftRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y - 90f, transform.eulerAngles.z);
        rightRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90f, transform.eulerAngles.z);
    }

    void Update()
    {
        RotateEnemy();
    }

    void RotateEnemy()
    {
        // Determina la rotaci�n objetivo
        Quaternion targetRotation = isRotatingRight ? rightRotation : leftRotation;

        // Suaviza la rotaci�n hacia el objetivo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Tambi�n rota el Spotlight si est� asignado
        if (spotlight != null)
        {
            spotlight.rotation = transform.rotation;
        }

        // Comprueba si el enemigo alcanz� la rotaci�n objetivo
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            // Cambia de direcci�n despu�s de alcanzar el objetivo
            isRotatingRight = !isRotatingRight;
        }
    }
}
