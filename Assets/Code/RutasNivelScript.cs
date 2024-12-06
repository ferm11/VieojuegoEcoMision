using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para el manejo de escenas

public class RutasNivelScript : MonoBehaviour
{

    public AudioClip menuMusic; // Asigna el clip de música desde el inspector
    private AudioSource audioSource;
    
    public void IrAMapaNiveles()
    {
        SceneManager.LoadScene("MapaNiveles");
    }

    public void IrANivel3()
    {
        Time.timeScale = 1f;  // Reanudar el juego
        SceneManager.LoadScene("Nivel3");
    }

    public void IrANivel4()
    {
        Time.timeScale = 1f;  // Reanudar el juego
        SceneManager.LoadScene("Nivel4");
        Time.timeScale = 1f;
    }

    public void IrAMenuPrincipal()
    {
        Time.timeScale = 1f;
        Debug.Log("Botón presionado: Cargando escena Opciones...");
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void IrAElegirPersonaje()
    {
        SceneManager.LoadScene("ElegirPersonaje2");
    }

    public void IrAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }

}
