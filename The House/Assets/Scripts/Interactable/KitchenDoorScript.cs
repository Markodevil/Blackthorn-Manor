using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorScript : MonoBehaviour {


    public HingeJoint hinge;
    public JointSpring hingeSpring;
    bool isOpen = false;
    public float mouseY;
    public float mouseX;

    public float kitchenDoorSpeed;
    private GameObject Player;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip kitchenDoorSound;
    private float soundPlayTimer;

    bool kitchenDoorSoundEnabled;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
        hingeSpring.spring = 0;
        hinge.spring = hingeSpring;
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------------------------------
        // Gets the distance from the player to the KitchenDoor
        //
        // Param 
        //      Vector3.Distance : gets the position of the player and the KitchenDoor 
        // Return 
        //      checks if the player is at a certain distance to open the KitchenDoor 
        //--------------------------------------------------------------------------------------
        //// Doors position 
        Vector3 kitchenDoorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;
        Vector3 Direction = kitchenDoorPosition - playerPosition;
        Direction.Normalize();
        float dist = Vector3.Distance(playerPosition, kitchenDoorPosition);

        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");

        // lets go of the kitchenDoor when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
            isOpen = false;
        }


        if (kitchenDoorSoundEnabled && mouseY > 0)
        {
            // audio.pitch = 1;
            audio.PlayOneShot(kitchenDoorSound, 1);
            kitchenDoorSoundEnabled = false;
        }
        else if (kitchenDoorSoundEnabled && mouseY < 0)
        {

            //audio.pitch = -1;
            audio.PlayOneShot(kitchenDoorSound, 1);
            kitchenDoorSoundEnabled = false;
        }



        // Adds force to the players forward direction to the kitchenDoor which will open or close 
        // the kitchenDoor depending on which side the player is located 
        if (isOpen)
        {
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward * mouseY * kitchenDoorSpeed, Player.transform.position);

        }

        // checks if player is on the right or left of the dresser
        // if on the right the player can open the dresser using mouseX 
        if (isOpen && Vector3.Dot(transform.right, Direction) < 0)
        {

            if (isOpen && Vector3.Dot(transform.forward, Direction) < 0)
            {

                rb.AddForceAtPosition(Player.transform.forward * -mouseX * kitchenDoorSpeed, Player.transform.position);
            }
            else
            {

                rb.AddForceAtPosition(Player.transform.forward * mouseX * -kitchenDoorSpeed, Player.transform.position);

            }
        }
        // if on the left the player can open the dresser using -mouseX 
        if (isOpen && Vector3.Dot(transform.right, Direction) > 0)
        {
            Debug.Log("BEHIND OF DOOR");

            if (isOpen && Vector3.Dot(transform.forward, Direction) < 0)
            {
                Debug.Log("FORWARD front OF DOOR");

                rb.AddForceAtPosition(-Player.transform.forward * -mouseX * kitchenDoorSpeed, Player.transform.position);
            }
            else
            {

                rb.AddForceAtPosition(-Player.transform.forward * mouseX * -kitchenDoorSpeed, Player.transform.position);

            }

        }

    }

    //--------------------------------------------------------------------------------------
    // Checks if the KitchenDoorState should be changed (is accessed from OpenClosetScript)
    //
    // Param 
    //      Bool : changes isOpen from false to true 
    // Return 
    //    if the player interacts with the KitchenDoor it will change the IsOpen bool to true
    //    which will make the player able to open the door
    //     
    //--------------------------------------------------------------------------------------
    public void ChangekitchenDoorState()
    {
        isOpen = !isOpen;
        PlaykitchenDoorSound();
    }
    void PlaykitchenDoorSound()
    {
        kitchenDoorSoundEnabled = true;
    }

}

