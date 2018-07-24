using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    CharacterController charControl;
    float Horizontal = 0;
    float Vertcile = 0;
    public float initialSpeed;
    private float speed;
    bool LookingAtCameras = false;
    bool isRunning;

    [Header("Footsteps")]
    public AudioSource footstepsManager;
    public float timeBetweenStepsWalking;
    public float timeBetweenStepsRunning;
    private float footstepTimer;

    private Vector3 desiredRot;
    private Quaternion desiredRotation;
    private bool rotating = false;

    // Use this for initialization
    void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void Start()
    {
        isRunning = false;
        footstepTimer = timeBetweenStepsWalking;
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotating = true;
            //calculate desired rotation
            desiredRotation = new Quaternion();
            desiredRot = transform.rotation.eulerAngles;
            desiredRot += new Vector3(0, 180, 0);
            desiredRotation.eulerAngles = desiredRot;
        }

        if (rotating)
        {
            //                       put desired rotation here \/
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 1.5f * Time.deltaTime);

            //if the difference between these 2 vectors is miniscule 
            if (Vector3.Distance(transform.rotation.eulerAngles, desiredRotation.eulerAngles) < 1.0f)
            {
                //stop rotating
                rotating = false;
                //set current rotation to desired rotation
                transform.rotation = desiredRotation;
            }
        }

        if (Vertcile > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = initialSpeed * 2;
                isRunning = true;
            }
            else
            {
                speed = initialSpeed;
                isRunning = false;
            }

        }

        if(isRunning)
        {
            if(Vertcile < 0)
            {
                speed = initialSpeed;
                isRunning = false;
            }
        }

        Movement();


    }

    void Movement()
    {

        Horizontal = Input.GetAxis("Horizontal");
        Vertcile = Input.GetAxis("Vertical");

        Vector3 MoveDirectionSide = transform.right * Horizontal * speed;
        Vector3 MoveDirectionForward = transform.forward * Vertcile * speed;

        if (Horizontal != 0 || Vertcile != 0)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                footstepsManager.Play();
                if (isRunning)
                {
                    footstepTimer = timeBetweenStepsRunning;
                }
                else
                {
                    footstepTimer = timeBetweenStepsWalking;
                }
            }
        }

        charControl.SimpleMove(MoveDirectionSide);
        charControl.SimpleMove(MoveDirectionForward);
    }
}
