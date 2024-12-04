using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Slider para controlar el volumen
    public Text volumeText; // Texto para mostrar el valor actual del volumen

    void Start()
    {
        // Inicializamos el slider con el volumen actual
        volumeSlider.value = AudioListener.volume;

        // Agregamos un listener al slider para que cambie el volumen al mover la barra
        volumeSlider.onValueChanged.AddListener(UpdateVolume);

        // Actualizamos el texto con el volumen actual
        UpdateVolume(volumeSlider.value);
    }

    // Método que se llama cuando se mueve el slider
    void UpdateVolume(float value)
    {
        AudioListener.volume = value; // Ajusta el volumen global
        if (volumeText != null)
        {
            // Actualiza el texto con el porcentaje de volumen
            volumeText.text = "Volumen: " + Mathf.RoundToInt(value * 100) + "%";
        }
    }
}
