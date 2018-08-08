﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoorScript : MonoBehaviour {


     bool isOpen = false;
    public float mouseY;
    public float doorOpenSpeed; 
    private GameObject Player;
    public Rigidbody rb;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {       
        //// Doors position 
        Vector3 doorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;

        float dist = Vector3.Distance(playerPosition, doorPosition);

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the door when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 4)
        {
            isOpen = false;
        }

        // Adds force to the players forward direction to the door which will open or close 
        // the door depending on which side the player is located 
        if (isOpen)
        {
                rb.AddForceAtPosition(Player.transform.forward * mouseY * doorOpenSpeed, Player.transform.position);
        }
       
    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
    }

}


