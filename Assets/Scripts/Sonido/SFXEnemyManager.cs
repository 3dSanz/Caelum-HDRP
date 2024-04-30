using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEnemyManager : MonoBehaviour
{
    public static SFXEnemyManager instance { get; private set; }
    AudioSource _audio;
    public AudioClip enemyShoot;
    public AudioClip enemyHitPlayer;
    public AudioClip enemyDeath;
    public AudioClip bulletCollide;
    public AudioClip bulletInit;

    // Start is called before the first frame update

    void Awake()
    {
        _audio = GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _audio.Stop();
        _audio.PlayOneShot(clip);
    }

    public void StopSound()
    {
        _audio.Stop();
    }

}