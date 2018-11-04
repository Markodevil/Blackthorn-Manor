using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip[] Sounds;
    private AudioClip playSound;
    int soundIndex;
    public float soundTimer;
    public int setSoundTimer;
    public float minimumSoundTime;
    bool randomiseTimer = true;
    bool playSoundEffect = false;
    // Update is called once per frame
    void Update () {
	
        // gets a random number from the set variables to be u
        if (randomiseTimer)
        {
            soundTimer = Random.Range(minimumSoundTime, setSoundTimer);
            randomiseTimer = false;
        }
        soundTimer -= Time.deltaTime;
        // if the SoundTimer is zero play random sound
        if (soundTimer <= 0)
        {
            soundIndex = Random.Range(0, Sounds.Length);
            playSound = Sounds[soundIndex];
            audioSource.clip = playSound;
            playSoundEffect = true;
            randomiseTimer = true;
        }
        // plays the sound
        if (playSoundEffect)
        {
            audioSource.Play();
            playSoundEffect = false;
        }
    }
}
