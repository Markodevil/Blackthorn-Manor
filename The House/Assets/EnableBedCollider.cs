using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBedCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }
}
