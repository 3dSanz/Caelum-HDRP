using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    void Awake()
    {
        bgmSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFXManager").GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MenuGame()
    {
        SceneManager.LoadScene(0);
    }

    public void DeathGame()
    {
        SceneManager.LoadScene(2);
    }

    public void MusicVolume(float value)
    {
        bgmSource.volume = value;
    }

    public void SFXVolume(float value)
    {
        sfxSource.volume = value;
    }
}
