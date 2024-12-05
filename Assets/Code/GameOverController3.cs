using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverImage; // Imagen de "Game Over"

    public void ShowGameOver()
{
    if (gameOverImage != null)
    {
        // Activa la imagen
        gameOverImage.SetActive(true);

        // Ajusta la posición de la imagen en la pantalla
        RectTransform rectTransform = gameOverImage.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(0.73f, 2.09f); // Posición en coordenadas locales
        }
        else
        {
            Debug.LogWarning("El objeto asignado a 'gameOverImage' no tiene un RectTransform.");
        }

        Debug.Log("Panel de Game Over Activado");

        // Pausar el juego
        Time.timeScale = 0f;
    }
    else
    {
        Debug.LogWarning("Game Over Image no está asignada en el Inspector.");
    }
}


    // Método para reiniciar la escena
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Reactivar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Cierra la aplicación (no funciona en el editor de Unity)
    }
}