using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarParticulas : MonoBehaviour
{
    Movimiento _mov;
    // Start is called before the first frame update
    void Start()
    {
        _mov = GetComponentInParent<Movimiento>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_mov._horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
        } else
        {
            transform.rotation = Quaternion.Euler(0,90,0);
        }
    }
}
