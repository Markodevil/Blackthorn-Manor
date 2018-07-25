using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoises : MonoBehaviour {

    float timeAlive = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeAlive -= Time.deltaTime;
        if(timeAlive <= 0)
        {
            Debug.Log("Destroyed stuff");
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("noise collision");
    }
}
