using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAnim : MonoBehaviour
{
    private Ataque _attack;
    public GameObject slashcentro;
    public GameObject slashArriba;
    public GameObject slashAbajo;
    // Start is called before the first frame update
    void Start()
    {
        _attack = GetComponentInParent<Ataque>();

    }

    public void HitAtaqueCentro()
    {
        _attack.PerformAttack(_attack._damage);
    }

    public void HitAtaqueAlto()
    {
        _attack.PerformUpAttack(_attack._damage);
    }

    public void HitAtaqueBajo()
    {
        _attack.DownAttack(_attack._damage);
    }

    public void SlashVFXCentroActivar()
    {
        slashcentro.SetActive(true);
    }

    public void SlashVFXCentroDesactivar()
    {
        slashcentro.SetActive(false);
    }

    public void SlashVFXArribaActivar()
    {
        slashArriba.SetActive(true);
    }

    public void SlashVFXArribaDesactivar()
    {
        slashArriba.SetActive(false);
    }

    public void SlashVFXAbajoActivar()
    {
        slashAbajo.SetActive(true);
    }

    public void SlashVFXAbajoDesactivar()
    {
        slashAbajo.SetActive(false);
    }
}
