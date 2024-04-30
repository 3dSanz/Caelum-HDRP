using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatarra : MonoBehaviour
{
    BoxCollider _bc;

    void Awake()
    {
        _bc = GetComponent<BoxCollider>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}
