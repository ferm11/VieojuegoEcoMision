using UnityEngine;
using TMPro; // Asegúrate de que estás usando TextMeshPro

public class VillanoDialogo : MonoBehaviour
{
    public GameObject player; // Referencia al jugador
    public float distanciaLimite = 5f; // Distancia límite para activar el diálogo
    public GameObject dialogo; // Referencia al objeto del diálogo (Canvas o Panel)
    public TextMeshProUGUI textoDialogo; // Referencia al componente TextMeshProUGUI para mostrar el texto del diálogo
    public GameObject botonContinuar; // Referencia al botón de continuar

    private RectTransform rectTransform;
    private string[] dialogos = new string[]
    {
        "Player: ¿Quién eres tú? ¿Y qué quieres de mí?",
        "Villano: Ah, así que eres tú, el 'héroe' que ha estado limpiando las calles, ¿verdad? ¡Vaya, al fin te encuentro!",
        "Player: No me hagas perder el tiempo. Responde mi pregunta: ¿quién eres realmente?",
        "Villano: ¡JAJAJAJA! Soy el caos encarnado. Soy el que se esconde en las sombras, el que ensucia todo lo que toco. He oído hablar de ti. Eres el que se dedica a ordenar, a limpiar, a mantener la fachada de la ciudad. ¿Qué pasa, crees que eso cambiará algo?",
        "Player: Sí, es cierto. He estado limpiando las calles, pero más importante, estoy aquí para poner fin a tu reinado de desorden.",
        "Villano: ¡Tienes una audacia impresionante! Pero no importa cuántos contenedores organices ni cuántas veces limpies. El caos siempre encontrará su camino. Y tú, te convertirás en parte de él.",
        "Player: Entonces, prepárate, porque voy a detenerte... una vez más.",
        "Villano: ¿De veras? Pues inténtalo, héroe. Pero te aseguro que esta vez, el caos te aplastará."
    };

    private int indiceDialogo = 0; // Índice para saber qué diálogo mostrar

    private VillanoScript villanoScript;  // Referencia al script del villano para activar la fase de ataque

    private void Start()
    {
        // Asegurarse de que el diálogo esté desactivado al inicio
        dialogo.SetActive(false);
        botonContinuar.SetActive(false); // El botón de continuar se desactiva al inicio
        villanoScript = GetComponent<VillanoScript>();  // Obtener la referencia al script del villano
    }

    private void Update()
    {
        // Calcular la distancia entre el villano y el jugador
        float distancia = Vector2.Distance(transform.position, player.transform.position);

        // Si la distancia es menor que el límite, activar el diálogo
        if (distancia < distanciaLimite)
        {
            if (!dialogo.activeSelf) // Si el diálogo no está activo ya, activarlo
            {
                dialogo.SetActive(true);
                ConfigurarDialogoPantallaCompleta(); // Configurar tamaño
                MostrarDialogo(); // Mostrar el primer diálogo
                botonContinuar.SetActive(true); // Activar el botón para continuar el diálogo
            }
        }
        else
        {
            if (dialogo.activeSelf) // Si el diálogo está activo y el jugador está fuera del rango, desactivarlo
            {
                dialogo.SetActive(false);
            }
        }
    }

    private void ConfigurarDialogoPantallaCompleta()
    {
        if (rectTransform == null) // Verificar si rectTransform ya se asignó
            rectTransform = dialogo.GetComponent<RectTransform>();
        
        if (rectTransform != null)
        {
            // Configurar los offsets del RectTransform para cubrir toda la pantalla
            rectTransform.anchorMin = new Vector2(0, 0); // Anclar a la esquina inferior izquierda
            rectTransform.anchorMax = new Vector2(1, 1); // Anclar a la esquina superior derecha
            rectTransform.offsetMin = Vector2.zero; // Restablecer el offset mínimo
            rectTransform.offsetMax = Vector2.zero; // Restablecer el offset máximo
            rectTransform.sizeDelta = Vector2.zero; // Asegurar que el tamaño delta sea 0
        }
    }

    private void MostrarDialogo()
    {
        // Mostrar el diálogo actual en el texto
        if (indiceDialogo < dialogos.Length)
        {
            textoDialogo.text = dialogos[indiceDialogo];
        }
        else
        {
            // Si ya no hay más diálogos, ocultar el panel y desactivar el botón
            dialogo.SetActive(false);
            botonContinuar.SetActive(false);
            villanoScript.StartAttackPhase();  // Activar la fase de ataque del villano
        }
    }

    // Este método debe ser asignado al botón de continuar en el inspector
    public void ContinuarDialogo()
    {
        indiceDialogo++; // Avanzar al siguiente diálogo
        MostrarDialogo(); // Mostrar el siguiente diálogo
    }
}
