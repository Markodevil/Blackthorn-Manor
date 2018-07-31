using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour {

    // Distance in which the player can interact with the door 
    public float interactDistance;
    // Refrences the Camera so it can be locked 
    FPSCamera fpsCamera;

    public MonoBehaviour camScript;

	// Use this for initialization
	void Start () {
        fpsCamera = GetComponent<FPSCamera>();

    }
	
	// Update is called once per frame
	void Update () {
		
        //--------------------------------------------------------------------------------------
        // Raycasts in front of the player checking if their is a Door infront of the player 
        //
        // Param 
        //      Direction: the direction in which i want to check if there is a door 
        // Return 
        //      Changes the doorState so that the door can be opened
        //--------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,interactDistance))
            {

                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.transform.GetComponent<HingeDoorScript>().changeDoorState();

                    camScript.enabled = false;

                }

            }
        }
        //--------------------------------------------------------------------------------------
        // Checks if the Mouse0 button is up  
        //
        // Param 
        //      Determines if Camscript is true 
        // Return 
        //      Changes Camscript to true so player can check cameras 
        //--------------------------------------------------------------------------------------
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            camScript.enabled = true;
        }

    }
}

