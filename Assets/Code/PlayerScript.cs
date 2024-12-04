using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] float speed;

    private Vector3 move;

   private void Start()
{
    ResetPlayerState(); // Método para inicializar correctamente al jugador

    // Mostrar el nombre de la escena actual
    Debug.Log("Escena actual: " + SceneManager.GetActiveScene().name);

    // Detener la música si estamos en la escena "Nivel1" o "Nivel1-2"
    if (SceneManager.GetActiveScene().name == "Nivel1" || SceneManager.GetActiveScene().name == "Nivel2")
    {
        // Aseguramos que AudioManager esté activo y detener la música
        AudioManager audioManager = FindObjectOfType<AudioManager>(); // Encontramos la instancia del AudioManager

        if (audioManager != null)
        {
            Debug.Log("Deteniendo música en: " + SceneManager.GetActiveScene().name); // Verificar qué escena se está cargando
            audioManager.GetComponent<AudioSource>().Stop(); // Detener la música
        }
        else
        {
            Debug.LogWarning("No se encontró el AudioManager en la escena.");
        }
    }
    else
    {
        Debug.Log("Escena no es Nivel1 ni Nivel1-2, la música sigue activa.");
    }
}


    private void Update()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        anim.SetFloat("horizontal", move.x);
        anim.SetFloat("vertical", move.y);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (move * speed * Time.fixedDeltaTime));
    }

    private void ResetPlayerState()
    {
        // Reinicia posición inicial y otros estados relevantes
        transform.position = new Vector3(0, 0, 0); // Cambia las coordenadas si es necesario
        move = Vector3.zero;
        anim.SetFloat("horizontal", 0);
        anim.SetFloat("vertical", 0);
    }
}
