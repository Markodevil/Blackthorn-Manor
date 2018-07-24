using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isOpen = false;
    public float doorOpenAngle;
    float doorCloseAngle = 0;
    public float smooth = 2;
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
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            
            isOpen = false;
        }

        Vector3 HingeLocation = transform.position;
        Vector3 Playerposition = Player.transform.position;

        Vector3 Direction = HingeLocation = Playerposition;

        Vector3.Normalize(Direction);

        // If Direction.Player.transform.right > 0 
        if (Vector3.Dot(Direction, Player.transform.right) < 0)
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
