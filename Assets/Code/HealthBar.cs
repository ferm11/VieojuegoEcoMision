using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
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
    public Transform player; // Referencia al jugador

    void Start()
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
            UpdateHealthBarImage();
        }

        // Asegurarse de que el panel de Game Over está desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Asignar los métodos a los botones de reiniciar y salir
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(Retry);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void TakeDamage(int damage)
{
    // Reducir la salud
    currentHealth -= damage;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    // Actualizar barra de salud
    if (healthSlider != null)
        healthSlider.value = currentHealth;

    if (healthBarFill != null)
        UpdateHealthBarImage();

    // Verificar si la salud llegó a cero
    if (currentHealth <= 0)
    {
        ActivateGameOverPanel();
    }
}

private void ActivateGameOverPanel()
{
    if (gameOverPanel != null)
    {
        // Activar el panel de Game Over
        gameOverPanel.SetActive(true);

        // Pausar el juego
        Time.timeScale = 0f;
        Debug.Log("Game Over Panel Activado");

        // Detener la música
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
            Debug.Log("Música detenida");
        }
        else
        {
            Debug.LogWarning("No se encontró el AudioSource");
        }

        // Centrar el panel en la pantalla (Canvas)
        RectTransform rectTransform = gameOverPanel.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero; // Centrar en el Canvas
            rectTransform.sizeDelta = new Vector2(400, 300);
            // Imprimir la posición del panel
            Debug.Log($"Posición actual del panel de Game Over: {rectTransform.anchoredPosition}");
        }
        else
        {
            Debug.LogWarning("El panel de Game Over no tiene un RectTransform.");
        }
    }
    else
    {
        Debug.LogWarning("Game Over Panel no está asignado en el Inspector.");
    }
}


    // Método para restaurar salud
    public void RestoreHealth(int amount)
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
            UpdateHealthBarImage();
        }
    }

    // Método para actualizar el relleno de la imagen
    private void UpdateHealthBarImage()
    {
        healthBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    // Método para reiniciar la escena
    private void Retry()
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
    private void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Para pruebas en el editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
