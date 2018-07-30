using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoorScript : MonoBehaviour {


     bool isOpen = false;
    public float mouseY;
    public float doorOpenSpeed; 
    public float interactDistance = 5;
    public GameObject Player;
    public Rigidbody rb; 
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mouseY = Input.GetAxis("Mouse Y");


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            isOpen = false;
        }

        Vector3 HingePosition = transform.position;
        Vector3 Playerposition = Player.transform.position;

        Vector3 Direction = HingePosition - Playerposition;

        Direction.Normalize();

        //--------------------------------------------------------------------------------------
        // Checks if the players posiition is infront or behind the door 
        //
        // Param 
        //     The direction of the player 
        // Return 
        //      Reverses the mouse axis when opening the door to give a more comfortable feel to  
        //      doors on either side 
        //--------------------------------------------------------------------------------------

   
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


