using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VillanoScript : MonoBehaviour
{
    // Movimiento
    public float speed = 2f;
    public float changeDirectionTime = 2f;
    public float xMin = -2.2f, xMax = 4.00f;  // Rango X de movimiento
    public float yMin = 5.11f, yMax = 9.28f; // Rango Y de movimiento

    private Vector2 direction;
    private float timer;

    // Ataque
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de disparo (asígnalo en el inspector)
    public float fireRate = 1.5f; // Tiempo entre disparos
    private float fireTimer;

    private float nextFireTime = 0f;

    // **Sonido de disparo**
    public AudioClip shootSound; // Asigna el clip de audio desde el Inspector
    private AudioSource audioSource;

    // Diálogo
    public GameObject dialoguePanel;  // Panel de diálogo
    public TextMeshProUGUI dialogueText;  // Cambiado de Text a TextMeshProUGUI
    public string[] dialogues;  // Array de diálogos
    private int currentDialogueIndex = 0;  // Para llevar el control de qué diálogo mostrar
    private bool isPlayerInRange = false; // Para saber si el jugador está cerca
    private bool isInDialogue = false;  // Para saber si estamos en la fase de diálogo

    // Estado inicial del villano
    private bool hasStarted = false;  // Para saber si el villano ya comenzó a moverse y disparar

    // Vida
    public float maxHealth = 100f;  // Vida máxima del villano
    private float currentHealth;  // Vida actual del villano

    // Barra de vida
    public Slider healthBar;

    public GameController3 gameController;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        ChangeDirection();

        fireTimer = Mathf.Infinity;
        nextFireTime = Mathf.Infinity;

        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("¡El villano necesita un Collider2D para detectar la colisión!");
        }

        if (healthBar == null)
        {
            Debug.LogError("¡La barra de vida no está asignada en el Inspector!");
        }

        // Configuración del AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isInDialogue)
        {
            StartDialogue();
        }

        if (isInDialogue)
        {
            fireTimer = Mathf.Infinity;  
            nextFireTime = Mathf.Infinity;  
            speed = 0f;

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                ShowNextDialogue();
            }
        }
        else if (hasStarted)
        {
            transform.Translate(direction * speed * Time.deltaTime);

            Vector2 currentPosition = transform.position;
            float clampedX = Mathf.Clamp(currentPosition.x, xMin, xMax); 
            float clampedY = Mathf.Clamp(currentPosition.y, yMin, yMax); 
            transform.position = new Vector2(clampedX, clampedY);

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChangeDirection();
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                FireProjectile();
            }

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void ChangeDirection()
    {
        direction = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-0.2f, 0.2f)
        ).normalized;

        Vector2 currentPosition = transform.position;
        if (currentPosition.x <= xMin || currentPosition.x >= xMax)
        {
            direction.x = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
        }
        if (currentPosition.y <= yMin || currentPosition.y >= yMax)
        {
            direction.y = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
        }

        timer = changeDirectionTime;
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * 10f; // Ajusta la velocidad según sea necesario
        }
        fireTimer = fireRate;

        // Reproducir sonido al disparar
        PlayShootSound();
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Reproducir sonido al disparar
        PlayShootSound();
    }

    void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogWarning("El sonido de disparo no está asignado o falta el AudioSource.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (!isInDialogue)
            {
                dialogueText.text = "Presiona 'E' para hablar con el villano";
            }
        }

        // Verificar si el villano fue golpeado por un proyectil del jugador
        if (other.CompareTag("PlayerProjectile"))
        {
            Debug.Log("Proyectil impactó al villano");
            TakeDamage(maxHealth * 0.02f);  // Resta un 2% de la vida máxima
            Destroy(other.gameObject);  // Destruye el proyectil del jugador
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueText.text = "";
        }
    }

    private void StartDialogue()
    {
        isInDialogue = true;
        dialoguePanel.SetActive(true); 
        ShowNextDialogue(); 
    }

    private void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        StartAttackPhase(); 
    }

    public void StartAttackPhase()
    {
        isInDialogue = false;
        hasStarted = true;  
        speed = 2f;  
        fireTimer = fireRate;  
        nextFireTime = Time.time + fireRate;  
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Asegúrate de que la vida no baje de 0
        currentHealth = Mathf.Max(currentHealth, 0);

        // Actualiza la barra de vida inmediatamente
        UpdateHealthBar();

        // Comprueba si el villano ha muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;  // Calcula el porcentaje de vida
        }
        else
        {
            Debug.LogWarning("La barra de vida no está configurada en el Inspector.");
        }
    }

    private void Die()
    {
        Debug.Log("Método Die() ejecutado."); // Asegurarte que se llama
        Debug.Log("El villano ha sido derrotado.");
        Destroy(gameObject);

        GameController3 gameController = FindObjectOfType<GameController3>();
        if (gameController != null)
        {
            gameController.ShowLevelComplete(); 
        }
    }
}
