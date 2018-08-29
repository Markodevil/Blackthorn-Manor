using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePhysics : MonoBehaviour {
    
    //This class exists to avoid the players rigidbody sending the ritual items flying 
    private GameObject Player; 

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // Ignroe collision between player and ritual items collider
        Physics.IgnoreCollision(Player.GetComponent<CapsuleCollider>(), this.GetComponent<BoxCollider>());
	}
}
