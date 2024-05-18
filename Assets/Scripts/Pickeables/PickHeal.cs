using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PickHeal : MonoBehaviour
{
    private VisualEffect _vfxItem;
    private Health _health;

    private void Start()
    {
        _vfxItem = GameObject.Find("ParticulaPickUp").GetComponent<VisualEffect>();
        _health = GameObject.Find("Parcy").GetComponent<Health>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _vfxItem.Play();
            _health._currentPotions++;
            Destroy(this.gameObject);
        }
    }
}
