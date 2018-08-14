using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour{
    public static SoundManager instance = null;
    public AudioMixerSnapshot mute;
    public AudioMixerSnapshot unmute;

    private bool musicOn;
    
    public AudioSource source;
    public AudioClip[] bgms;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        musicOn = false;
    }

    void Start()
    {
        source.loop = true;
        PlayBGM(0);
    }

    public void Mute()
    {
        mute.TransitionTo(0.1f);
    }

    public void UnMute()
    {
        unmute.TransitionTo(0.1f);
    }

    private void PlayMusic(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void PlayBGM(int scenidx)
    {
        StopMusic();
        PlayMusic(bgms[scenidx]);
    }

    private void StopMusic()
    {
        source.Stop();
    }
}