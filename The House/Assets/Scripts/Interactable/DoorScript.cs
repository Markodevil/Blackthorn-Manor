using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorScript : MonoBehaviour
{
    public GameObject Player;

    public bool isOpen = false;
    // The angle the door will open to
    public float doorOpenAngle;
    // the angle the door will close to 
    public float doorCloseAngle ;
    public AudioClip[] doorSounds;
    private AudioClip playSound;
    public AudioSource audioSource;
    // How fast the door opens
    public float doorOpenSpeed;
    // How long until the door can be opened/closed to avoid spam
    private float doorDelayTime = 0.55f;

    [Header("SpawnDoor Stuff")]
    [HideInInspector]
    public bool normalTrack = false;
    public bool spawnDoor = false;

    // Opens the door from the direction of the player
    bool openedRight;
    bool openedLeft;
    bool openZeroRotation;
    bool doorClosed;
    
    // Begins delaytimer 
    bool delayTimer;
    // helps door progress through open and closing stages 
    int doorOpenToggle;
    int randomDoorSound;

    // Use this for initialization
    void Start()
    {
        doorCloseAngle = transform.rotation.eulerAngles.y;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //--------------------------------------------------------------------------------------
        // Gets the distance from the player to the door
        //
        // Param 
        //      Vector3.Distance : gets the position of the player and the door 
        // Return 
        //      checks if the player is at a certain distance to open the door 
        //--------------------------------------------------------------------------------------
        Vector3 Doorpos = transform.position;
        Vector3 playrpos = Player.transform.position;
        Vector3 Direction = Doorpos - playrpos;
        float dist = Vector3.Distance(playrpos, Doorpos);
        // is used for doors that are on the z axis 
        Quaternion Zerorotation = Quaternion.Euler(0, 90, 0);

        // if the player is on the right side of the door 
        // bring the door to the open angle
        if (openedRight)
        {
            Quaternion targetRotation = Quaternion.Euler(0, -doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }
        // if the player is on the left side of the door and opens the door
        // bring the door to the open angle
        if (openedLeft)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }
        // if the player opens the door on the zAxis on either side 
        // open the door at 180 degrees 
        if (openZeroRotation)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 180, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorOpenSpeed * Time.deltaTime);
        }
        // if the player closes the door bring door to the closing angle
        if (doorClosed)
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, doorOpenSpeed * Time.deltaTime);
        }
        // when the door has been opened or closed the timer will count down to when the player can 
        // interact with the door again
        if (delayTimer)
        {
            doorDelayTime -= Time.deltaTime;
        }
        if (doorDelayTime <= 0 )
        {
            delayTimer = false;
            doorDelayTime = 0.55f;
        }
        // Begins the door open state 
        if (isOpen)
        {
            // once player interacts with the first door 
            // the ghosts patrol expands to MasterBedroom
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isOpen = false;
                if (spawnDoor == true)
                {
                    normalTrack = true;
                }
            }
            
            // goes though all stages of the door 
            switch (doorOpenToggle)
            {
                // opens the door 
                case 1:
                    // if the door is being opened from the front
                    if (Vector3.Dot(Direction, Player.transform.right) > 0)
                    {

                       // if player opens the door when doors y is 90 
                        if (transform.localRotation == Zerorotation)
                        {
                            openZeroRotation = true;
                            doorOpenToggle++;

                        }
                        if (transform.localRotation != Zerorotation)
                        {

                            openedRight = true;
                            doorOpenToggle++;
                        }
                        // plays door sound
                            playSound = doorSounds[randomDoorSound];
                            audioSource.clip = playSound;
                            audioSource.Play();
                    }
                    // else if the door is being open from behind 
                    else
                    {
                        // opens door from behind (left of the doors coordinates)
                        openedLeft = true;
                        // plays door sound
                        playSound = doorSounds[randomDoorSound];
                        audioSource.clip = playSound;
                        audioSource.Play();
                        // progresses through switch statement
                        doorOpenToggle++;

                    }

                    break;
                // closes the door
                case 3:
                    // Gets random door sound available in the list
                    randomDoorSound = Random.Range(0, doorSounds.Length);
                    playSound = doorSounds[randomDoorSound];
                    audioSource.clip = playSound;
                    // Plays sound 
                    audioSource.Play();
                    // Sets all possible open bools to false to ensure the door closes 
                    openedRight = false;
                    openedLeft = false;
                    openZeroRotation = false;
                    //Rotates door to closing position
                    doorClosed = true;
                 
                    break;
                // resets switchstatement so that player can open door again
                case 4:
                    doorClosed = false;
                    openZeroRotation = false;
                    doorOpenToggle = 1;
                   
                    break;
            }

        }
        
    }
     
    //--------------------------------------------------------------------------------------
    // changes the doors state (is accessed from OpenDoorScript)
    //
    // Param 
    //     isOpen: changes isOpen from false to true
    // Return 
    //      opens and closes the door when the player interacts with it 
    //--------------------------------------------------------------------------------------
    public void ChangeDoorState()
    {
        isOpen = true;
        

        randomDoorSound = Random.Range(0, doorSounds.Length);

        Debug.Log(randomDoorSound);
        if (delayTimer == false)
        {
            doorOpenToggle++;
            delayTimer = true;
        }
    }
}
