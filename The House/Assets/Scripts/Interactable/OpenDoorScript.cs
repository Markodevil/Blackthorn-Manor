using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour {


    public float interactDistance = 5; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            Debug.Log("Raycast 1");
            if (Physics.Raycast(ray,out hit,interactDistance))
            {
                Debug.Log("Raycast 2");

                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.transform.parent.GetComponent<DoorScript>().changeDoorState();
                    Debug.Log("Raycast 3");

                }

            }
        }
	}
}
