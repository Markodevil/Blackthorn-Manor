using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horcruxes : MonoBehaviour {

    public int numberOfHorcruxesNeeded;
    private int currentNumberOfHorcruxes = 0;

    public bool completed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(currentNumberOfHorcruxes == numberOfHorcruxesNeeded)
        {
            completed = true;
        }
	}

    public void AddHorcruxToRitual()
    {
        currentNumberOfHorcruxes++;
    }
}
