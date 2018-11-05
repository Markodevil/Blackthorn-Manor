using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController charControl;
    float Horizontal = 0;
    float Vertical = 0;
    public float initialSpeed;
    public float sprintMultiplier;
    public float crouchMultiplier;
    private float speed;
    //bool LookingAtCameras = false;
    bool isRunning;

    [Header("Footsteps")]
    public AudioSource footstepsSound;
    public float timeBetweenStepsWalking;
    public float timeBetweenStepsRunning;
    private float footstepTimer;

    private Vector3 desiredRot;
    private Quaternion desiredRotation;
    //private bool rotating = false;

    [Header("Foot/Movement stuff")]
    public GameObject[] footsies;
    private int footIndex = 0;
    public float playerSoundLvl;
    public Transform GhostTransform;
    [HideInInspector]
    public bool playerHasBeenHeard = false;

    [SerializeField]
    private bool isTouchingSomething = false;

    public bool isBreathing = true;
    public bool useHeadbob;
    public Animator headbobAnim;

    [Header("Crouching")]
    public bool isCreepin;
    public float crouchCameraHeight;
    float initialCameraHeight;
    public float crouchSpeed;
    public AudioClip[] playerStressSounds;
    private AudioClip playSound;

    //  Breathing/hear Audiosource for crouching 
    public AudioSource audio;
    // Panic Audiosource for hiding 
    public AudioSource PanicSource;

    public bool Panic = false;
    bool PlayPanicSound = true;

    public Image movementStateImage;
    public Sprite notMoving;
    public Sprite walking;
    public Sprite sprinting;
    public Sprite crouching;
    public enum howAmIMoving
    {
        notMoving,
        creeping,
        walking,
        running
    }
    [HideInInspector]
    public howAmIMoving currentMovementState;

    public enum floorType
    {
        timber,
        carpet,
        tiles
    }

    [HideInInspector]
    public floorType currentFloorType;

    [Header("Sounds for walking")]
    public AudioClip timberStep;
    public AudioClip carpetStep;
    public AudioClip tileStep;

    int floorLayerMask;
    // Use this for initialization
    void Awake()
    {
        charControl = GetComponent<CharacterController>();
    }

    private void Start()
    {
        floorLayerMask = LayerMask.GetMask("Floor");
        isRunning = false;
        isCreepin = false;
        footstepTimer = timeBetweenStepsWalking;
        speed = initialSpeed;
        initialCameraHeight = Camera.main.transform.localPosition.y;
        currentMovementState = howAmIMoving.walking;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 15.0f, floorLayerMask))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Timber":
                    currentFloorType = floorType.timber;
                    break;
                case "Carpet":
                    currentFloorType = floorType.carpet;
                    break;
                case "Tile":
                    currentFloorType = floorType.tiles;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  if (Panic)
        //  {
        //      PlayPanicSound = true;
        //      audio.Play();
        //  }
        // if (Panic)
        //{        
        //    PlayPanicSound = true;
        //}
        //Debug.Log(isRunning);
        //Debug.Log(currentMovementState);

        if (Panic && PlayPanicSound)
        {
            //Debug.Log("PLAYPANIC");

            audio.Stop();
            playSound = playerStressSounds[1];
            PanicSource.clip = playSound;
            PanicSource.Play();
            PlayPanicSound = false;
            isBreathing = false;

        }

        if (Panic == false && PlayPanicSound == false)
        {
            //Debug.Log("AUDIOSTOP");
            PanicSource.Stop();
            PlayPanicSound = true;
            isBreathing = true;

        }
        if (isTouchingSomething)
        {
            headbobAnim.SetBool("isRunning", false);
            headbobAnim.SetBool("isWalking", false);
            //isRunning = false;
            //isCreepin = false;
            //if (currentMovementState == howAmIMoving.running)
            //    playerSoundLvl /= 2;
            //currentMovementState = howAmIMoving.walking;
            //if (useHeadbob)
            //    headbobAnim.SetBool("isRunning", isRunning);
            return;
        }
        else
        {
            //set headbob anim bool
            if (useHeadbob)
            {
                if (currentMovementState == howAmIMoving.running)
                    headbobAnim.SetBool("isRunning", true);
                else
                    headbobAnim.SetBool("isRunning", false);

                if (currentMovementState == howAmIMoving.walking)
                    headbobAnim.SetBool("isWalking", true);
                else
                    headbobAnim.SetBool("isWalking", false);

            }
        }

        //When the player stops crouching stop playing sound effect
        if (!isCreepin)
        {
            audio.Stop();
            isBreathing = true;

            if (Horizontal == 0 && Vertical == 0)
            {
                currentMovementState = howAmIMoving.notMoving;
            }
        }


        //if(!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftControl))
        //{
        //    if(!Input.GetKey(KeyCode.LeftShift))
        //    {
        //        isCreepin = false;
        //        currentMovementState = howAmIMoving.walking;
        //    }
        //}
        switch (currentMovementState)
        {
            case howAmIMoving.notMoving:
                if (movementStateImage)
                    movementStateImage.sprite = notMoving;

                //headbobAnim.SetBool("isRunning", false);

                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, initialCameraHeight, 0.25f), crouchSpeed);
                if (Horizontal != 0 || Vertical != 0)
                {
                    currentMovementState = howAmIMoving.walking;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    isCreepin = true;
                    currentMovementState = howAmIMoving.creeping;
                }
                break;
            case howAmIMoving.creeping:
                if (movementStateImage)
                {
                    if (Vertical != 0 || Horizontal != 0)
                    {

                        movementStateImage.sprite = crouching;

                    }
                    else
                    {
                        movementStateImage.sprite = notMoving;
                    }

                }
                speed = initialSpeed * crouchMultiplier;

                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, crouchCameraHeight, 0.25f), crouchSpeed);
                if (isBreathing == true)
                {
                    playSound = playerStressSounds[0];
                    audio.clip = playSound;
                    audio.Play();
                    isBreathing = false;
                }

                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    isCreepin = false;
                    currentMovementState = howAmIMoving.walking;
                }

                break;
            case howAmIMoving.walking:
                if (movementStateImage)
                    movementStateImage.sprite = walking;
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, initialCameraHeight, 0.25f), crouchSpeed);
                //speed is equal to initial speed
                speed = initialSpeed;

                //if you are already holding left shift
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    //if you start to move forward but not strafe
                    if (Vertical > 0 && Horizontal == 0)
                    {
                        //you are now running
                        isRunning = true;
                        playerSoundLvl *= 2;
                        currentMovementState = howAmIMoving.running;
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    isCreepin = true;
                    currentMovementState = howAmIMoving.creeping;
                }

                break;
            case howAmIMoving.running:
                if (movementStateImage)
                    movementStateImage.sprite = sprinting;
                Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, initialCameraHeight, 0.25f), crouchSpeed);
                //speed is equal to twice the initial speed
                speed = initialSpeed * sprintMultiplier;

                //headbobAnim.SetBool("isRunning", true);

                //if you begin to move backwards
                if (Vertical <= 0)
                {
                    //you are no longer running
                    isRunning = false;
                }

                //if you let go of shift
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    //no longer running
                    isRunning = false;
                    playerSoundLvl /= 2;
                    currentMovementState = howAmIMoving.walking;

                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    isRunning = false;
                    playerSoundLvl /= 2;
                    isCreepin = true;
                    currentMovementState = howAmIMoving.creeping;
                }

                break;
        }

        //move
        //Movement();
    }

    private void FixedUpdate()
    {
        if (isTouchingSomething)
            return;
        Movement();
    }

    void Movement()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Vector3 MoveDirectionSide = transform.right * Horizontal * speed;
        Vector3 MoveDirectionForward = transform.forward * Vertical * speed;

        if (!isCreepin)
        {
            //if you are moving in any direction
            if (Horizontal != 0 || Vertical != 0)
            {
                //footstep sound timer
                footstepTimer -= Time.deltaTime;

                if (footstepTimer <= 0)
                {
                    //play a footstep sound
                    //footstepsSound.Play();
                    switch (currentFloorType)
                    {
                        case floorType.timber:
                            footstepsSound.PlayOneShot(timberStep);
                            break;
                        case floorType.carpet:
                            footstepsSound.PlayOneShot(carpetStep);
                            break;
                        case floorType.tiles:
                            footstepsSound.PlayOneShot(tileStep);
                            break;
                    }

                    //create array of colliders overlapping with a sphere who's origin is our foot 
                    Collider[] hitCollider = Physics.OverlapSphere(footsies[footIndex].transform.position, playerSoundLvl);
                    for (int i = 0; i < hitCollider.Length; i++)
                    {
                        //if current collider's gameobject in array is tagged Ghost
                        if (hitCollider[i].gameObject.tag == "SoundTrigger")
                        {
                            //We've heard the player
                            //Debug.Log("Ghost heard the sound");
                            //Ghost temp = hitCollider[i].gameObject.GetComponent<Ghost>();
                            ////ICANTBELIVEIMISSEDTHISFREAKINMISSTAKE
                            //if (temp.CalulatePathLength(footsies[footIndex].transform.position) <= ghostSoundResponceLvl)
                            //{
                            //    //We're within range to respond to the sound 
                            //    temp.SetDestination();
                            //    playerHasBeenHeard = true;
                            //    Debug.Log("The Ghost is responding to the sound:");
                            //}
                            GhostAI ghostAI = hitCollider[i].gameObject.GetComponentInParent<GhostAI>();
                            if (ghostAI)
                            {
                                //Vector3 direction = GhostTransform.position - footsies[footIndex].transform.position;
                                //direction.Normalize();
                                //if (!Physics.Raycast(footsies[footIndex].transform.position, direction, Vector3.Distance(footsies[footIndex].transform.position, GhostTransform.position), LayerMask.NameToLayer("Obsticle")))
                                //{
                                ghostAI.HearSomething(gameObject.transform.position);

                                //}
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

        }

        //move
        //charControl.SimpleMove(MoveDirectionSide);
        //charControl.SimpleMove(MoveDirectionForward);


        //Vector3 Move = MoveDirectionSide + MoveDirectionForward;
        //Move = Move.normalized;
        //charControl.Move(Move * speed * Time.deltaTime);

        //charControl.SimpleMove(MoveDirectionSide);
        //charControl.SimpleMove(MoveDirectionForward);

        // just in case player leaves the ground       
        if (transform.position.y >= 0.87)
        {
            // returns player to the ground
            transform.Translate(-Vector3.up * Time.deltaTime);
        }

        Vector3 Move = MoveDirectionSide + MoveDirectionForward;
        Move = Move.normalized;
        //this works if you change the gravity on input 
        charControl.Move(Move * speed * Time.deltaTime);


        //DO NOT DO THIS \/ EVER IN LIFE 
        //transform.position += Vector3.ClampMagnitude(Move, speed) * Time.deltaTime;

        

        // Vector3.Normalize(Move);
        // Move = Vector3.ClampMagnitude(Move, 1);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 15.0f, floorLayerMask))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Timber":
                    currentFloorType = floorType.timber;
                    break;
                case "Carpet":
                    currentFloorType = floorType.carpet;
                    break;
                case "Tile":
                    currentFloorType = floorType.tiles;
                    break;
            }
        }
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

    public bool GetTouchingSomething()
    {
        return isTouchingSomething;
    }


    private void OnCollisionStay(Collision collision)
    {
        // When the player collides with a wall
        // the player can no longer run 
        if (collision.gameObject.tag == "Wall")
        {
            isRunning = false;
        }

    }

    public bool GetIsTouchingSomething()
    {
        return isTouchingSomething;
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void EnableMovement()
    {
        FindObjectOfType<GameManager>().EnableMovement();
    }
}
