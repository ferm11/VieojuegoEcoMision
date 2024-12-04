using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SalirMenuPrincipal : MonoBehaviour
{
    public void SalirAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
