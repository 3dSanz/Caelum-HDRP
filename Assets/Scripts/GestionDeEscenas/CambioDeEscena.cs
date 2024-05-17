using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    private GameObject _canvasX;
    private BoxCollider _collider;
    [SerializeField] private int _numeroEscena;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _canvasX.SetActive(true);
            if(Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(_numeroEscena);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canvasX.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(_numeroEscena);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canvasX.SetActive(false);
        }
    }

}
