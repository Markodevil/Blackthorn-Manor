using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    CharacterController charControl; 
    public float Speed;
    bool LookingAtCameras = false;
    bool isRunning;

    [Header("Footsteps")]
    public AudioSource footstepsManager;
    public float timeBetweenStepsWalking;
    public float timeBetweenStepsRunning;
    private float footstepTimer;

	// Use this for initialization
	void Awake ()
    {
        charControl = GetComponent<CharacterController>();
	}

    private void Start()
    {
        isRunning = false;
        footstepTimer = timeBetweenStepsWalking;
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed *= 2;
            isRunning = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed /= 2;
            isRunning = false;
        }
        Movement();

        
    }

    void Movement()
    {

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertcile = Input.GetAxis("Vertical");

        Vector3 MoveDirectionSide = transform.right * Horizontal * Speed;
        Vector3 MoveDirectionForward = transform.forward * Vertcile * Speed;

        if(Horizontal != 0 || Vertcile != 0)
        {
            footstepTimer -= Time.deltaTime;
            if(footstepTimer <= 0)
            {
                footstepsManager.Play();
                if(isRunning)
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
