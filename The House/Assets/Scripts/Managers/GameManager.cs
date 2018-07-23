using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string codeInit;
    public int codeIndex = 0;
    private string code1;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked;
		if(Input.anyKeyDown)
        {
            if(Input.GetKeyDown(codeInit[codeIndex].ToString()))
            {
                codeIndex++;
            }
            else
            {
                codeIndex = 0;
            }
        }

        if (codeIndex == codeInit.Length)
        {
            Debug.Log("code put in");
        }
	}
}
