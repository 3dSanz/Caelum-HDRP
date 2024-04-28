using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnCollision : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float _timeGolpe = 2f;
    private float _timeSiguienteGolpe;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" /*&& Time.time >= _timeSiguienteGolpe*/)
        {
            other.GetComponentInParent<Health>().TakeDamage(damage);
            //_timeSiguienteGolpe = Time.time + _timeGolpe;
        }
    }
}
