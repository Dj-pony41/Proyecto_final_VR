using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio : MonoBehaviour
{
    public float sensitivity = 0.1f; public float smoothSpeed = 5f; public Vector3 customCenterRotation;
    private Quaternion initialCameraRotation;
    void Start()
    {
        if (SystemInfo.supportsGyroscope) 
        {
            Input.gyro.enabled = true;
            initialCameraRotation = transform.rotation;
        }
        else
        {
            Debug.LogWarning("Giroscopio no soportado en este dispositivo."); 
        }
    }

    //void Update()
    //{
    //    if (Input.gyro.enabled)
    //    {
    //        //Quaternion deviceRotation = Input.gyro.attitude;
    //        //deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

    //        //Cambio sugerido:
    //        Quaternion deviceRotation = Input.gyro.attitude;
    //        deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);


    //        Quaternion targetRotation = initialCameraRotation * deviceRotation;

    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    //    }
    //}


    void Update()
    {
        if (Input.gyro.enabled)
        {
            // Obtener la rotación del giroscopio
            Quaternion deviceRotation = Input.gyro.attitude;

            // Aplicar sensibilidad antes de ajustar la rotación (multiplicamos por sensibilidad)
            deviceRotation = Quaternion.Euler(deviceRotation.eulerAngles.x * sensitivity,
                                               deviceRotation.eulerAngles.y * sensitivity,
                                               deviceRotation.eulerAngles.z * sensitivity);

            // Ajustar la orientación a la de Unity
            deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

            // Calcular la rotación final con respecto a la rotación inicial
            Quaternion targetRotation = initialCameraRotation * deviceRotation;

            // Interpolar suavemente entre la rotación actual y la deseada
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }


    public void CenterToCustomRotation()
    {
        Quaternion customRotation = Quaternion.Euler(customCenterRotation);
        transform.rotation = customRotation;
    }
}