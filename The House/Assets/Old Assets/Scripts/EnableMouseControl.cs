using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMouseControl : MonoBehaviour
{

	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }


}
