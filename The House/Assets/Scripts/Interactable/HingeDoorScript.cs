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

        Vector3 Direction = HingePosition = Playerposition;

        Vector3.Normalize(Direction);

        //--------------------------------------------------------------------------------------
        // Checks if the players posiition is infront or behind the door 
        //
        // Param 
        //     The direction of the player 
        // Return 
        //      Reverses the mouse axis when opening the door to give a more comfortable feel to  
        //      doors on either side 
        //--------------------------------------------------------------------------------------


        //MAKE SURE DOORS X AXIS IS FACING THE PLAYER WHEN FIRST OPENING THE DOOR 
        if (Vector3.Dot(Direction, Player.transform.right) > 0)
        {
         //   rb.AddTorque(transform.up * mouseY);

            Debug.Log("ZERO");
            if (isOpen && mouseY > 0)
            {
                //Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
                //transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, mouseY * Time.deltaTime * doorOpenSpeed);
                rb.AddForce(-transform.forward * mouseY * doorOpenSpeed);
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAND OPEN");

            }


            if (isOpen && mouseY < 0)
            {
                // Quaternion targetRotation2 = Quaternion.Euler(0, doorOpenAngle, 0);
                // transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, -mouseY * Time.deltaTime * doorOpenSpeed);
                rb.AddForce(transform.forward * -mouseY * doorOpenSpeed);

            }


        }
        else
        {
            Debug.Log("Greater Than Zero");
            if (isOpen && mouseY > 0)
            {
                rb.AddForce(transform.forward * mouseY * doorOpenSpeed);

            }


            if (isOpen && mouseY < 0)
            {
                rb.AddForce(transform.forward * mouseY * doorOpenSpeed);
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAND CLOSED");

            }
        }
    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
    }

}


