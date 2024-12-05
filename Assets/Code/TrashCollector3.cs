using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class TrashCollector3 : MonoBehaviour
{
    private GameObject heldTrash = null;  // Objeto de basura que el personaje está sosteniendo

    [SerializeField] private Vector3 holdOffset = new Vector3(0.5f, 0, 0);  // Posición de la basura en la mano
    [SerializeField] private float messageDistance = 1.5f;  // Distancia para mostrar los mensajes

    private bool isNearTrash = false;     // Si está cerca de la basura
    private bool isNearContainer = false; // Si está cerca del contenedor

    private GUIStyle messageStyle; // Estilo del mensaje
    private float scale = 1f; // Escala para animar el texto
    private Color messageColor = Color.white; // Color del mensaje
    private float colorChangeSpeed = 0.5f; // Velocidad de cambio de color

    public HealthBar healthBar;  // Referencia a la barra de salud
    private Vector3 initialPosition;  // Posición inicial de la basura

    // Audio
    [SerializeField] private AudioSource correctSound;  // Sonido para basura correcta
    [SerializeField] private AudioSource incorrectSound;  // Sonido para basura incorrecta

    public GameObject gameOverPanel; // Panel de Game Over
    public GameObject levelCompletePanel; // Panel de nivel completado

    // Variables para gestionar el nivel
    private int totalTrash = 10; // 5 basura inorgánica y 5 basura orgánica
    private int collectedTrash = 0;

    public AudioSource backgroundMusic;  // Arrastra el AudioSource de la música al Inspector


    void Start()
    {
        // Inicializar el estilo del mensaje
        messageStyle = new GUIStyle();
        messageStyle.fontSize = 30;  // Tamaño de la fuente
        messageStyle.normal.textColor = messageColor;  // Color inicial del texto
        messageStyle.alignment = TextAnchor.MiddleCenter;  // Centra el texto
        messageStyle.fontStyle = FontStyle.Bold;  // Estilo negrita
        messageStyle.normal.background = MakeTex(600, 1, new Color(0, 0, 0, 0.5f));  // Fondo translúcido negro

        // Asegurarse de que el panel de nivel completado esté desactivado al inicio
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false); // Desactivar el panel de nivel completado
        }

        // Asegurarse de que el panel de Game Over esté desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Desactivar el panel de Game Over
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash == null)
            {
                TryPickUpTrash();
            }
            else
            {
                TryDropTrash();
            }
        }

        // Chequear si ha recogido toda la basura
        if (collectedTrash >= totalTrash)
        {
            ShowLevelCompletePanel();
        }
    }

    void TryPickUpTrash()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("BasuraOrganica") || collider.CompareTag("BasuraInorganica"))
            {
                heldTrash = collider.gameObject;
                initialPosition = heldTrash.transform.position;
                heldTrash.transform.SetParent(transform);
                heldTrash.transform.localPosition = holdOffset;
                break;
            }
        }
    }

    void TryDropTrash()
    {
        if (heldTrash == null)
        {
            Debug.LogError("No hay basura recogida para soltar.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("ContenedorOrganico") || collider.CompareTag("ContenedorInorganico"))
            {
                string containerName = collider.gameObject.name;
                string trashTag = heldTrash.tag;

                if ((collider.CompareTag("ContenedorOrganico") && trashTag == "BasuraOrganica") ||
                    (collider.CompareTag("ContenedorInorganico") && trashTag == "BasuraInorganica"))
                {
                    heldTrash.transform.SetParent(null);
                    heldTrash.transform.position = collider.transform.position;
                    heldTrash = null;

                    // Incrementar el contador de basura recogida
                    collectedTrash++;

                    // Reproducir sonido de éxito
                    if (correctSound != null)
                    {
                        correctSound.Play();
                    }
                }
                else
                {
                    if (healthBar != null)
                    {
                        healthBar.TakeDamage(10);
                    }
                    else
                    {
                        Debug.LogError("HealthBar no está asignado correctamente.");
                    }

                    heldTrash.transform.position = initialPosition;
                    heldTrash.transform.SetParent(null);
                    heldTrash = null;

                    // Reproducir sonido de error
                    if (incorrectSound != null)
                    {
                        incorrectSound.Play();
                    }
                }
                break;
            }
        }
    }

    // Mostrar el panel de nivel completado
    void ShowLevelCompletePanel()
    {
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
            Debug.Log("Nivel completado");
        }
    }

    // Método para reiniciar el nivel
    public void RetryLevel()
    {
        Time.timeScale = 1f;  // Reanudar el juego
        collectedTrash = 0;
        
        // Desactivar el panel de nivel completado
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        // Aquí podrías reiniciar la escena o resetear el estado del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reinicia la escena actual
    }

    // Método para avanzar al siguiente nivel
    public void NextLevel()
    {
        Time.timeScale = 1f;  // Reanudar el juego
        collectedTrash = 0;

        // Desactivar el panel de nivel completado
        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        // Aquí podrías cargar el siguiente nivel (asegúrate de tener las escenas configuradas)
        SceneManager.LoadScene("NextLevelSceneName");  // Reemplaza con el nombre de tu siguiente escena
    }

    // Crear textura para el fondo del mensaje
    Texture2D MakeTex(int width, int height, Color col)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, col);
            }
        }
        texture.Apply();
        return texture;
    }
}
