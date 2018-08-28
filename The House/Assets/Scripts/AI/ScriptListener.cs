using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptListener : MonoBehaviour {

    MenuManager mm;


    private void Awake()
    {
        mm = GameObject.FindObjectOfType<MenuManager>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadGameOver()
    {
        if (mm == null)
            return;
        mm.sceneName = "GameOver";
        mm.fade.ResetTrigger("FadeIn");
        mm.fade.SetTrigger("FadeOut");
    }
}
