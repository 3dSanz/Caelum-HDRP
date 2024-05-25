using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseCanvas; // Asegúrate de asignar este objeto en el Inspector de Unity
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused; // Cambia el estado de pausa
            Pause();
        }
    }

    void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 0; // Pausa el juego
            pauseCanvas.SetActive(true); // Muestra el Canvas de pausa
        }
        else
        {
            Time.timeScale = 1; // Reanuda el juego
            pauseCanvas.SetActive(false); // Oculta el Canvas de pausa
        }
    }
}