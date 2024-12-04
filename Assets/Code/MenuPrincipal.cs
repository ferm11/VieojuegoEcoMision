using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para el manejo de escenas

public class MenuPrincipal : MonoBehaviour
{
    // Este método se llama cuando el botón es clicado
    public void IrALaEscenaOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
