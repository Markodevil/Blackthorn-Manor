using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositions : MonoBehaviour {


    public GameObject[] Cameras;
    private CameraSwitch camSwitch;
    GameObject player;
    private int Change;
    public bool isNotPaused;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        camSwitch = player.GetComponent<CameraSwitch>();
        isNotPaused = true;
        Cameras[0].SetActive(true);
        Change = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {  
        // If player is looking at phone
        if (camSwitch.lookingAtPhone == true && isNotPaused)
        {
            // Rotates to the right
            if (Input.GetKeyDown(KeyCode.D))
            {
                // If the array is at zero 
                if(Change == Cameras.Length - 1)
                {
                    // Turn off past camera 
                    Cameras[Change].SetActive(false);
                    // Reset array
                    Change = 0;
                    // Turns on current camera
                    Cameras[Change].SetActive(true);
                }             
                else
                {
                    // Turns off past camera
                    Cameras[Change].SetActive(false);
                    Change++;
                    // Turns on current camera
                    Cameras[Change].SetActive(true);
                }
            }           
            // Rotates to the left
            if (Input.GetKeyDown(KeyCode.A))
            { 
                // If at begging of array
                if (Change == 0)
                {
                    // Turn off past camera 
                    Cameras[Change].SetActive(false);
                    // Set to the end of array
                    Change += Cameras.Length - 1;
                    // Turns on current camera
                    Cameras[Change].SetActive(true);
                }
                else
                {                  
                    // Turn off past camera 
                    Cameras[Change].SetActive(false);
                    Change--;
                    // Turn on past camera 
                    Cameras[Change].SetActive(true);
                }
         
            }

        }   
	}
}
