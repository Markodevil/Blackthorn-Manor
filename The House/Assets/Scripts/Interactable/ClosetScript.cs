using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This script is attached to the closet to open 
public class ClosetScript : MonoBehaviour {


    public HingeJoint hinge;
    public JointSpring hingeSpring;
    bool isOpen = false;
    public float mouseY;
    public float mouseX;

    public float closetOpenSpeed;
    private GameObject Player;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip closetSound;
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

        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
        //// Doors position 
        Vector3 closetPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;
        Vector3 Direction = closetPosition - playerPosition;
        Direction.Normalize();
        float dist = Vector3.Distance(playerPosition, closetPosition);

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");

        // lets go of the closet when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
            isOpen = false;
        }
  
      

        if (closetSoundEnabled && mouseY > 0)
        {
            // audio.pitch = 1;
            audio.PlayOneShot(closetSound, 1);
            closetSoundEnabled = false;
        }
        else if (closetSoundEnabled && mouseY < 0)
        {

            //audio.pitch = -1;
            audio.PlayOneShot(closetSound, 1);
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


       // if the player is in the forward position of the dresser you can open it using -mouseX 
       if (isOpen && Vector3.Dot(transform.forward, Direction) < 0)
       {
           Debug.Log("Front");
           if (isOpen && Vector3.Dot(transform.right, Direction) < 0)
           {
       
               rb.AddForceAtPosition(Player.transform.forward * -mouseX * closetOpenSpeed, Player.transform.position);
           }
           else
           {
       
               rb.AddForceAtPosition(Player.transform.forward * mouseX * -closetOpenSpeed, Player.transform.position);
       
           }
       }
        // if the player is in the -forward position of the dresser you can open it using -mouseX 
        if (isOpen && Vector3.Dot(transform.forward, Direction) > 0)
        {
            Debug.Log("Behind");

            if (isOpen && Vector3.Dot(transform.right, Direction) > 0)
            {

                rb.AddForceAtPosition(Player.transform.forward * -mouseX * closetOpenSpeed, Player.transform.position);
            }
            else
            {

                rb.AddForceAtPosition(-Player.transform.forward * mouseX * -closetOpenSpeed, Player.transform.position);

            }
        }
    }


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

