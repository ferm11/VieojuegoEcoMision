using UnityEngine;
using UnityEngine.UI;  // Asegúrate de tener esta importación para Slider
using System.Collections; // Asegúrate de incluir esta línea

public class PlayerScript3 : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;  // Rigidbody2D del jugador
    [SerializeField] Animator anim;   // Animator para las animaciones
    [SerializeField] float speed;     // Velocidad del jugador
    [SerializeField] Slider healthSlider;  // Barra de salud

    private float maxHealth = 100f;  // Salud máxima
    private float currentHealth;     // Salud actual

    private Vector3 move;            // Movimiento del jugador

    // Variables añadidas para disparar proyectiles
    [SerializeField] GameObject projectilePrefab;  // Prefab del proyectil
    [SerializeField] Transform shootPoint;         // Punto de disparo

    public GameOverController gameOverController;
    public GameObject gameOverImage; 
    private bool canShoot = true;
    private float shootCooldown = 1f;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;  // Reanudar el juego
        currentHealth = maxHealth;  // Inicializa la salud al máximo
        healthSlider.maxValue = maxHealth;  // Establece el valor máximo de la barra de salud
        healthSlider.value = currentHealth; // Establece el valor actual de la barra de salud

        ResetPlayerState();  // Resetea el estado del jugador al inicio

        // Busca el controlador de "Game Over" en la escena
        gameOverController = FindObjectOfType<GameOverController>();

        if (gameOverController == null)
        {
            Debug.LogWarning("No se encontró el GameOverController en la escena.");
        }

        // Audio de disparo
        // Obtiene el componente AudioSource o lo añade si no existe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        // Obtiene las entradas de movimiento
        move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        
        // Actualiza las animaciones de movimiento
        anim.SetFloat("horizontal", move.x);
        anim.SetFloat("vertical", move.y);

        // Detectar si se presiona la tecla E para disparar
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            ShootProjectile();
            PlayShootSound();
            StartCoroutine(ShootCooldown());
        }
    }

    // Metodo de audio para disparar
    private void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound); // Reproduce el sonido una vez
        }
        else
        {
            Debug.LogWarning("El sonido de disparo no está asignado o falta el AudioSource.");
        }
    }

    // Método para manejar el tiempo de espera entre disparos
    private IEnumerator ShootCooldown()
    {
        canShoot = false; // Deshabilitar disparos
        yield return new WaitForSeconds(shootCooldown); // Esperar el tiempo especificado
        canShoot = true; // Volver a habilitar disparos
    }

    private void FixedUpdate()
    {
        // Mueve al jugador según la entrada
        rb.MovePosition(transform.position + (move * speed * Time.fixedDeltaTime));
    }

    private void ResetPlayerState()
    {
        // Resetea la posición del jugador y su estado de movimiento
        transform.position = new Vector3(0, 0, 0);
        move = Vector3.zero;
        anim.SetFloat("horizontal", 0);
        anim.SetFloat("vertical", 0);
    }

    // Función añadida para disparar proyectiles
    private void ShootProjectile()
    {
        // Instanciar el proyectil en el punto de disparo
        if (projectilePrefab != null && shootPoint != null)
        {
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        }
    }

    // Función para recibir daño
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Resta vida
        healthSlider.value = currentHealth;  // Actualiza la barra de salud

        if (currentHealth <= 0)
        {
            Die();  // Si la salud llega a cero, muere
        }
    }

    // Función para cuando el jugador muere
    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        
        // Asegúrate de que gameOverController esté asignado
        if (gameOverController != null)
        {
            gameOverController.ShowGameOver();  // Activa el panel de Game Over
        }
        else
        {
            Debug.LogWarning("El GameOverController no está asignado.");
        }
    }

    // Usamos OnTriggerEnter2D si el collider es Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("VillanoProjectile"))  // Si colisiona con un proyectil del villano
        {
            TakeDamage(5f);  // Resta vida al jugador
            Destroy(other.gameObject);  // Destruye el proyectil
        }
    }
}
