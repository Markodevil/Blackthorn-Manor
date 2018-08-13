using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShaderScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        Material mat = new Material(Shader.Find("Custom/OutlineShader"));


        
        if(Input.GetKeyDown(KeyCode.L))
        {
            mat.SetFloat("_Outline", 0.3f);
        }

	}

}
