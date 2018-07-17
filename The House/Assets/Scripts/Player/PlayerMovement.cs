using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    CharacterController charControl; 
    public float Speed;
    bool LookingAtCameras = false; 
	// Use this for initialization
	void Awake ()
    {
        charControl = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        Movement();

        
    }

    void Movement()
    {

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertcile = Input.GetAxis("Vertical");

        Vector3 MoveDirectionSide = transform.right * Horizontal * Speed;
        Vector3 MoveDirectionForward = transform.forward * Vertcile * Speed;

        charControl.SimpleMove(MoveDirectionSide);
        charControl.SimpleMove(MoveDirectionForward);
    }
}
