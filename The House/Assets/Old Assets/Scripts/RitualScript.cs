using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualScript : MonoBehaviour
{
    public int NumberOfObjects;
    public GameObject ritualObjects;
    public GameObject ghost;
    public AudioSource music;
    public GameObject messageGoToBed;
    public GameObject TriggerSleep;
    public AudioClip candleFlame;
    public GameObject circleGlow;
    public GameObject suppliesMessage;
    public GameObject bedroomLight;

    // Use this for initialization
    void Start ()
    {
        TriggerSleep.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (NumberOfObjects == 3)
        {
            // turn off message trigger
            suppliesMessage.SetActive(false);
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
         if (NumberOfObjects == 3)
            {
                ritualObjects.SetActive(true);
                NumberOfObjects = 4;

                // deactivate the ghost
                ghost.SetActive(false);

                // turn off the music
                music.enabled = false;

                // play sound of candles lighting
                AudioSource audio = GetComponent<AudioSource>();
                audio.PlayOneShot(candleFlame);
                // turn on go to bed message
                messageGoToBed.SetActive(true);
                StartCoroutine("WaitForDelay");

                // turn on sleep message
                TriggerSleep.SetActive(true);

                // turn off glow tutorial
                //circleGlow.SetActive(false);

                // turn on bedroom light
                bedroomLight.SetActive(true);
                


                
            }
        }
    }
    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(messageGoToBed);

    }
}
