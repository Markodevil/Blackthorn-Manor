using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public bool isOpen = false;
    public float doorOpenAngle;
    public float doorCloseAngle ;
    public float mouseY;
    public bool OpeningDoor = false;
    public float interactDistance = 5;
    public float doorOpenSpeed;
    private float doorDelayTime = 0.55f;
    public GameObject Player;
    bool OpenedRight;
    bool OpenedLeft;
    bool OpenZeroRotation;
    bool DoorClosed;
    bool DelayTimer;
    int doorOpenToggle;
    // Use this for initialization
    void Start()
    {
        doorCloseAngle = transform.rotation.eulerAngles.y;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Doorpos = transform.position;
        Vector3 playrpos = Player.transform.position;
        Vector3 Direction = Doorpos - playrpos;

        float dist = Vector3.Distance(playrpos, Doorpos);
        Quaternion Zerorotation = Quaternion.Euler(0, 90, 0);

        if (OpenedRight)
        {
            Quaternion targetRotation = Quaternion.Euler(0, -doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }
        if (OpenedLeft)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }
        if (OpenZeroRotation)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }

        if (DoorClosed)
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, doorOpenSpeed * Time.deltaTime);
        }
        if (DelayTimer)
        {
            doorDelayTime -= Time.deltaTime;

        }
        if (doorDelayTime <= 0 )
        {
            DelayTimer = false;
            doorDelayTime = 0.55f;
        }
        if (isOpen)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Debug.Log("false");
                isOpen = false;
            }
            //if (Input.GetKeyDown(KeyCode.Mouse0))
            //{
            //    doorOpenToggle++;
            //}
            
            
            switch (doorOpenToggle)
            {
                case 1:
                    if (Vector3.Dot(Direction, Player.transform.right) > 0)
                    {

                       
                        if (transform.localRotation == Zerorotation)
                        {
                            Debug.Log("thescuffedRotation");
                            OpenZeroRotation = true;
                            doorOpenToggle++;

                        }
                        if (transform.localRotation != Zerorotation)
                        {
                            Debug.Log("NormalRotation");

                            OpenedRight = true;
                            doorOpenToggle++;
                        }

                    }

                    else
                    {
                        Debug.Log("Behind ");

                        OpenedLeft = true;
                        doorOpenToggle++;

                    }

                    break;
                case 3:
                    OpenedRight = false;
                    OpenedLeft = false;
                    OpenZeroRotation = false;

                    DoorClosed = true;
                 
                    break;
                case 4:
                    DoorClosed = false;
                    OpenZeroRotation = false;
                    doorOpenToggle = 1;
                   
                    break;
            }

        }

    }
   public void ChangeDoorState()
    {
        isOpen = true;

        if (DelayTimer == false)
        {
            doorOpenToggle++;
            DelayTimer = true;
        }
    }
}
