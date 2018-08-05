
using UnityEngine;
using System.Collections;

public class Jumpscare : MonoBehaviour
{

    public AudioClip SoundToPlay;
    public float Volume;
    AudioSource soundSource;
    public bool alreadyPlayed = false;
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter()
    {
        if (!alreadyPlayed)
        {
            soundSource.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }
    }
}