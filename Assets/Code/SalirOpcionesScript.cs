using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SalirOpcionesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void SalirAOpciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
