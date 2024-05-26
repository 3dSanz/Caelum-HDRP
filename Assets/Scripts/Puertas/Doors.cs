using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator _anim;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isOpen)
        {
            _anim.SetTrigger("AbrirPuerta");
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isOpen)
        {
            _anim.SetTrigger("CerrarPuerta");
            isOpen = false;
        }
    }
}
