using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; }
    AudioSource _audio;
    public AudioClip playerJump;
    public AudioClip deathSound;
    public AudioClip dashSound;
    public AudioClip airHitSound;
    public AudioClip enemyHitSound;
    public AudioClip objectHitSound;
    public AudioClip enemyShootPlayerHit;
    public AudioClip enemyHitPlayerSound;
    public AudioClip reboteDisparo;
    public AudioClip cogerChatarra;

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
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Singleton pattern: Ensure only one instance of AudioManager exists
    public static SFXManager instance;

    // Audio clips for different sound effects
    public AudioClip playerJump;
    public AudioClip deathSound;
    // ... Agrega otros clips de sonido seg�n tus necesidades ...

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear un objeto AudioSource para reproducir los sonidos
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // M�todo para reproducir un sonido espec�fico
    public void PlaySound(string soundId)
    {
        switch (soundId)
        {
            case "playerJump":
                audioSource.PlayOneShot(playerJump);
                break;
            case "deathSound":
                audioSource.PlayOneShot(deathSound);
                break;
            default:
                Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
                break;
        }
    }
}*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance { get; private set; }
    private AudioSource _audio;
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    public AudioClip playerJump;
    public AudioClip deathSound;
    public AudioClip pickupCoin;
    public AudioClip caminar;
    public AudioClip explosion;
    public AudioClip hitHurt;
    public AudioClip success;
    public AudioClip attack;
    public AudioClip dash;
    public AudioClip enemyShoot;

    // Start is called before the first frame update
    void Awake()
    {
        _audio = GetComponent<AudioSource>();

        // Asigna los clips de sonido a los identificadores
        soundDictionary["playerJump"] = playerJump;
        soundDictionary["deathSound"] = deathSound;
        soundDictionary["pickupCoin"] = pickupCoin;
        soundDictionary["caminar"] = caminar;
        soundDictionary["explosion"] = explosion;
        soundDictionary["hitHurt"] = hitHurt;
        soundDictionary["success"] = success;
        soundDictionary["attack"] = attack;
        soundDictionary["dash"] = dash;
        soundDictionary["enemyShoot"] = enemyShoot;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaySound(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            _audio.PlayOneShot(soundDictionary[soundId]);
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
        }
    }

    public void PlayOnce(string soundId)
    {
        _audio.PlayOneShot(soundDictionary[soundId]);
    }

    public bool IsPlaying(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            return _audio.isPlaying;
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
            return false;
        }
    }

    public void StopSound(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            _audio.Stop();
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
        }
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>(); // Usaremos una lista de AudioSources
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    public AudioClip playerJump;
    public AudioClip deathSound;
    public AudioClip caminar;
    // Agrega otros clips de sonido seg�n tus necesidades

    private void Awake()
    {
        // Crea varios AudioSources (puedes ajustar la cantidad seg�n tus necesidades)
        for (int i = 0; i < 3; i++)
        {
            var newSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(newSource);
        }

        // Asigna los clips de sonido a los identificadores
        soundDictionary["playerJump"] = playerJump;
        soundDictionary["deathSound"] = deathSound;
        soundDictionary["caminar"] = caminar;

        // Reproduce los sonidos en bucle
        foreach (var source in audioSources)
        {
            source.loop = true;
            source.Play();
        }
    }

    public void PlaySound(string soundId)
    {
        // Encuentra un AudioSource disponible y reproduce el sonido
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(GetClipById(soundId));
                break;
            }
        }
    }

    public bool IsPlaying(string soundId)
    {
        // Verifica si alg�n AudioSource est� reproduciendo el sonido
        foreach (var source in audioSources)
        {
            if (source.isPlaying && GetClipById(soundId) == source.clip)
            {
                return true;
            }
        }

        Debug.LogWarning($"El sonido con el identificador '{soundId}' no est� en reproducci�n.");
        return false;
    }

    public void StopSound(string soundId)
    {
        // Det�n el sonido asociado al identificador proporcionado
        foreach (var source in audioSources)
        {
            if (source.isPlaying && GetClipById(soundId) == source.clip)
            {
                source.Stop();
                break;
            }
        }
    }

    private AudioClip GetClipById(string soundId)
    {
        // Implementa la l�gica para obtener el clip seg�n el identificador
        // Por ejemplo, puedes usar un diccionario similar al ejemplo anterior
        // o cualquier otra estructura de datos que prefieras
        // Devuelve el clip correspondiente al identificador
        if (soundDictionary.ContainsKey(soundId))
        {
            return soundDictionary[soundId];
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
            return null;
        }
    }
}*/
