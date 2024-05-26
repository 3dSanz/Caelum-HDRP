using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Animator _anim;
    private SFXManager sfxManager;
    private SaveVidasMonedas _save;
    private TransicionEscena _tescena;
    private ParticleSystem _pSystem;
    [SerializeField] private GameObject _particulaCura;
    private VisualEffect _vfxHeal;
    public float _currentHealth;
    public float _maxHealth;
    public bool _isAlive = true;
    [SerializeField] GameObject[] _hpIU;
    [SerializeField] GameObject[] _noHpIU;
    [SerializeField] GameObject[] _siPociones;
    [SerializeField] GameObject[] _noPociones;
    public float _currentPotions;
    public float _maxPotions;
    int sceneNumber;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        sfxManager = GameObject.Find("SFXManager").GetComponent<SFXManager>();
        _tescena = GameObject.Find("TransicionEscena").GetComponent<TransicionEscena>();
        _pSystem = GameObject.Find("ParticulaGolpeado").GetComponent<ParticleSystem>();
        _vfxHeal = GameObject.Find("ParticulaCura").GetComponent<VisualEffect>();
        _save = GameObject.Find("SaveItems").GetComponent<SaveVidasMonedas>();
        _currentHealth = _save._currentHP;
        _currentPotions = _save._currentP;
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

    }

    void Update()
    {
        ControlUIHP();
        ControlUIPotions();
        if (Input.GetKeyDown(KeyCode.E) && _currentPotions > 0 && _currentHealth != _maxHealth)
        {
            Heal();
            _currentPotions--;
        }

        if(_currentPotions > _maxPotions)
        {
            _currentPotions = _maxPotions;
            _currentHealth++;
            _vfxHeal.Play();
            SFXManager.instance.PlaySound(SFXManager.instance.playerHeal);
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if(_isAlive == true)
        {
            _anim.SetTrigger("Hit");
            _currentHealth -= amount;
            SFXManager.instance.StopSound();
            SFXManager.instance.PlaySound(SFXManager.instance.enemyHitPlayerSound);
            _pSystem.Play();

            if (_currentHealth <= 0 && _isAlive == true)
            {
                _anim.SetTrigger("IsDeath");
                SFXManager.instance.PlaySound(SFXManager.instance.deathSound);
                //Invoke(nameof(Die), 1.5f);
                _isAlive = false;
                _save.DeleteDataHP();
            }
        }
        
        if(_isAlive == false && sceneNumber == 1)
        {
            _tescena.CambiarEscena2();
        }

        if (_isAlive == false && sceneNumber == 3)
        {
            _tescena.CambiarEscena4();
        }
    }

    void Heal()
    {
        _currentHealth++;
        _vfxHeal.Play();
        SFXManager.instance.PlaySound(SFXManager.instance.playerHeal);
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    /*IEnumerator DeathMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CP1" || other.gameObject.tag == "CP2")
        {
            //save.checkPoint = "1";
            Debug.Log("Items and Life Saved!");
            _save.SaveData();
        }
    }
    private void ControlUIPotions()
    {
        if (_currentPotions == 2)
        {
            _siPociones[0].SetActive(true);
            _siPociones[1].SetActive(true);

            _noPociones[0].SetActive(false);
            _noPociones[1].SetActive(false);
        }
        else if (_currentPotions == 1)
        {
            _siPociones[0].SetActive(true);
            _siPociones[1].SetActive(false);


            _noPociones[0].SetActive(false);
            _noPociones[1].SetActive(true);

        }
        else if (_currentPotions == 0)
        {
            _siPociones[0].SetActive(false);
            _siPociones[1].SetActive(false);


            _noPociones[0].SetActive(true);
            _noPociones[1].SetActive(true);

        }
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
