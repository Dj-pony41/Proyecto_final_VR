using UnityEngine;
using UnityEngine.InputSystem;

public class AccionesController : MonoBehaviour
{
	[Header("Configuraciones de Agarre")]
	public float distanciaAgarre = 2f; // Distancia m�xima para agarrar objetos
	public Transform puntoAgarre; // Punto donde el objeto se sujetar�
	public LayerMask capaInteractuable; // Capa para objetos interactuables

	private GameObject objetoActual; // Objeto actualmente agarrado

	private PlayerInput playerInput; // Referencia al sistema de entrada

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();

		// Vincula la acci�n 'Interact' al m�todo correspondiente
		InputAction interactAction = playerInput.actions["Interact"];
		if (interactAction != null)
		{
			interactAction.performed += _ => Interactuar();
		}
		else
		{
			Debug.LogError("La acci�n 'Interact' no est� configurada en el Input Action Asset.");
		}
	}

	void Interactuar()
	{
		if (objetoActual == null)
		{
			IntentarAgarre();
		}
		else
		{
			SoltarObjeto();
		}
	}

	void IntentarAgarre()
	{
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay(ray.origin, ray.direction * distanciaAgarre, Color.red, 1f);

		if (Physics.Raycast(ray, out RaycastHit hit, distanciaAgarre, capaInteractuable))
		{
			Debug.Log($"Objeto detectado: {hit.collider.name}");
			Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
			if (rb != null)
			{
				Debug.Log($"Agarrando objeto: {hit.collider.name}");
				AgarrarObjeto(hit.collider.gameObject, rb); // Aqu� deber�a ejecutarse
			}
			else
			{
				Debug.Log("El objeto detectado no tiene un Rigidbody.");
			}
		}
		else
		{
			Debug.Log("No se detect� ning�n objeto interactuable.");
		}


	}



	void AgarrarObjeto(GameObject objeto, Rigidbody rb)
	{
		objetoActual = objeto;
		rb.isKinematic = true; // Desactiva la f�sica
		objeto.transform.SetParent(puntoAgarre);
		objeto.transform.localPosition = Vector3.zero;
		objeto.transform.localRotation = Quaternion.identity;
		Debug.Log($"Objeto {objeto.name} agarrado y movido al punto de agarre.");
	}


	void SoltarObjeto()
	{
		if (objetoActual != null)
		{
			Rigidbody rb = objetoActual.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.isKinematic = false; // Reactiva la f�sica
			}
			objetoActual.transform.SetParent(null); // Suelta el objeto
			Debug.Log($"Objeto {objetoActual.name} soltado.");
			objetoActual = null;
		}
	}

	private void OnDestroy()
	{
		// Desvincular la acci�n para evitar errores
		InputAction interactAction = playerInput.actions["Interact"];
		if (interactAction != null)
		{
			interactAction.performed -= _ => Interactuar();
		}
	}
}
