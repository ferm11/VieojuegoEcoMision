using UnityEngine;

public class ProjectilPlayer : MonoBehaviour
{
    public float speed = 5f; // Velocidad del proyectil
    public float lifeTime = 3f; // Tiempo antes de destruir el proyectil
    public float minSpeed = 0.1f; // Velocidad mínima antes de destruir el proyectil
    private Rigidbody2D rb;
    public static int score = 0; // Variable estática para el puntaje

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Asegura que el proyectil se mueva hacia arriba al inicializarlo
        if (rb != null)
        {
            rb.velocity = Vector2.up * speed;
        }

        // Destruye el proyectil después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Verifica si el proyectil se detuvo por debajo de la velocidad mínima
        if (rb != null && rb.velocity.magnitude < minSpeed)
        {
            Destroy(gameObject); // Destruye el proyectil si se detiene
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Villain")) // Si colisiona con un villano
        {
            VillanoScript villain = other.GetComponent<VillanoScript>();
            if (villain != null)
            {
                villain.TakeDamage(100); // Reduce la vida del villano
                AddScore(10); // Agrega 10 puntos al puntaje
            }
            Destroy(gameObject); // Destruye el proyectil
        }
    }

    // Método para incrementar el puntaje
    private void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score); // Muestra el puntaje en la consola
    }
}
