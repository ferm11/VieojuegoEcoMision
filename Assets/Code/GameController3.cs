using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de que esta línea está incluida

public class GameController3 : MonoBehaviour
{
    public GameObject levelCompleteImage; // Imagen de nivel completado
    public GameObject anotherPanel; // Referencia al otro panel que quieres abrir

    private void Start()
    {
        if (anotherPanel != null)
        {
            Debug.Log("Probando activación de panel en Start().");
            anotherPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("El objeto 'anotherPanel' no está asignado en el Inspector.");
        }
    }

    public void ShowLevelComplete()
    {
        if (levelCompleteImage != null)
        {
            levelCompleteImage.SetActive(true);

            RectTransform rectTransform = levelCompleteImage.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(0.73f, 2.09f);
            }
            else
            {
                Debug.LogWarning("El objeto asignado a 'levelCompleteImage' no tiene un RectTransform.");
            }

            Debug.Log("Panel de Level Complete Activado");
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("Level Complete Image no está asignada en el Inspector.");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Reactivar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continuar()
{
    Debug.Log("Método OpenPanel() llamado."); // Confirmar ejecución
    if (anotherPanel != null)
    {
        Debug.Log($"Panel encontrado: {anotherPanel.name}, configurando posición y activando...");
        
        // Configurar la posición del panel
        RectTransform rectTransform = anotherPanel.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = new Vector2(0.73f, 2.09f); // Posición deseada
            Debug.Log("Posición del panel configurada.");
        }
        else
        {
            Debug.LogWarning("El panel no tiene un componente RectTransform.");
        }
        
        // Activar el panel
        anotherPanel.SetActive(true);
        Debug.Log("Otro panel activado.");
    }
    else
    {
        Debug.LogWarning("El objeto 'anotherPanel' no está asignado en el Inspector.");
    }
}


}
