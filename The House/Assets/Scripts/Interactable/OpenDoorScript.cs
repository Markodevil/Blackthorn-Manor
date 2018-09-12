using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the player so that the door can be opened 
public class OpenDoorScript : MonoBehaviour {

    // Distance in which the player can interact with the door 
    public float interactDistance;
    // Refrences the Camera so it can be locked 
    FPSCamera fpsCamera;
    private GameObject Door;
    public MonoBehaviour camScript;
    bool isHoldingDown = false;
    public bool isLocked = true;

	// Use this for initialization
	void Start () {
        fpsCamera = GetComponent<FPSCamera>();
        Door = GameObject.FindGameObjectWithTag("Door");
    }
	
	// Update is called once per frame
	void Update () {
        if (isLocked)
            return;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward,Color.green);

        //Distance from the door to the player 
        Vector3 distance = transform.position - Door.transform.position;
        distance.y = 0;
        // gets the magnitude of players position and doors position 
        float dist = distance.magnitude;
        //--------------------------------------------------------------------------------------
        // Raycasts in front of the player checking if their is a Door infront of the player 
        //
        // Param 
        //      Raycast : The raycast distance between the player and the drawer
        // Return 
        //      Changes the doorState so that the door can be opened
        //--------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit, interactDistance))
        //    (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(ray, out hit, interactDistance)))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Door = hit.collider.gameObject;
                distance = transform.position - Door.transform.position;
                distance.y = 0;
                dist = distance.magnitude;
                isHoldingDown = true;
            //    fpsCamera.SetTouching(true);
                //Goes into HingeDoorScript and allows player to open doors 
                hit.collider.transform.GetComponent<HingeDoorScript>().ChangeDoorState();
               // hit.collider.transform.GetComponent<DoorScript>().changeDoorState();

            }
        }

    }
}

