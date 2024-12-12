using UnityEngine;

public class EnemyTurn : MonoBehaviour
{
    public Transform spotlight; // Arrastra el objeto Spotlight aquí
    public float rotationSpeed = 2f; // Velocidad de rotación
    private bool isRotatingRight = true; // Controla la dirección del giro
    private Quaternion leftRotation; // Rotación hacia la izquierda
    private Quaternion rightRotation; // Rotación hacia la derecha

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
        // Determina la rotación objetivo
        Quaternion targetRotation = isRotatingRight ? rightRotation : leftRotation;

        // Suaviza la rotación hacia el objetivo
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // También rota el Spotlight si está asignado
        if (spotlight != null)
        {
            spotlight.rotation = transform.rotation;
        }

        // Comprueba si el enemigo alcanzó la rotación objetivo
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            // Cambia de dirección después de alcanzar el objetivo
            isRotatingRight = !isRotatingRight;
        }
    }
}
