using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 5f;

    [Header("Rotación de Cámara")]
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

    [Header("Video de animación")]
    public float velociadaRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    [Header("Chatgpt de animación")]
    private Vector2 ultimaDireccion;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        cam = Camera.main;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Rotación de la cámara
        RotarCamara();

        // Detectar si estamos en el suelo
        estaEnSuelo = Physics.CheckSphere(comprobadorSuelo.position, 0.1f, sueloLayer);

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
        // Calcula la dirección del movimiento
        Vector3 velocidadActual = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float velX = Vector3.Dot(transform.right, velocidadActual.normalized); // Movimiento lateral
        float velY = Vector3.Dot(transform.forward, velocidadActual.normalized); // Movimiento adelante/atrás

        // Detectar cambio brusco de dirección (giro 180°)
        bool giro180 = Vector2.Dot(ultimaDireccion, new Vector2(velX, velY)) < -0.5f && velY != 0;

        // Actualizar el Animator con los parámetros de movimiento
        animator.SetFloat("VelX", velX);
        animator.SetFloat("VelY", velY);
        animator.SetBool("IsTurning", giro180);

        // Actualiza la última dirección
        ultimaDireccion = new Vector2(velX, velY);
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

        // Rotar la cámara en el eje X (vertical)
        rotacionCamaraX -= rotacionInput.y * sensibilidad;
        rotacionCamaraX = Mathf.Clamp(rotacionCamaraX, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(rotacionCamaraX, 0f, 0f);
    }

    // Métodos conectados al Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        movimientoInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotacionInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            estaSaltando = true;
        }
    }
}
