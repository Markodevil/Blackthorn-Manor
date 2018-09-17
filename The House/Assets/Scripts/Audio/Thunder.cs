using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] thunderSounds;
    public GameObject lightningObject;
    public Transform[] lightningSpawns;

    public float lightningDelay;
    public float thunderDelay;

    float lightningTimer;
    float thunderTimer;

    bool playThunder = false;
    GameObject objectThingy;


    // Use this for initialization
    void Start()
    {
        lightningTimer = lightningDelay;
        thunderTimer = thunderDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playThunder)
            lightningTimer -= Time.deltaTime;
        if (lightningTimer <= 0)
        {
            if (objectThingy != null)
                Destroy(objectThingy);
            objectThingy = Instantiate(lightningObject, lightningSpawns[Random.Range(0, lightningSpawns.Length)].position, Quaternion.identity);
            playThunder = true;
            lightningTimer = lightningDelay;
        }

        if (playThunder)
        {
            thunderTimer -= Time.deltaTime;
            if (thunderTimer <= 0)
            {
                audioSource.PlayOneShot(thunderSounds[Random.Range(0, thunderSounds.Length)]);
                playThunder = false;
                thunderTimer = thunderDelay;
            }
        }
    }
}
