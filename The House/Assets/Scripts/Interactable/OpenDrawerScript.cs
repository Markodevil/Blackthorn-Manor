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
        // Raycasts in front of the player checking if their is a Drawer infront of the player 
        //
        // Param 
        //      Raycast : The raycast distance between the player and the drawer
        // Return 
        //      Changes the DrawerState so that the drawer can be opened
        //--------------------------------------------------------------------------------------
  
        Vector3 dresserPosition = Dresser.transform.position;
        Vector3 playerPosition = transform.position;

        Vector3 distance = playerPosition - dresserPosition;
        distance.Normalize();

        float dist = Vector3.Distance(playerPosition, dresserPosition);

        // checks if the player is in distance to open the Dresser     
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // When the player is in range and presses mouse1 
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit, interactDistance) && hit.collider.CompareTag("Drawer"))
        {
            // When true the camera cant be moved by mouse
            Dresser = hit.collider.gameObject;
            distance = transform.position - Dresser.transform.position;
            distance.y = 0;
            dist = distance.magnitude;
            isHoldingDown = true;
            fpsCamera.SetTouching(true);
            fpsCamera.SetTouchingDrawer(true);
            //Goes into DrawerScript and allows player to open drawers 
            hit.collider.transform.GetComponent<DrawerScript>().changeDrawerState();
            FindObjectOfType<GameManager>().hasTouchedDresser = true;

        }
        // if player is holding down mouse1 and moves away from the dresser
        // camera movement will be enabled 
        if (isHoldingDown && dist > 3)
        {
            isHoldingDown = false;
            fpsCamera.SetTouching(false);
            fpsCamera.SetTouchingDrawer(false);
        }


        //--------------------------------------------------------------------------------------
        // Checks if the Mouse0 button is up  
        //
        // Param 
        //     FPSCamera: Makes the fps Camera appear before the player
        // Return 
        //      Changes Camscript to true so player can check cameras 
        //--------------------------------------------------------------------------------------
        if (Input.GetKeyUp(KeyCode.Mouse0) && isHoldingDown)
        {
            fpsCamera.SetTouching(false);
            fpsCamera.SetTouchingDrawer(false);
        }

    }
}
