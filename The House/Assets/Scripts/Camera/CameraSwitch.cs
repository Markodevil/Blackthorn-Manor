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
    public MonoBehaviour[] playerScriptsToBeToggled;
    public GameObject playerCamera;

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
        if (!lookingAtPhone)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < playerScriptsToBeToggled.Length; i++)
                {
                    playerScriptsToBeToggled[i].enabled = false;
                }
                phoneThing.SetActive(true);
                lookingAtPhone = true;
            }

        }

        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                cameras[cameraIndex].SetActive(false);
                if (cameraIndex == 0)
                    cameraIndex = cameras.Length - 1;
                else
                    cameraIndex--;
                cameras[cameraIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                cameras[cameraIndex].SetActive(false);
                if (cameraIndex == cameras.Length - 1)
                    cameraIndex = 0;
                else
                    cameraIndex++;
                cameras[cameraIndex].SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                for (int i = 0; i < playerScriptsToBeToggled.Length; i++)
                {
                    playerScriptsToBeToggled[i].enabled = true;
                }
                phoneThing.SetActive(false);
                lookingAtPhone = false;
            }
        }
    }
}
