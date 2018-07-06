using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour{
    public static SoundManager instance = null;
    public AudioMixerSnapshot mute;
    public AudioMixerSnapshot unmute;
    
    public AudioSource source;
    public AudioClip[] musicClips;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        source.loop = true;
        //임시 음악 재생
        PlayMusic();
    }

    void Mute()
    {
        mute.TransitionTo(0.1f);
    }

    void UnMute()
    {
        unmute.TransitionTo(0.1f);
    }

    void PlayMusic(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    void PlayMusic()
    {
        source.PlayOneShot(musicClips[0]);
    }
}