using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class AjustesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void IrAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }
}
