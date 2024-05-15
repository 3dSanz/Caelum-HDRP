using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBalas : MonoBehaviour
{
    //GameObjects
    [SerializeField] private GameObject _ataqueArriba;
    [SerializeField] private GameObject _ataqueCentro;
    [SerializeField] private GameObject _ataqueAbajo;
    // Start is called before the first frame update
    
    public void ActivarAtaqueCentro()
    {
        _ataqueCentro.SetActive(true);
    }

    public void DesactivarAtaqueCentro()
    {
        _ataqueCentro.SetActive(false);
    }

    public void ActivarAtaqueAlto()
    {
        _ataqueArriba.SetActive(true);
    }

    public void DesactivarAtaqueAlto()
    {
        _ataqueArriba.SetActive(false);
    }

    public void ActivarAtaqueBajo()
    {
        _ataqueAbajo.SetActive(true);
    }

    public void DesactivarAtaqueBajo()
    {
        _ataqueAbajo.SetActive(false);
    }
}
