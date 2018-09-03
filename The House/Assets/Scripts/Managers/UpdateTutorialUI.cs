using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTutorialUI : MonoBehaviour {

    private GameManager go;

    private void Awake()
    {
        go = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTutorialText()
    {
        go.UpdateTutorialUI();
    }
}
