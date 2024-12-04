using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class IniciarMapa : MonoBehaviour
{
    // Este método se llama cuando el botón es clicado
    public void IrAElegirMapa()
    {
        SceneManager.LoadScene("MapaNiveles");
    }
}
