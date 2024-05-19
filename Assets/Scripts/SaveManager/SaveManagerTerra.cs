using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManagerTerra : MonoBehaviour
{
    //[SerializeField] private Text checkPointText;
    [SerializeField] public string checkPoint;
    [SerializeField] public Vector3 playerPosition;

    Movimiento player;


    void Awake()
    {
        LoadData(); 
    }
    void Start()
    {
        player = GameObject.Find("Parcy").GetComponent<Movimiento>();

    }

    void Update()
    {
        playerPosition = player.transform.position;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("checkpoint", checkPoint);   
        PlayerPrefs.SetFloat("position x", player.transform.position.x);
        PlayerPrefs.SetFloat("position y", player.transform.position.y);
        PlayerPrefs.SetFloat("position z", player.transform.position.z);

        LoadData();
    }

    void LoadData()
    {
        //playerPosition = new Vector3(PlayerPrefs.GetFloat("position x", 8.82f), PlayerPrefs.GetFloat("position y", -0.5f), PlayerPrefs.GetFloat("position z", 83.6f));
        playerPosition = new Vector3(PlayerPrefs.GetFloat("position x", 507.65f), PlayerPrefs.GetFloat("position y", 5.14f), PlayerPrefs.GetFloat("position z", 83.6f));
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("checkpoint");
        PlayerPrefs.DeleteKey("position x");
        PlayerPrefs.DeleteKey("position y");
        PlayerPrefs.DeleteKey("position z");
        //PlayerPrefs.DeleteAll(); Se carga todos los PlayerPrefs de TODOS los scripts
        LoadData();
    }

    
}
