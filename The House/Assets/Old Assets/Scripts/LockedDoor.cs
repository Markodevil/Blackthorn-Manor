using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public GameObject LockedDoorText;
    public AudioSource LockedDoorSound;

    // Use this for initialization
    void Start()
    {

    }

// Update is called once per frame
void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey (KeyCode.E))
            {
                LockedDoorText.SetActive(true);
                Debug.Log("Pressing E");
                LockedDoorSound.Play();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            LockedDoorText.SetActive(false);
        }
    }
}
