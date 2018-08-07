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

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        float dist = Vector3.Distance(Dresser.transform.position, transform.position);
        // checks if the player is in distance to open the door 
       
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit, interactDistance) )
        {
            if (hit.collider.CompareTag("Drawer"))
            {
                // When true the camera cant be moved by mouse
                fpsCamera.SetTouching(true);
                //Goes into HingeDoorScript and allows player to open doors 
                hit.collider.transform.GetComponent<DrawerScript>().changeDrawerState();
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
            //camScript.enabled = true;
            fpsCamera.SetTouching(false);
        }

    }
}
