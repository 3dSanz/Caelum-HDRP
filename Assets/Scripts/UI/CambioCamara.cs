using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject cinemachineCamera;
    public GameObject botonInicio;
    public GameObject iniciarConversacion;
    private bool dentro;

    private void Awake()
    {
        iniciarConversacion.SetActive(true);
    }

    private void Start()
    {
        iniciarConversacion.SetActive(false);
    }
    private void Update()
    {
        if ((Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.B)) && dentro == true)
        {
            iniciarConversacion.SetActive(true);
            cinemachineCamera.SetActive(true); // Activa la cámara
            botonInicio.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el objeto que entra en el trigger tiene la etiqueta "Player"
        {
            dentro = true;
            botonInicio.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el objeto que sale del trigger tiene la etiqueta "Player"
        {
            iniciarConversacion.SetActive(false);
            botonInicio.SetActive(false);
            cinemachineCamera.SetActive(false); // Desactiva la cámara
            dentro = false;
        }
    }
}