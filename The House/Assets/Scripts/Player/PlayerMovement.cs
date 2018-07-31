using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController charControl;
    float Horizontal = 0;
    float Verticle = 0;
    public float initialSpeed;
    private float speed;
    bool LookingAtCameras = false;
    bool isRunning;
    public bool hasBeenHeard = false;

    [Header("Footsteps")]
    public AudioSource footstepsSound;
    public float timeBetweenStepsWalking;
    public float timeBetweenStepsRunning;
    private float footstepTimer;

    private Vector3 desiredRot;
    private Quaternion desiredRotation;
    private bool rotating = false;

    public GameObject[] footsies;
    private int footIndex = 0;
    [SerializeField]
    private float playerSoundLvl;
    [SerializeField]
    private float ghostSoundResponceLvl;


    private bool isTouchingSomething = false;



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
        if (isTouchingSomething)
            return; 
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

        //if you are moving forward
        if (Verticle > 0)
        {
            //if you are holding left shift
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //speed is doubled
                speed = initialSpeed * 2;
                isRunning = true;
            }
            else
            {
                //speed is as normal
                speed = initialSpeed;
                isRunning = false;
            }

        }

        //if you are running
        if (isRunning)
        {
            //if you are moving backwards
            if (Verticle < 0)
            {
                //speed is as normal
                speed = initialSpeed;
                isRunning = false;
            }
        }

        //move
        Movement();
    }

    void Movement()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Verticle = Input.GetAxis("Vertical");

        Vector3 MoveDirectionSide = transform.right * Horizontal * speed;
        Vector3 MoveDirectionForward = transform.forward * Verticle * speed;

        //if you are moving in any direction
        if (Horizontal != 0 || Verticle != 0)
        {
            //footstep sound timer
            footstepTimer -= Time.deltaTime;
            
            if (footstepTimer <= 0)
            {
                //play a footstep sound
                footstepsSound.Play();

                //create array of colliders overlapping with a sphere who's origin is our foot 
                Collider[] hitCollider = Physics.OverlapSphere(footsies[footIndex].transform.position, playerSoundLvl);
                for (int i = 0; i < hitCollider.Length; i++)
                {
                    //if current collider's gameobject in array is tagged Ghost
                    if (hitCollider[i].gameObject.tag == "Ghost")
                    {
                        //We've heard the player
                        Debug.Log("ghost can hear my footsies");
                        Ghost temp = hitCollider[i].gameObject.GetComponent<Ghost>();
                        if (temp.CalulatePathLength(footsies[footIndex].transform.position) <= ghostSoundResponceLvl)
                        {
                            //We're within range to respond to the sound 
                            temp.SetDestination();
                            Debug.Log("ghost heard me");
                        }
                    }
                }

                //reset footstep timer
                if (isRunning)
                {
                    footstepTimer = timeBetweenStepsRunning;
                }
                else
                {
                    footstepTimer = timeBetweenStepsWalking;
                }


                //increase foot index 
                if (footIndex + 1 != footsies.Length)
                {
                    footIndex++;

                }
                else
                {
                    footIndex = 0;
                }
            }
        }

        //move
        charControl.SimpleMove(MoveDirectionSide);
        charControl.SimpleMove(MoveDirectionForward);
    }

    public void SetTouchingSomething(bool yeah)
    {
        isTouchingSomething = yeah;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerSoundLvl);
    }
}
