using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

    public GameObject player;
    public GameObject playerCam;
    public float throwForce;
    public bool playerHere;
    public bool beingCarried;
    public bool touched;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(playerCam.transform.forward);
        RaycastHit hit;
		if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10.0f))
        {
            Debug.Log("Player looking at me");
            playerHere = true;
        }
        else
        {
            Debug.Log("Player not looking at me");
            playerHere = false;
        }

        if(playerHere && Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam.transform;
            beingCarried = true;
        }

        if(beingCarried)
        {
            if(touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }

            if(Input.GetMouseButtonUp(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            touched = true;
        }
    }
}
