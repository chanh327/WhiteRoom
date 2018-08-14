using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class DoorSound : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip lockSound;
    public AudioClip unlockSound;
    public AudioClip failSound;
    public AudioClip lockButtonPushSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        if (!audioSource.isPlaying)
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

    public void PlayLockedSound()
    {
        PlaySound(lockSound);
    }

    public void PlayUnlockedSound()
    {
        PlaySound(unlockSound);
    }

    public void PlayFailSound()
    {
        PlaySound(failSound);
    }

    public void PlayLockButtonPushSound()
    {
        PlaySound(lockButtonPushSound);
    }
}