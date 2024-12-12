using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public GameObject gameOverText; // Arrastra aqu� el objeto de texto desde el inspector

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(false); // Aseg�rate de que el texto est� oculto al inicio
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador alcanz� el final. Game Over!");

            // Muestra el texto de Game Over
            if (gameOverText != null)
            {
                gameOverText.SetActive(true);
            }

            // Reinicia la escena despu�s de 3 segundos
            Invoke("RestartScene", 3f);
        }
    }

    void RestartScene()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
