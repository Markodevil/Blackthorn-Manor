using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public RenderTexture renderTexture;
    public int cameraIndex = 0;
    public GameObject[] cameras;
    public GameObject phoneThing;
    public bool lookingAtPhone = false;
    public GameObject playerCamera;

    [SerializeField]
    private FPSCamera cameraScript;
    [SerializeField]
    private PlayerMovement playerScript;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(false);
        }
        cameras[0].SetActive(true);
        playerCamera = GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if you aren't looking at your phone
        if (!lookingAtPhone)
        {
            //if you press the f key
            if (Input.GetKeyDown(KeyCode.F))
            {
                //set the player scripts to not do things
                cameraScript.SetTouching(true);
                playerScript.SetTouchingSomething(true);
                //set the phone to true
                phoneThing.SetActive(true);
                lookingAtPhone = true;
            }

        }

        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //set current cameras active to false
                cameras[cameraIndex].SetActive(false);
                //camera index things
                if (cameraIndex == 0)
                    cameraIndex = cameras.Length - 1;
                else
                    cameraIndex--;
                //set new index to active
                cameras[cameraIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //set current cameras active to false
                cameras[cameraIndex].SetActive(false);
                //camera index things
                if (cameraIndex == cameras.Length - 1)
                    cameraIndex = 0;
                else
                    cameraIndex++;
                //set new index to active
                cameras[cameraIndex].SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                //set player scripts to do things
                cameraScript.SetTouching(false);
                playerScript.SetTouchingSomething(false);

                //turn the phone off
                phoneThing.SetActive(false);
                lookingAtPhone = false;
            }
        }
    }
}
