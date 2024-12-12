using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGiro : MonoBehaviour
{
    // Velocidad de giro
    public float velocidadGiro = 180.0f;

    // Tiempo entre giros
    public float tiempoEntreGiros = 2.0f;

    // Estado actual del giro (0: no girar, 1: girar izquierda, 2: girar derecha)
    private int estadoGiro = 0;

    // Ángulo objetivo para el giro
    private float targetAngle = 0.0f;

    // Update es llamado una vez por frame
    void Update()
    {
        // Verifica si es hora de cambiar el estado del giro
        if (estadoGiro != 0 && Time.time % tiempoEntreGiros < Time.deltaTime)
        {
            // Cambia el estado del giro
            estadoGiro = (estadoGiro == 1) ? 2 : 1;
            targetAngle = (estadoGiro == 1) ? -180.0f : 180.0f;
        }

        // Gira el enemigo según el estado actual
        if (estadoGiro != 0)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, velocidadGiro * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            if (Mathf.Abs(angle - targetAngle) < 0.1f)
            {
                estadoGiro = 0;
            }
        }
    }

    // Función para iniciar el giro
    public void IniciarGiro()
    {
        // Inicia el giro
        estadoGiro = 1;
        targetAngle = -180.0f;
    }

    // Función para detener el giro
    public void DetenerGiro()
    {
        // Detiene el giro
        estadoGiro = 0;
    }
}