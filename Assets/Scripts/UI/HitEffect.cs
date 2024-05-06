using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HitEffect : MonoBehaviour
{
    private Health _hp;
    private Volume _volume;
    [SerializeField] private float _cont;

    void Awake()
    {
        _hp = GameObject.Find("Parcy").GetComponent<Health>();
        _volume = GetComponent<Volume>();
    }

    private void Start()
    {
        _cont = _hp._currentHealth;
    }

    void Update()
    {
        if(_hp._currentHealth < _cont)
        {
            StartCoroutine(EfectoCamara());
            _cont = _hp._currentHealth;
        }
    }
    IEnumerator EfectoCamara()
    {
        _volume.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _volume.enabled = false;
    }
}
