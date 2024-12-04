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
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayAudioForCurrentScene(); // Reproducir el audio adecuado al inicio
    }

    void PlayAudioForCurrentScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // Obtener el índice de la escena actual

        // Si hay un clip de audio asignado para esta escena, reproducirlo
        if (sceneAudioClips.Length > sceneIndex && sceneAudioClips[sceneIndex] != null)
        {
            audioSource.clip = sceneAudioClips[sceneIndex];
            audioSource.Play();
        }
    }

    // Método para cambiar el audio en tiempo real si lo deseas
    public void ChangeAudioClip(AudioClip newClip)
    {
        audioSource.clip = newClip;
        audioSource.Play();
    }
}