using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;       // Botón para pausar y reanudar
    public Text pauseText;          // Texto de "Juego Pausado"
    private bool isPaused = false;  // Estado de pausa
    public AudioSource backgroundMusic; // Música de fondo

    void Start()
    {
        // Asegúrate de que el botón está asignado
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause); // Añadir listener al botón
        }
        else
        {
            Debug.LogError("PauseButton no está asignado.");
        }

        // Asegúrate de que el texto de pausa está desactivado inicialmente
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("PauseText no está asignado.");
        }
    }

    void TogglePause()
{
    // Cambiar el estado de pausa
    isPaused = !isPaused;

    if (isPaused)
    {
        // Pausar el juego
        Time.timeScale = 0f;
        pauseText.gameObject.SetActive(true); // Mostrar texto de pausa
        
        // Pausar la música
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause(); 
            Debug.Log("Música pausada.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado música de fondo.");
        }

        Debug.Log("Juego pausado.");
    }
    else
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        pauseText.gameObject.SetActive(false); // Ocultar texto de pausa

        // Reanudar la música
        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause(); 
            Debug.Log("Música reanudada.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado música de fondo.");
        }

        Debug.Log("Juego reanudado.");
    }
}

}
