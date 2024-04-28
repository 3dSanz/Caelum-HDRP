using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip lvl1Music;
    public AudioClip lvl1Boss;
    public AudioClip lvl2Music;
    public AudioClip lvl2Boss;

    private AudioSource source;


    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = lvl1Music;
        source.Play();
        source.loop = true;
    }

    public void ChangeBGM(AudioClip newClip)
    {
        source.Stop(); // Detén la música actual
        source.clip = newClip; // Asigna el nuevo clip
        source.Play(); // Comienza a reproducir el nuevo clip
    }

    public void StopBGM()
    {
        source.Stop();
    }
}
