using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;

    [Header("Rotaci�n de C�mara")]
    public float sensibilidad = 2f;

    [Header("Control de Gravedad")]
    public float gravedad = 9.81f;
    public Transform comprobadorSuelo;
    public LayerMask sueloLayer;

    private Vector2 movimientoInput;
    private Vector2 rotacionInput;
    private bool estaSaltando = false;
    private bool estaEnSuelo;

    private Rigidbody rb;
    private Camera cam;

    private float rotacionCamaraX = 0f;

    [Header("Video de animaci�n")]
    public float velociadaRotacion = 200.0f;
    public float x, y;

    [Header("Chatgpt de animaci�n")]
    private Vector2 ultimaDireccion;
    private Animator animator;

    [Header("Configuraciones")]
    public bool usarMouseParaCamara = true; // Interruptor para activar/desactivar el mouse

    [Header("Animaciones y Poses")]
    private Animator animatorPoses;
    private int currentPose = 0; // �ndice de la pose actual (0 = ninguna pose activa)
    private int totalPoses = 5;  // N�mero total de poses disponibles


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;

        animatorPoses = GetComponent<Animator>();
        animator = GetComponent<Animator>(); // Aseg�rate de inicializar este tambi�n.
    }


    void Update()
    {
        // Rotaci�n de la c�mara
        if (usarMouseParaCamara)
        {
            RotarCamara(); // Solo rota la c�mara si el mouse est� activo
        }

        // Detectar si estamos en el suelo
        estaEnSuelo = Physics.CheckSphere(comprobadorSuelo.position, 0.1f, sueloLayer);

        if (movimientoInput != Vector2.zero && currentPose != 0)
        {
            CancelarPose();
        }

        // Actualizar el Animator seg�n el estado de salto
        animator.SetBool("IsJumping", !estaEnSuelo);

        // Saltar si se detecta la entrada y estamos en el suelo
        if (estaSaltando && estaEnSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estaSaltando = false;
        }

        // Movimiento y animaciones
        ActualizarAnimaciones();
    }


    void ActualizarAnimaciones()
    {
        // Usa movimientoInput para calcular las direcciones.
        float velX = Mathf.Lerp(animator.GetFloat("VelX"), movimientoInput.x, Time.deltaTime * 10f);
        float velY = Mathf.Lerp(animator.GetFloat("VelY"), movimientoInput.y, Time.deltaTime * 10f);


        // Detectar cambio brusco de direcci�n (giro 180�).
        bool giro180 = Vector2.Dot(ultimaDireccion, new Vector2(velX, velY)) < -0.5f && velY != 0;

        // Actualizar el Animator con los par�metros de movimiento.

        animator.SetFloat("VelX", velX);
        animator.SetFloat("VelY", velY);
        animator.SetBool("IsTurning", giro180);

        // Actualiza la �ltima direcci�n.
        ultimaDireccion = new Vector2(velX, velY);
    }

    public void ActivarPoseAleatoria()
    {
        if (currentPose == 0) // Solo activa una nueva pose si no hay una activa
        {
            int poseIndex = Random.Range(1, totalPoses + 1); // Generar un �ndice aleatorio (1 a 5)
            currentPose = poseIndex; // Establece la pose actual
            animator.SetInteger("PoseIndex", poseIndex);
        }
    }


    public void CancelarPose()
    {
        if (currentPose != 0) // Solo cancela si hay una pose activa
        {
            currentPose = 0; // Ninguna pose activa
            animator.SetInteger("PoseIndex", 0);
        }
    }



    void FixedUpdate()
    {
        // Movimiento del jugador usando Rigidbody
        Vector3 direccion = transform.forward * movimientoInput.y + transform.right * movimientoInput.x;
        Vector3 velocidadDeseada = direccion.normalized * velocidadMovimiento;

        Vector3 nuevaVelocidad = new Vector3(velocidadDeseada.x, rb.velocity.y, velocidadDeseada.z);
        rb.velocity = nuevaVelocidad;

        // Aplicar gravedad manual si no estamos en el suelo
        if (!estaEnSuelo)
        {
            rb.AddForce(Vector3.down * gravedad, ForceMode.Acceleration);
        }
    }

    void RotarCamara()
    {
        // Rotar el cuerpo del jugador (eje Y)
        transform.Rotate(Vector3.up * rotacionInput.x * sensibilidad);

        // Rotar la c�mara en el eje X (vertical)
        rotacionCamaraX -= rotacionInput.y * sensibilidad;
        rotacionCamaraX = Mathf.Clamp(rotacionCamaraX, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(rotacionCamaraX, 0f, 0f);
    }

    // M�todos conectados al Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (usarMouseParaCamara) // Solo toma los datos del mouse si est� activo
        {
            rotacionInput = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            estaSaltando = true;
        }
    }

    public void OnPoseRandom(InputAction.CallbackContext context)
    {
        if (context.performed) ActivarPoseAleatoria();
    }
}
