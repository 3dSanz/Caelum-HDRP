using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecolectarMonedas : MonoBehaviour
{   
    public Text coinText;
    int contMonedas;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "ColisionMoneda")
        {
            Chatarra coin = other.gameObject.GetComponent<Chatarra>();
            coin.Pick();
            AddCoin();
        }
    }

     public void AddCoin()
    {
        contMonedas++;
        SFXManager.instance.StopSound();
        SFXManager.instance.PlaySound(SFXManager.instance.cogerChatarra);
        coinText.text = contMonedas.ToString();
    }
}
