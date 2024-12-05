using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para el manejo de escenas

public class RutasNivelScript : MonoBehaviour
{
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
}
