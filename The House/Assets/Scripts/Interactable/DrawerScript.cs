using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    bool isOpen = false;
    public float mouseY;
    public float drawerSpeed; 
    private GameObject Player;
    public Rigidbody rb;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Use this for initialization
    void Start () {

        isOpen = false;

    }

    // Update is called once per frame
    void Update () {
        mouseY = Input.GetAxis("Mouse Y");

        // lets go of the drawer when Mouse0 is released 
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            isOpen = false;
        }
        // Doors position 
        Vector3 drawerPosition = transform.position;
        // Players position
        Vector3 playerPosition = Player.transform.position;

        // Gets the direction of the player from the door 
        Vector3 Direction = drawerPosition - playerPosition;
        Direction.Normalize();

     

        // Checks if can be opened and if player is positioned infront of the Dresser 
        if (isOpen && Vector3.Dot(transform.forward, Direction) > 0)
        {
            Debug.Log("IsOpen");
            // Adds force from the players forward position to the door 
            rb.AddForceAtPosition(Player.transform.forward * mouseY * drawerSpeed, Player.transform.position);
        }
        
    }

    public void changeDrawerState()
    {
        Debug.Log("ChangeDrawerState");
        isOpen = !isOpen;
    }
}
