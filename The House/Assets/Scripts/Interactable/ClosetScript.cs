using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is attached to the closet to open 
public class ClosetScript : MonoBehaviour {

   // Gets closet Hinge properties 
    public HingeJoint hinge;
    public JointSpring hingeSpring;
    
    // Gets mouseY Axis 
    private float mouseY;

    // ClosetSound Cooldown
    float closetCoolDown = 0.65f;
    bool canPlaySound;

    // Opening and closing speed of closet 
    public float closetOpenSpeed;
   
    // Checks if closet is open 
    bool isOpen = false;
    // References Player Gameobject
    private GameObject Player;
    // Gets Closet RigidBody
    public Rigidbody rb;
    // Closet Audio 
    public AudioSource audio;
    public AudioClip closetSound;
    // Closet Audio Delay
    private float soundPlayTimer;

    //closes the closet when closeTimer is 0 
    bool closetSoundEnabled;
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
        // Gets the distance from the player to the Closet door 
        //
        // Param 
        //      Vector3.Distance : gets the position of the player and the Closet 
        // Return 
        //      checks if the player is at a certain distance to open the Closet 
        //--------------------------------------------------------------------------------------
        Vector3 closetPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;
        // closet position
        Vector3 Direction = closetPosition - playerPosition;
        Direction.Normalize();
        float dist = Vector3.Distance(playerPosition, closetPosition);

        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the closet when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
            isOpen = false;
            closetSoundEnabled = false;
        }
    // when the timer is up the closet can make a sound again 
      if (canPlaySound)
        {
            closetCoolDown -= Time.deltaTime;
        }
      // If closet is being opened toward or away from player 
      // it will play the ClosetSound 
      if (isOpen && mouseY == 0 && closetCoolDown <= 0)
        {
            closetSoundEnabled = true;
            canPlaySound = false;
            closetCoolDown = 0.65f;
        }
        if (closetSoundEnabled && mouseY > 2.5f)
        {
            audio.PlayOneShot(closetSound, 1);
            canPlaySound = true;
            closetSoundEnabled = false;
        }
        if (closetSoundEnabled && mouseY < -2.5f)
        {
            audio.PlayOneShot(closetSound, 1);
            canPlaySound = true;
            closetSoundEnabled = false;
        }

        // Adds force to the players forward direction to the closet which will open or close 
        // the closet depending on which side the player is located 
        if (isOpen)
        {
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward * mouseY * closetOpenSpeed, Player.transform.position);

        }


   
    }

    //--------------------------------------------------------------------------------------
    // Checks if the closetState should be changed (is accessed from OpenClosetScript)
    //
    // Param 
    //      Bool : changes isOpen from false to true 
    // Return 
    //    if the player interacts with the closet it will change the IsOpen bool to true
    //    which will make the player able to open the Closetdoor
    //     
    //--------------------------------------------------------------------------------------
    public void ChangeClosetState()
    {
        isOpen = !isOpen;
        PlayClosetSound();
    }
    void PlayClosetSound()
    {
        closetSoundEnabled = true;
    }

}

