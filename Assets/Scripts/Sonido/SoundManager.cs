using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip lvl1Music;
    public AudioClip lvl1Boss;
    public AudioClip lvl2Music;
    public AudioClip lvl2Boss;
    public AudioClip lvl2Tree;

    private AudioSource source;


    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.buildIndex == 1)
        {
            source.clip = lvl1Music;
        }else if(currentScene.buildIndex == 3)
        {
            source.clip = lvl2Music;
        }

        source.Play();
        source.loop = true;
    }

    public void ChangeBGM(AudioClip newClip)
    {
        source.Stop(); // Det�n la m�sica actual
        source.clip = newClip; // Asigna el nuevo clip
        source.Play(); // Comienza a reproducir el nuevo clip
    }

    public void StopBGM()
    {
        source.Stop();
    }
}
