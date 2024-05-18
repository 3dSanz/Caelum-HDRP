using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public GameObject _canvasX;
    private BoxCollider _collider;
    [SerializeField] private int _numeroEscena;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _canvasX.SetActive(true);
            if(Input.GetKeyDown(KeyCode.X))
            {
                TransicionEscena.instance.EfectoCambioEscena();
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
                TransicionEscena.instance.EfectoCambioEscena();
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
