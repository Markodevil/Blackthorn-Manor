using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject canvasOverlay;

    public Image[] selectedCameraDisplay;

    public AudioSource cameraSoundManager;
    public AudioClip openCameraSound;
    public AudioClip closeCameraSound;
    public AudioClip changeCameraSound;

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
        if (canvasOverlay != null)
            SetCanvasPosition(cameras[0].transform);
        UpdateCanvasIndex();
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
                RaycastHit hit;
                if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1.0f))
                {
                    //set the player scripts to not do things
                    cameraScript.SetTouching(true);
                    playerScript.SetTouchingSomething(true);
                    //set the phone to true
                    phoneThing.SetActive(true);
                    lookingAtPhone = true;

                    if (cameraSoundManager)
                        cameraSoundManager.PlayOneShot(openCameraSound);
                }
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
                if (canvasOverlay != null)
                    SetCanvasPosition(cameras[cameraIndex].transform);
                UpdateCanvasIndex();

                if (cameraSoundManager)
                    cameraSoundManager.PlayOneShot(changeCameraSound);
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
                if (canvasOverlay != null)
                    SetCanvasPosition(cameras[cameraIndex].transform);
                UpdateCanvasIndex();

                if (cameraSoundManager)
                    cameraSoundManager.PlayOneShot(changeCameraSound);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //set player scripts to do things
                cameraScript.SetTouching(false);
                playerScript.SetTouchingSomething(false);
                //playerScript.isCreepin = false;

                //turn the phone off
                phoneThing.SetActive(false);
                lookingAtPhone = false;
                if (cameraSoundManager)
                    cameraSoundManager.PlayOneShot(closeCameraSound);
            }
        }
    }

    private void SetCanvasPosition(Transform position)
    {
        canvasOverlay.transform.SetParent(position.transform);
        canvasOverlay.transform.localPosition = new Vector3(0, 0, 0.205f);
        canvasOverlay.transform.localRotation = Quaternion.identity;
    }

    private void UpdateCanvasIndex()
    {
        SetColour(true, 0, Color.red);
        SetColour(false, cameraIndex, Color.white);
    }

    private void SetColour(bool setAll, int index, Color colour)
    {
        if (setAll)
        {
            for (int i = 0; i < selectedCameraDisplay.Length; i++)
            {
                selectedCameraDisplay[i].color = colour;
            }

        }
        else
        {
            selectedCameraDisplay[index].color = colour;
        }
    }
}
