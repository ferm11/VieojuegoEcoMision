using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para el manejo de escenas

public class JuegoTermiadoScript : MonoBehaviour
{
    public void IrAJuegoTerminado()
    {
        SceneManager.LoadScene("JuegoTerminado");
    }
}
