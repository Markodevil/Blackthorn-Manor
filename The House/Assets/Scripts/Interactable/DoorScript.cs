using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isOpen = false;
    public float doorOpenAngle;
    float doorCloseAngle = 0;
    public float mouseY;
    public bool OpeningDoor = false;
    public float interactDistance = 5;
    public float doorOpenSpeed;
    public GameObject Player; 

    // Use this for initialization
    void Start()
    {
        doorCloseAngle = transform.rotation.eulerAngles.y;
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

            Debug.Log("ZERO");
            if (isOpen && mouseY > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, mouseY * Time.deltaTime * doorOpenSpeed);
            }


            if (isOpen && mouseY < 0)
            {
                Quaternion targetRotation2 = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, -mouseY * Time.deltaTime * doorOpenSpeed);
            }


        }
        else
        {
            Debug.Log("Greater Than Zero");
            if (isOpen && mouseY > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, mouseY * Time.deltaTime * doorOpenSpeed);
            }
            
            
            if (isOpen && mouseY < 0)
            {
                Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, -mouseY * Time.deltaTime * doorOpenSpeed);
            }
        }
    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
    }

}
