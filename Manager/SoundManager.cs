using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioMixerSnapshot musicMute;
    public AudioMixerSnapshot musicUnMute;
    public AudioMixerSnapshot effectMute;
    public AudioMixerSnapshot effectUnMute;

    public AudioSource source;
    public AudioClip[] bgms;

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
        PlayBGM(0);
    }

    public void MusicMute()
    {
        musicMute.TransitionTo(0.1f);
    }

    public void MusicUnMute()
    {
        musicUnMute.TransitionTo(0.1f);
    }

    public void EffectMute()
    {
        effectMute.TransitionTo(0.1f);
    }

    public void EffectUnMute()
    {
        effectUnMute.TransitionTo(0.1f);
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