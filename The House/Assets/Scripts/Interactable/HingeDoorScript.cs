using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the door to open 
public class HingeDoorScript : MonoBehaviour {

    public HingeJoint hinge;
    public JointSpring hingeSpring;
    bool isOpen = false;
    public float mouseY;
    public float doorOpenSpeed;
    private GameObject Player;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip doorCreakSound; 
    private float soundPlayTimer;
    // Sets the value for closeTimer
    public float doorCloseTime; 
    // Timer until door closes 
    private float closeTimer;
    //closes the door when closeTimer is 0 
    bool closeDoor = false;
    bool doorSoundEnabled;
    bool DoorSoundCooldown = false;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        closeTimer = doorCloseTime;
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
        hingeSpring.spring = 0;
        hinge.spring = hingeSpring;
    }

    // Update is called once per frame
    void Update()
    {

        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
        //// Doors position 
        Vector3 doorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;

        float dist = Vector3.Distance(playerPosition, doorPosition);
   
        // Counts down for the door to close     

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");
        
        // lets go of the door when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.E) || dist > 3.5f)
        {
            isOpen = false;
        }
        if (closeDoor)
        {
            closeTimer -= Time.deltaTime;

        }
        // Doors spring returns door back to starting position 
        if (closeTimer <= 0 && closeDoor == true)
        {
            hingeSpring.spring = 5;
            hinge.spring = hingeSpring;
            hinge.useSpring = true;
            closeDoor = false;
            DoorSoundCooldown = false;
          //  audio.PlayOneShot(doorCreakSound, 1);
          //  Debug.Log("Played Closing Sound");

        }
        
        if (doorSoundEnabled && Input.GetKey(KeyCode.Mouse0) && DoorSoundCooldown == false 
            || doorSoundEnabled && Input.GetKey(KeyCode.E) && DoorSoundCooldown == false)
        {
            Debug.Log("DoorSoundPlayed");
            // audio.pitch = 1;
            audio.Play();
            doorSoundEnabled = false;
            DoorSoundCooldown = true;

        }

        // Adds force to the players forward direction to the door which will open or close 
        // the door depending on which side the player is located 
        if (isOpen)
        {
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward *  doorOpenSpeed * 10, Player.transform.position);
            closeTimer = doorCloseTime;
            closeDoor = true;
        }

    }

    public void ChangeDoorState()
    {
        isOpen = !isOpen;
        PlayDoorSound();
    }
    void PlayDoorSound()
    {
        doorSoundEnabled = true;
    }

}
