using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawerScript : MonoBehaviour
{

    // Distance in which the player can interact with the door 
    public float interactDistance;

    // Refrences the Camera so it can be locked 
    FPSCamera fpsCamera;
    private GameObject Dresser;
    public MonoBehaviour camScript;
    bool isHoldingDown = false;
    // Use this for initialization
    void Start()
    {
        fpsCamera = GetComponent<FPSCamera>();
        Dresser = GameObject.FindGameObjectWithTag("Drawer");
    }

    // Update is called once per frame
    void Update()
    {

        //--------------------------------------------------------------------------------------
        // Raycasts in front of the player checking if their is a Door infront of the player 
        //
        // Param 
        //      Direction: the direction in which i want to check if there is a door 
        // Return 
        //      Changes the DrawerState so that the door can be opened
        //--------------------------------------------------------------------------------------

        Vector3 distance;
        distance.y = 0;
        float dist = 0.0f;

        // checks if the player is in distance to open the Dresser     
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit, interactDistance) && hit.collider.CompareTag("Drawer"))
        {
            // When true the camera cant be moved by mouse
            Dresser = hit.collider.gameObject;
            distance = transform.position - Dresser.transform.position;
            distance.y = 0;
            dist = distance.magnitude;
            isHoldingDown = true;
            fpsCamera.SetTouching(true);
            //Goes into HingeDoorScript and allows player to open doors 
            hit.collider.transform.GetComponent<DrawerScript>().changeDrawerState();

        }

        // if player is holding down mouse1 and moves away from the dresser
        // camera movement will be enabled 
        if (isHoldingDown && dist > 2)
        {
            isHoldingDown = false;
            fpsCamera.SetTouching(false);
        }


        //--------------------------------------------------------------------------------------
        // Checks if the Mouse0 button is up  
        //
        // Param 
        //      Determines if Camscript is true 
        // Return 
        //      Changes Camscript to true so player can check cameras 
        //--------------------------------------------------------------------------------------
        if (Input.GetKeyUp(KeyCode.Mouse0) && isHoldingDown)
        {
            //camScript.enabled = true;
            fpsCamera.SetTouching(false);
        }

    }
}
