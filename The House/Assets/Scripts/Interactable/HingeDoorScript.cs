using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is attached to the door to open 
public class HingeDoorScript : MonoBehaviour {

    public HingeJoint hinge;
    public JointSpring hingeSpring;
    bool isOpen = false;
    public float mouseY;
    public float doorOpenSpeed;
    private GameObject Player;
    private GameObject Ghost;
    private NavMeshAgent ghostAgent;
    public GameObject SpawnDoor;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip doorCreakSound; 
    private float soundPlayTimer;
    // Sets the value for closeTimer
    public float doorCloseTime; 
    // Timer until door closes 
    private float closeTimer;
    //closes the door when closeTimer is 0 
    public bool closeDoor = false;
    bool doorSoundEnabled;
    bool DoorSoundCooldown = false;
    bool GhostOpenDoor = false;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Ghost = GameObject.FindGameObjectWithTag("Ghost");
        ghostAgent = Ghost.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //Sets the doorClostTime to closetimer 
        closeTimer = doorCloseTime;
        //Sets the doorHinge at 0 on start of the game 
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
        hingeSpring.spring = 0;
        hinge.spring = hingeSpring;
    }

    // Update is called once per frame
    void Update()
    {
        // ignores collision between player and Door
        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
        //// Doors position 
        Vector3 doorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;
        // Distance from player to door 
        float dist = Vector3.Distance(playerPosition, doorPosition);
        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");
        
        // lets go of the door when Mouse0 is released 
       // if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.E) || dist > 3.5f)
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
               isOpen = false;
           
        }
        if (closeDoor)
        {
            closeTimer -= Time.deltaTime;
        }
        //--------------------------------------------------------------------------------------
        // Checks if the closeTimer is lessthan zero and closeDoor is true
        //
        // Param 
        //      Determines if the door can close 
        // Return 
        //      Door spring is enabled and set to 5 and the door begins to close 
        //--------------------------------------------------------------------------------------
        // Doors spring returns door back to starting position 
        if (closeTimer <= 0 && closeDoor == true)
        {
            // Sets spring to 5 which returns door back to its original position 
            hingeSpring.spring = 5;
            hinge.spring = hingeSpring;
            // Enables the use of hinge springs 
            hinge.useSpring = true;
            closeDoor = false;
            DoorSoundCooldown = false;
        }
        
        // Door makes sound when opened
        if (doorSoundEnabled && Input.GetKey(KeyCode.Mouse0) && DoorSoundCooldown == false )
           // || doorSoundEnabled && Input.GetKey(KeyCode.E) && DoorSoundCooldown == false)
        {
            // audio.pitch = 1;
            audio.Play();
            // Stops the door sound from playing more than once
            doorSoundEnabled = false;
            DoorSoundCooldown = true;
            //checks if there is a door in the scene that is the spawn door
            if (SpawnDoor != null)
            {
                //|=turns on the bit/mask, ^= toggles the bit/mask
                //navMesh.getareafromname gives us the amount of bits we need to shift to get te mask we want 
                //NOT the actual mask
                ghostAgent.areaMask |= (1 << NavMesh.GetAreaFromName("Spawn"));
            }
        }

        // Adds force to the players forward direction to the door which will open or close 
        // the door depending on which side the player is located 
        if (isOpen)
        {
            //Sets the spring to 0 on open to make the door open easier
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            // Adds force from the players forward position towards the door
            rb.AddForceAtPosition(Player.transform.forward *  doorOpenSpeed * 13, Player.transform.position);
            closeTimer = doorCloseTime;
            closeDoor = true;
            
        }
        if (GhostOpenDoor)
        {
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            closeTimer = doorCloseTime;
            closeDoor = true;
            GhostOpenDoor = false;
        }
    }
    // Sets isopen to true and enables the doorSound
    public void ChangeDoorState()
    {
        isOpen = !isOpen;
        PlayDoorSound();
    }
    // sets bool to true to play door sound
    void PlayDoorSound()
    {
        doorSoundEnabled = true;
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            //Debug.Log("Ghost Opened Door");
            GhostOpenDoor = true;

        }
    }
}
