﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    //checks if drawer is open
    bool isOpen = false;
    // Gets mouse X and Y Axis
    public float mouseY;
    public float mouseX;
    // Opening and closing speed of drawer
    public float drawerSpeed;
    private GameObject Player;
    public Rigidbody rb;
    //DrawerSounds 
    public AudioSource audio;
    public AudioClip[] drawerSound;
    bool drawerSoundBool;
    // Picks Random DoorSound to add to the drawersound list
    private int RandomDrawerSound;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //fpsCamera = GetComponent<FPSCamera>();
    }
    // Use this for initialization
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ignores physics between player and dresser
        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
        //--------------------------------------------------------------------------------------
        // Gets the distance from the player to the Dresser
        //
        // Param 
        //      Vector3.Distance : gets the position of the player and the Dresser 
        // Return 
        //      checks if the player is at a certain distance to open the Dresser 
        //--------------------------------------------------------------------------------------
        Vector3 drawerPosition = transform.position;
        // Players position
        Vector3 playerPosition = Player.transform.position;
        // Gets the direction of the player from the drawer 
        Vector3 Direction = drawerPosition - playerPosition;
        Direction.Normalize();
        float dist = Vector3.Distance(playerPosition, drawerPosition);
        // Gets the Mouse Y and Mouse X coordinates 
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");
       
        // lets go of the drawer when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 2.65f)
        {
            isOpen = false;
            drawerSoundBool = false;

        }
        // if the drawer is being opened and on zero mouseY
        // make the drawer able to play a sound again 
        if (isOpen && mouseY == 0)
        {
            drawerSoundBool = true;

        }
        // when the drawer is being pushed it makes a sound 
        if (drawerSoundBool && mouseY > 1.5f)
        {
            audio.PlayOneShot(drawerSound[RandomDrawerSound], 1);
            drawerSoundBool = false;
        }
        // when the drawer is being pulled it makes a sound 

        if (drawerSoundBool && mouseY < -1.5f)
        {
            audio.PlayOneShot(drawerSound[RandomDrawerSound], 1);
            drawerSoundBool = false;
         
        }
        // Checks if can be opened and if player is positioned infront of the Dresser 
        if (isOpen && Vector3.Dot(transform.forward, Direction) > 0)
        {
            // Adds force from the players forward position to the drawer 
            rb.AddForceAtPosition(Player.transform.forward * mouseY * drawerSpeed, Player.transform.position);

        }
        // checks if player is on the right or left of the dresser
        // if on the right the player can open the dresser using mouseX 
        if (isOpen && Vector3.Dot(transform.right, Direction) < 0)
        {
            rb.AddForceAtPosition(Player.transform.forward * mouseX * drawerSpeed, Player.transform.position);
        }
        // if on the left the player can open the dresser using -mouseX 
        if (isOpen && Vector3.Dot(-transform.right, Direction) < 0)
        {
            rb.AddForceAtPosition(Player.transform.forward * -mouseX * drawerSpeed, Player.transform.position);
        }
     

    }
    // sets bool to true to play the drawer sound
    void playDrawerSound()
    {
        RandomDrawerSound = Random.Range(0, drawerSound.Length);
        audio.clip = drawerSound[RandomDrawerSound];
        drawerSoundBool = true;
    }
    // Sets isOpen to true and enables the drawer sound
    //--------------------------------------------------------------------------------------
    // Checks if the drawerstate should be changed (is accessed from OpenDrawerScript)
    //
    // Param 
    //      Bool : changes isOpen from false to true 
    // Return 
    //    if the player interacts with the drawer it will change the IsOpen bool to true
    //    which will make the player able to open and close the drawer
    //     
    //--------------------------------------------------------------------------------------
    public void changeDrawerState()
    {
        isOpen = !isOpen;
        playDrawerSound();
    }
    //--------------------------------------------------------------------------------------
    // Checks if the drawer is colliding with a requiured item 
    //
    // Param 
    //      transform.rotation = rotates the gameobject and makes it a child to the drawer 
    // Return 
    //   if the drawer is colliding with a required item it will rotate the item to fit
    //   in the drawer and will make it a child so that it moves with the drawer.
    //     
    //--------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.childCount != 0)
        {
 
            if (collision.gameObject.tag == "RequiredItem")
            {
                collision.transform.SetParent(transform.GetChild(0));
                transform.GetChild(0).Rotate(0, 0, 90);
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
  
    }
}