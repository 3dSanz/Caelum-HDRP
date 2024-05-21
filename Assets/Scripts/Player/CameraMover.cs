using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraMover : MonoBehaviour
{
    Movimiento _mov;
    Salto _jump;
    public CinemachineVirtualCamera cinemachineCamera;
    public float maxCameraMoveUp = 8.36f; // Límite superior
    public float maxCameraMoveDown = 3.08f; // Límite inferior
    public float cameraSpeed = 0.08f; // Velocidad de la cámara
    private float verticalInput;
    [SerializeField] private bool isMovingDown;
    [SerializeField] private bool isMovingUp;
    private float originalY = 6.13f; // Posición a la que vuelve la cámara

    void Start()
    {
        // Asegúrate de que la cámara comienza en la posición original
        cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = originalY;
        _mov = GetComponent<Movimiento>();
        _jump = GetComponent<Salto>();
    }

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");

        if(_mov._horizontal != 0 || _jump._isGrounded == false)
        {
            cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = originalY;
        }
        else
        {
            if (verticalInput < 0 && !isMovingDown && !isMovingUp)
            {
                StartCoroutine(MoveCameraDown());
            }
            else if (verticalInput > 0 && !isMovingUp && !isMovingDown)
            {
                StartCoroutine(MoveCameraUp());
            }
            else if (verticalInput == 0 && (isMovingDown || isMovingUp))
            {
                StopAllCoroutines();
                ResetCameraPosition();
            }
        }  
    }

    IEnumerator MoveCameraDown()
    {
        isMovingDown = true;

        while (cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y > maxCameraMoveDown)
        {
            cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y -= cameraSpeed;
            yield return null;
        }
    }

    IEnumerator MoveCameraUp()
    {
        isMovingUp = true;

        while (cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y < maxCameraMoveUp)
        {
            cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y += cameraSpeed;
            yield return null;
        }
    }

    void ResetCameraPosition()
    {
        cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset.y = originalY;
        isMovingDown = false;
        isMovingUp = false;
    }
}