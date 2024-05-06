using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{
    public static TransicionEscena instance { get; private set; }
    private Animator _anim;
    [SerializeField] private AnimationClip Inicio;
    [SerializeField] private AnimationClip Final;
    private bool _primeraTransicion = true;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if(_primeraTransicion == true)
        {
            StartCoroutine(PrimeraTransicion());
        }
    }

    IEnumerator PrimeraTransicion()
    {
        yield return new WaitForSeconds(Inicio.length);
        _primeraTransicion = false;
        gameObject.SetActive(false);
    }

    public void CambiarEscena0()
    {
        gameObject.SetActive(true);
        StartCoroutine(Menu());
    }

    IEnumerator Menu()
    {
        _anim.SetTrigger("iniciar");
        yield return new WaitForSeconds(Final.length);
        SceneManager.LoadScene(0);
    }

    public void CambiarEscena1()
    {
        gameObject.SetActive(true);
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        _anim.SetTrigger("iniciar");
        yield return new WaitForSeconds(Final.length);
        SceneManager.LoadScene(1);
    }

    public void CambiarEscena2()
    {
        gameObject.SetActive(true);
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        _anim.SetTrigger("iniciar");
        yield return new WaitForSeconds(Final.length);
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        gameObject.SetActive(true);
        Application.Quit();
    }
}
