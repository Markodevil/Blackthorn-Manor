using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool isOpen = false;
   public  float doorOpenAngle;
    float doorCloseAngle = 0;
    public float smooth = 2;
    public float mouseY;
    public bool OpeningDoor = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        mouseY = Input.GetAxis("Mouse Y");
       
            if (isOpen && mouseY > 0 )
            {
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, mouseY * Time.deltaTime);
            }
        
        
            if (isOpen && mouseY < 0 )
            {
                Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, -mouseY * Time.deltaTime);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isOpen = false;
            }

    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
    }

}
