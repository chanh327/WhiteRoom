using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ColishSound : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;

    void Awake()
    {

        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        source.PlayOneShot(clip);
    }
}