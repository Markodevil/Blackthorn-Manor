using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //// Doors position 
        Vector3 doorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;

        float dist = Vector3.Distance(playerPosition, doorPosition);
   
        // Counts down for the door to close     

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");
        
        // lets go of the door when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
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
            Debug.Log("timer is negative again");
            hingeSpring.spring = 5;
            hinge.spring = hingeSpring;
            hinge.useSpring = true;
            closeDoor = false;
            audio.PlayOneShot(doorCreakSound, 1);
            Debug.Log("Played Closing Sound");

        }

        // Adds force to the players forward direction to the door which will open or close 
        // the door depending on which side the player is located 
        if (isOpen)
        {
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward * mouseY * doorOpenSpeed, Player.transform.position);
            closeTimer = doorCloseTime;
            closeDoor = true;

        }

    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
        playDoorSound();
    }
    void playDoorSound()
    {
        Debug.Log("DoorSoundPLays");
        audio.PlayOneShot(doorCreakSound, 1);
    }

}


