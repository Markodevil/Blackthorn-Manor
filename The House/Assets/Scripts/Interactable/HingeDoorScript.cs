using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoorScript : MonoBehaviour {

    public HingeJoint hinge;
    public JointSpring hingeSpring;
    bool isOpen = false;
    public float mouseY;
    public float doorOpenSpeed;
    private GameObject Player;
    public Rigidbody rb;

    // Sets the value for closeTimer
    public float doorCloseTime; 
    // Timer until door closes 
    private float closeTimer;

    bool closeDoor; 
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        closeTimer = doorCloseTime;
        hinge = GetComponent<HingeJoint>();
        hingeSpring = hinge.spring;
        hingeSpring.spring = 0;
        hinge.spring = hingeSpring;
    }

    // Update is called once per frame
    void Update()
    {       
        //// Doors position 
        Vector3 doorPosition = transform.position;
        //// Players position
        Vector3 playerPosition = Player.transform.position;

        float dist = Vector3.Distance(playerPosition, doorPosition);

        //returns mouseY Axis
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the door when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 3.5f)
        {
            isOpen = false;
            closeDoor = true;
        }
        // Counts down for the door to close 
        if (closeDoor)
        {
            closeTimer -= Time.deltaTime;
        }
        // Doors spring returns door back to beggining position 
        if (closeTimer <= 0 )
        {
            hingeSpring.spring = 5;
            hinge.spring = hingeSpring;
            hinge.useSpring = true;
        }
        // Adds force to the players forward direction to the door which will open or close 
        // the door depending on which side the player is located 
        if (isOpen)
        {

            hingeSpring.spring = 0;
            hinge.spring = hingeSpring;
            rb.AddForceAtPosition(Player.transform.forward * mouseY * doorOpenSpeed, Player.transform.position);
            closeTimer = doorCloseTime;

        }

    }

    public void changeDoorState()
    {
        isOpen = !isOpen;
    }

}


