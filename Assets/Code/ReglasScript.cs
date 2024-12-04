using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escenas

public class ReglasScript : MonoBehaviour
{
    // Método para redirigir a la escena "Reglas"
    public void IrAReglas()
    {
        SceneManager.LoadScene("Reglas");
    }

    // Método para redirigir a la escena "Opciones"
    public void SalirAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
