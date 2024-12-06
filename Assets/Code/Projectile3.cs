using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Velocidad del proyectil
    public float lifeTime = 3f; // Tiempo antes de destruir el proyectil
    public float minSpeed = 0.1f;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private float distanceTraveled = 0f;  // Variable para rastrear la distancia recorrida

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);  // Destruye el proyectil después de un tiempo
    }

    void Update()
    {
        if (rb != null && rb.velocity.magnitude < minSpeed)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);  // Movimiento del proyectil

        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled > 100f)  // Destruye el proyectil si se mueve demasiado lejos
        {
            Destroy(gameObject);
        }
    }

    // Reemplaza OnTriggerEnter2D con OnCollisionEnter2D si el Collider no es Trigger
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player")) // Si colisiona con el jugador
    {
        Debug.Log("Colisión con el jugador detectada.");

        PlayerScript3 player = other.GetComponent<PlayerScript3>();
        if (player != null)
        {
            player.TakeDamage(10f); // Daño al jugador
        }
        Destroy(gameObject); // Destruye el proyectil
    }

    if (other.CompareTag("PlayerProjectile")) // Si choca con otro proyectil del jugador
    {
        // Reproduce el audio del choque
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No hay clip asignado al AudioSource o AudioSource no encontrado.");
        }

        Destroy(gameObject); // Destruye este proyectil
        Destroy(other.gameObject); // Destruye el proyectil que lo golpeó
    }
}
}