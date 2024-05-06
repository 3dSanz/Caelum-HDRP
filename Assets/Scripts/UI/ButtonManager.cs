using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private AudioSource bgmSource;
    private AudioSource sfxSource;
    private TransicionEscena _tescena;

    void Awake()
    {
        bgmSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
        _tescena = GameObject.Find("TransicionEscena").GetComponent<TransicionEscena>();
    }

    public void PlayGame()
    {
        _tescena.CambiarEscena1();
    }

    public void ExitGame()
    {
        _tescena.ExitGame();
    }

    public void MenuGame()
    {
        _tescena.CambiarEscena0();
    }

    /*public void DeathGame()
    {
        SceneManager.LoadScene(2);
    }*/

    public void MusicVolume(float value)
    {
        bgmSource.volume = value;
    }

    public void SFXVolume(float value)
    {
        sfxSource.volume = value;
    }
}
