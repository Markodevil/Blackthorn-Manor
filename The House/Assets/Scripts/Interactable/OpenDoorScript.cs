using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour {


    public float interactDistance = 5;
    FPSCamera fpsCamera;
    public MonoBehaviour camScript;

	// Use this for initialization
	void Start () {
        fpsCamera = GetComponent<FPSCamera>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKey(KeyCode.Mouse0))
        {
            // Disable Mouse Rotation when opening door

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit,interactDistance))
            {

                if (hit.collider.CompareTag("Door"))
                {
                    //fpsCamera.IsPeeking = true;
                    hit.collider.transform.parent.GetComponent<DoorScript>().changeDoorState();
                    camScript.enabled = false;

                }

            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            camScript.enabled = true;
        }
        //--------------------------------------------------------------------------------------
        // Raycasts in front of the player checking if their is a Door infront of the player 
        //
        // Param 
        //      Direction: the direction in which i want to check if there is a door 
        // Return 
        //      Changes the doorState so that the door can be opened
        //--------------------------------------------------------------------------------------

    }
}

