using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveVidasMonedas : MonoBehaviour
{
    [SerializeField] public float _currentHP;
    [SerializeField] public float _currentP;
    [SerializeField] public float _totalMonedas;
    Health _health;
    RecolectarMonedas _money;
    // Start is called before the first frame update
    void Awake()
    {
        LoadData();
    }

    void Start()
    {
        _health = GameObject.Find("Parcy").GetComponent<Health>();
        _money = GameObject.Find("Parcy").GetComponent<RecolectarMonedas>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentHP = _health._currentHealth;
        _currentP = _health._currentPotions;
        _totalMonedas = _money.contMonedas;
    }

    public void SaveData()
    {

        PlayerPrefs.SetFloat("CurrentHP", _health._currentHealth);
        PlayerPrefs.SetFloat("CurrentPotions", _health._currentPotions);
        PlayerPrefs.SetFloat("TotalMonedas", _money.contMonedas);

        LoadData();
    }

    void LoadData()
    {
        _currentHP = PlayerPrefs.GetFloat("CurrentHP", 4f);
        _currentP = PlayerPrefs.GetFloat("CurrentPotions", 2f);
        _totalMonedas = PlayerPrefs.GetFloat("TotalMonedas", 0f);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("CurrentHP");
        PlayerPrefs.DeleteKey("CurrentPotions");
        PlayerPrefs.DeleteKey("TotalMonedas");
        //PlayerPrefs.DeleteAll(); Se carga todos los PlayerPrefs de TODOS los scripts
        LoadData();
    }

    public void DeleteDataHP()
    {
        PlayerPrefs.DeleteKey("CurrentHP");
        PlayerPrefs.DeleteKey("CurrentPotions");
        LoadData();
    }
}
