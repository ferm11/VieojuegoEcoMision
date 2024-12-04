using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class SeleccionarPersonajeScript : MonoBehaviour
{
    public void IrASeleccionarPersonaje()
    {
        SceneManager.LoadScene("ElegirPersonaje");
    }

    public void IrANivel1Player1(){
        SceneManager.LoadScene("Nivel1");
    }

    public void IrANivel1Player2(){
        SceneManager.LoadScene("Nivel2");
    }


}
