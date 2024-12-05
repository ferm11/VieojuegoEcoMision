using UnityEngine;

public class BackgroundMusic3 : MonoBehaviour
{
    public AudioClip backgroundClip; // Clip de audio asignado desde el Inspector
    public float volume = 0.5f; // Volumen ajustable desde el Inspector

    private AudioSource audioSource;

    private void Awake()
    {
        // Asegura que solo haya un objeto con este script (singleton)
        if (FindObjectsOfType<BackgroundMusic3>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // Persiste entre escenas

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.volume = volume;
        audioSource.loop = true; // Habilita el bucle nativo de Unity

        PlayMusic(); // Inicia la música
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
