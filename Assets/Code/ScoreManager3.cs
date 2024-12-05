using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager3 : MonoBehaviour
{
    public static int score = 0; // Puntaje global
    public Text scoreText; // Campo para asignar el texto en el Inspector

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetScore;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetScore;
    }

    private void Update()
    {
        scoreText.text = "Puntaje: " + ProjectilPlayer.score; // Actualiza el texto del puntaje
    }

    private void ResetScore(Scene scene, LoadSceneMode mode)
    {
        ProjectilPlayer.score = 0; // Reinicia el puntaje a 0
    }
    
}

