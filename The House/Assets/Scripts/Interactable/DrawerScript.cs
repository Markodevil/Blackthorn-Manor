using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    bool isOpen = false;
    public float mouseY;
    public float drawerSpeed; 
    private GameObject Player;
    private GameObject Outline;
    public Rigidbody rb;
    public AudioSource audio;
    public AudioClip drawerSound;
    private Collision Col;
    FPSCamera fpsCamera;
    bool drawerSoundBool;
    bool IsOutlineOff;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Outline = GameObject.FindGameObjectWithTag("Outlined");
        fpsCamera = GetComponent<FPSCamera>();
    }
    // Use this for initialization
    void Start () {

        isOpen = false;
        OnCollisionEnter(Col);

    }

    // Update is called once per frame
    void Update () {

        Vector3 drawerPosition = transform.position;
        // Players position
        Vector3 playerPosition = Player.transform.position;

        // Gets the direction of the player from the door 
        Vector3 Direction = drawerPosition - playerPosition;
        Direction.Normalize();

        float dist = Vector3.Distance(playerPosition, drawerPosition);
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the drawer when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0) || dist > 2)
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

            // Adds force from the players forward position to the door 
            rb.AddForceAtPosition(Player.transform.forward * mouseY * drawerSpeed, Player.transform.position);

        }
        if (IsOutlineOff == false)
        {
            Outline.SetActive(false);

        }

    }

    public void changeDrawerState()
    {
        isOpen = !isOpen;
        playDrawerSound();
    }

    void playDrawerSound()
    {
        drawerSoundBool = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "RequiredItem")
        {
            Debug.Log("Cup not inside");
            IsOutlineOff = false;
        }
 
    }
}
