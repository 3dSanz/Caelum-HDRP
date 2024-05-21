using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el objeto que entra en el trigger tiene la etiqueta "Player"
        {
            cinemachineCamera.Priority = 30; // Activa la cámara
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el objeto que sale del trigger tiene la etiqueta "Player"
        {
            cinemachineCamera.Priority = 0; // Desactiva la cámara
        }
    }
}