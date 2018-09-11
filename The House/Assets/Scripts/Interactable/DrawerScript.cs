using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    bool isOpen = false;
    public float mouseY;
    public float mouseX;
    public float drawerSpeed; 
    private GameObject Player;
    public GameObject Outline;

    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip drawerSound;
    private Collision Col;
    FPSCamera fpsCamera;
    bool drawerSoundBool;
    // Toggles the outline when collided with the back of the dresser
    public bool toggleOutline;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Outline = GameObject.FindGameObjectWithTag("Outlined");
        fpsCamera = GetComponent<FPSCamera>();
    }
    // Use this for initialization
    void Start () {

        isOpen = false;

    }

    // Update is called once per frame
    void Update () {
        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());


        Vector3 drawerPosition = transform.position;
        // Players position
        Vector3 playerPosition = Player.transform.position;

        // Gets the direction of the player from the drawer 
        Vector3 Direction = drawerPosition - playerPosition;
        Direction.Normalize();

        float dist = Vector3.Distance(playerPosition, drawerPosition);
        mouseY = Input.GetAxis("Mouse Y");
        mouseX = Input.GetAxis("Mouse X");

        // lets go of the drawer when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 2.65f)
        {
            isOpen = false;
        }
        // Plays a sound on Drawers Direction
        if (drawerSoundBool && mouseY > 0 || drawerSoundBool && mouseY < 0)
        {
            audio.PlayOneShot(drawerSound, 1);
            drawerSoundBool = false;
        }
      
        // Checks if can be opened and if player is positioned infront of the Dresser 
        if (isOpen && Vector3.Dot(transform.forward, Direction) > 0)
        {
            // Adds force from the players forward position to the drawer 
            rb.AddForceAtPosition(Player.transform.forward * mouseY * drawerSpeed, Player.transform.position);

        }
        if (isOpen && Vector3.Dot(transform.right, Direction) < 0)
        {
            rb.AddForceAtPosition(Player.transform.forward * mouseX * drawerSpeed, Player.transform.position);
        }

        if (isOpen && Vector3.Dot(-transform.right, Direction) < 0)
        {
            rb.AddForceAtPosition(Player.transform.forward * -mouseX * drawerSpeed, Player.transform.position);
        }


    }
    // Sets isOpen to true and enables the drawer sound
    public void changeDrawerState()
    {
        isOpen = !isOpen;
        playDrawerSound();
    }
    // sets bool to true to play the drawer sound
    void playDrawerSound()
    {
        drawerSoundBool = true;
    }
    //--------------------------------------------------------------------------------------
    // Checks if there is a collision between the dresser and a required item
    //
    // Param 
    //      Gameobject: determines if the outlined gamesobject is on or off
    // Return 
    //     if theres a collision between the dresser and required item the dressers outline
    //     will be enabled
    //--------------------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {          
        if (collision.gameObject.tag == "RequiredItem")
         {
            this.Outline.SetActive(true);
            toggleOutline = true;
            Debug.Log("itemisiN");

        }
        else
        {
            this.Outline.SetActive(false);
            Debug.Log("itemisout");
        }
        if (toggleOutline)
        {
            if (collision.gameObject.tag == "Back")
            {
                this.Outline.SetActive(true);
            }
        }
   
    }

}
