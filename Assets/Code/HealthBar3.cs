using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar3 : MonoBehaviour
{
    // Opción 1: Usando Slider
    public Slider healthSlider;  // Arrastra el Slider al Inspector

    // Opción 2: Usando Image
    public Image healthBarFill;  // Arrastra la imagen de relleno al Inspector

    // Configuración general de la barra de salud
    public int maxHealth = 100;  // Salud máxima
    private int currentHealth;   // Salud actual

    // Referencia al panel de Game Over
    public GameObject gameOverPanel;

    // Referencias a los botones del panel
    public Button retryButton;
    public Button quitButton;

    // Referencia al AudioSource que reproduce la música
    public AudioSource backgroundMusic;  // Arrastra el AudioSource de la música al Inspector

    void Start3()
    {
        // Inicializar salud
        currentHealth = maxHealth;

        // Configuración del Slider (Opción 1)
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        // Configuración inicial del relleno de la imagen (Opción 2)
        if (healthBarFill != null)
        {
            UpdateHealthBarImage3();
        }

        // Asegurarse de que el panel de Game Over está desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Asignar los métodos a los botones de reiniciar y salir
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(Retry3);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame3);
        }
    }

    // Método para reducir la salud
    public void TakeDamage3(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar Slider (Opción 1)
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Actualizar relleno de imagen (Opción 2)
        if (healthBarFill != null)
        {
            UpdateHealthBarImage3();
        }

        // Mostrar panel de Game Over si la salud llega a cero
        if (currentHealth <= 0)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f; // Pausar el juego
                Debug.Log("Game Over Panel Activado");

                // Detener la música
                if (backgroundMusic != null)
                {
                    backgroundMusic.Pause();  // Detener la música
                    Debug.Log("Música detenida");
                }
                else
                {
                    Debug.LogWarning("No se encontró el AudioSource");
                }
            }
        }
    }

    // Método para restaurar salud
    public void RestoreHealth3(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar Slider (Opción 1)
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Actualizar relleno de imagen (Opción 2)
        if (healthBarFill != null)
        {
            UpdateHealthBarImage3();
        }
    }

    // Método para actualizar el relleno de la imagen
    private void UpdateHealthBarImage3()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    // Método para reiniciar la escena
    private void Retry3()
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

    // Método para salir del juego
    private void QuitGame3()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Para pruebas en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
