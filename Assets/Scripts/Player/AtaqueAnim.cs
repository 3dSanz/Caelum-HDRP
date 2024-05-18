using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAnim : MonoBehaviour
{
    private Ataque _attack;
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
}
