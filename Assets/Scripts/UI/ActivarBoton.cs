using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBoton : MonoBehaviour
{
    public GameObject _boton;
    // Start is called before the first frame update
    public void ActivarBotonMenu()
    {
        _boton.SetActive(true);
    }
}
