using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

public AudioSource backgroundMusic;  // Arrastra el AudioSource de la música al Inspector

    // Reinicia el nivel actual
    public void Retry()
    {
        Time.timeScale = 1f; // Reactivar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena

        // Reanudar la música si es necesario
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();  // Reanudar la música
            Debug.Log("Música reanudada");
        }
    }

    // Carga el siguiente nivel
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Reactiva el tiempo si estaba pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
