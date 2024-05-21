using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDemoAE : MonoBehaviour
{
    //private TransicionEscena _tEscena;
    //public GameObject _transicionEscena;
    public GameObject _final;
    //Animator _anim;
    //Animator _anim2;

    void Start()
    {
        //_tEscena = GameObject.Find("TransicionEscena").GetComponent<TransicionEscena>();
        //_anim2 = _final.GetComponent<Animator>();
    }

    /*public void FadeOut()
    {
        _transicionEscena.SetActive(true);
        _tEscena.EfectoCambioEscena();
    }*/

    public void Final()
    {
        _final.SetActive(true);
    }
}
