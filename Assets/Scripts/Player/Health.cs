using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Health : MonoBehaviour
{
    private Animator _anim;
    private SFXManager sfxManager;
    public float _currentHealth = 4f;
    public bool _isAlive = true;
    [SerializeField] GameObject[] _hpIU;
    [SerializeField] GameObject[] _noHpIU;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
    }

    void Update()
    {
        ControlUIHP();
    }

    public void TakeDamage(float amount)
    {
        if(_isAlive == true)
        {
            _anim.SetTrigger("Hit");
            _currentHealth -= amount;
            SFXManager.instance.StopSound();
            SFXManager.instance.PlaySound(SFXManager.instance.enemyHitPlayerSound);

            if (_currentHealth <= 0 && _isAlive == true)
            {
                _anim.SetTrigger("IsDeath");
                SFXManager.instance.PlaySound(SFXManager.instance.deathSound);
                //Invoke(nameof(Die), 1.5f);
                _isAlive = false;
            }
        }  
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void ControlUIHP()
    {
        if (_currentHealth == 4)
        {
            _hpIU[0].SetActive(true);
            _hpIU[1].SetActive(true);
            _hpIU[2].SetActive(true);
            _hpIU[3].SetActive(true);

            _noHpIU[0].SetActive(false);
            _noHpIU[1].SetActive(false);
            _noHpIU[2].SetActive(false);
            _noHpIU[3].SetActive(false);
        } else if(_currentHealth == 3)
        {
            _hpIU[0].SetActive(true);
            _hpIU[1].SetActive(true);
            _hpIU[2].SetActive(true);
            _hpIU[3].SetActive(false);

            _noHpIU[0].SetActive(false);
            _noHpIU[1].SetActive(false);
            _noHpIU[2].SetActive(false);
            _noHpIU[3].SetActive(true);
        }else if(_currentHealth == 2)
        {
            _hpIU[0].SetActive(true);
            _hpIU[1].SetActive(true);
            _hpIU[2].SetActive(false);
            _hpIU[3].SetActive(false);

            _noHpIU[0].SetActive(false);
            _noHpIU[1].SetActive(false);
            _noHpIU[2].SetActive(true);
            _noHpIU[3].SetActive(true);
        }
        else if(_currentHealth == 1)
        {
            _hpIU[0].SetActive(true);
            _hpIU[1].SetActive(false);
            _hpIU[2].SetActive(false);
            _hpIU[3].SetActive(false);

            _noHpIU[0].SetActive(false);
            _noHpIU[1].SetActive(true);
            _noHpIU[2].SetActive(true);
            _noHpIU[3].SetActive(true);
        }else if(_currentHealth <= 0)
        {
            _hpIU[0].SetActive(false);
            _hpIU[1].SetActive(false);
            _hpIU[2].SetActive(false);
            _hpIU[3].SetActive(false);

            _noHpIU[0].SetActive(true);
            _noHpIU[1].SetActive(true);
            _noHpIU[2].SetActive(true);
            _noHpIU[3].SetActive(true);
        }
    }
}
