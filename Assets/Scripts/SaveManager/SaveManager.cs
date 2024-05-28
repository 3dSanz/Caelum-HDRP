using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] public string checkPoint;
    [SerializeField] public Vector3 playerPosition;
    [SerializeField] private float _positionX;
    [SerializeField] private float _positionY;
    [SerializeField] private float _positionZ;

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
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(sceneName + "checkpoint", checkPoint);
        PlayerPrefs.SetFloat(sceneName + "position x", player.transform.position.x);
        PlayerPrefs.SetFloat(sceneName + "position y", player.transform.position.y);
        PlayerPrefs.SetFloat(sceneName + "position z", player.transform.position.z);

        LoadData();
    }

    void LoadData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey(sceneName + "position x"))
        {
            playerPosition = new Vector3(PlayerPrefs.GetFloat(sceneName + "position x"), PlayerPrefs.GetFloat(sceneName + "position y"), PlayerPrefs.GetFloat(sceneName + "position z"));
        }
        else
        {
            playerPosition = new Vector3(_positionX, _positionY, _positionZ);
        }
    }

    public void DeleteData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.DeleteKey(sceneName + "checkpoint");
        PlayerPrefs.DeleteKey(sceneName + "position x");
        PlayerPrefs.DeleteKey(sceneName + "position y");
        PlayerPrefs.DeleteKey(sceneName + "position z");
        PlayerPrefs.DeleteAll();

        LoadData();
    }
}