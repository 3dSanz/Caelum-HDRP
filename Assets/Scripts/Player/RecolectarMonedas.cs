using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.VFX.VFXTypeAttribute;

public class RecolectarMonedas : MonoBehaviour
{   
    public Text coinText;
    public float contMonedas;
    private SaveVidasMonedas _save;

    void Start()
    {
        _save = GameObject.Find("SaveItems").GetComponent<SaveVidasMonedas>();
        contMonedas = _save._totalMonedas;
    }
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
