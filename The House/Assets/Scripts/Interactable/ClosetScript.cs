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
    public float closetOpenSpeed;
    private GameObject Player;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip closetSound;
    private float soundPlayTimer;
    // Sets the value for closeTimer
    public float closetCloseTime;
    // Timer until closet closes 
    public float closeTimer;
    //closes the closet when closeTimer is 0 
    bool closeCloset = false;
    bool closetSoundEnabled;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        closeTimer = closetCloseTime;
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

        float dist = Vector3.Distance(playerPosition, closetPosition);

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the closet when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
            isOpen = false;
        }
        if (closeCloset)
        {
            closeTimer -= Time.deltaTime;

        }
        // Doors spring returns closet back to starting position 
        if (closeTimer <= 0 && closeCloset == true)
        {
            hingeSpring.spring = 5;
            hinge.spring = hingeSpring;
            hinge.useSpring = true;
            closeCloset = false;
            //  audio.PlayOneShot(doorCreakSound, 1);
            //  Debug.Log("Played Closing Sound");

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
            Debug.Log("ISOPEN");
            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward * mouseY * closetOpenSpeed, Player.transform.position);
            closeTimer = closetCloseTime;
            closeCloset = true;

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

