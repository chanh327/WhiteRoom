using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class DoorSound : MonoBehaviour{
    
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(clip);
    }

    public void PlayOpenSound()
    {
        PlaySound(openSound);
    }

    public void PlayCloseSound()
    {
        PlaySound(closeSound);
    }


}