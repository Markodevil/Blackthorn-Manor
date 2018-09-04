using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip[] Sounds;
    private AudioClip playSound;
    int soundIndex;
    public float SoundTimer;
    private int SoundTimerReset = 20;
    bool RandomiseTimer = true;
    bool PlaySoundEffect = false;
    // Update is called once per frame
    void Update () {
	
        if (RandomiseTimer)
        {
            SoundTimer = Random.Range(3, SoundTimerReset);
            RandomiseTimer = false;
        }
        SoundTimer -= Time.deltaTime;

        if (SoundTimer <= 0)
        {
            soundIndex = Random.Range(0, Sounds.Length);
            playSound = Sounds[soundIndex];
            audioSource.clip = playSound;
            PlaySoundEffect = true;
            RandomiseTimer = true;
        }

        if (PlaySoundEffect)
        {
            audioSource.Play();
            PlaySoundEffect = false;
        }
    }
}
