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
        //source.PlayOneShot(clip);
        StartCoroutine(CoPlayMusic(clip));
    }

    private IEnumerator CoPlayMusic(AudioClip clip)
    {
        yield return new WaitForSeconds(2.5f);
        source.clip = clip;
        source.Play();
    }

    public void PlayBGM(int scenidx)
    {
        StopMusic();
        PlayMusic(bgms[scenidx-1]);
    }

    private void StopMusic()
    {
        source.Stop();
    }
}