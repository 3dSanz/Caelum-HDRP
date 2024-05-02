using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatarra : MonoBehaviour
{
    BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Pick()
    {
        boxCollider.enabled = false;
        SFXManager.instance.StopSound();
        SFXManager.instance.PlaySound(SFXManager.instance.cogerChatarra);
        Destroy(this.gameObject);
    }
}
