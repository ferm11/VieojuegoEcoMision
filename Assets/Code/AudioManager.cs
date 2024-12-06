using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] sceneAudioClips; // Clips de audio para las escenas
    private AudioSource audioSource; // Componente AudioSource para reproducir música

    void Awake()
    {
        // Verificar si ya existe una instancia del AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Asegura que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe, destruir este objeto
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayAudioForCurrentScene(); // Reproducir el audio adecuado al inicio
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de cambio de escena
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayAudioForCurrentScene(); // Cambiar la música al cargar una nueva escena
    }

    void PlayAudioForCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // Obtener el índice de la escena actual

        // Si hay un clip de audio asignado para esta escena, reproducirlo si no es el mismo
        if (sceneAudioClips.Length > sceneIndex && sceneAudioClips[sceneIndex] != null)
        {
            if (audioSource.clip != sceneAudioClips[sceneIndex])
            {
                audioSource.clip = sceneAudioClips[sceneIndex];
                audioSource.loop = true; // Asegurar que la música se repita
                audioSource.Play();
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento al destruir el objeto
    }
}
