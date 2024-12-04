using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SalirPersonajeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void SalirPersonaje()
    {
        SceneManager.LoadScene("ElegirPersonaje");
    }
}
