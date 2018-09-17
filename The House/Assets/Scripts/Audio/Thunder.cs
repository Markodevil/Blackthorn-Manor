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

    public Quaternion spawnRot;

    float destroyObject = 0.2f;


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
            Transform randomSpawn = lightningSpawns[Random.Range(0, lightningSpawns.Length)];
            objectThingy = Instantiate(lightningObject, randomSpawn.position, randomSpawn.rotation);
            playThunder = true;
            lightningTimer = lightningDelay;
        }

        if (objectThingy)
        {
            destroyObject -= Time.deltaTime;
            if (destroyObject <= 0)
            {
                //Destroy(objectThingy);
                //destroyObject = 0.2f;
            }
        }

        if (playThunder)
        {
            thunderTimer -= Time.deltaTime;
            if (thunderTimer <= 0)
            {
                audioSource.PlayOneShot(thunderSounds[Random.Range(0, thunderSounds.Length)]);
                //playThunder = false;
                thunderTimer = thunderDelay;
            }
        }
    }
}
