using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudioItem : MonoBehaviour {

    AudioSource audSource;


    private void Awake()
    {
        audSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        audSource = GetComponent<AudioSource>();
        Debug.Log(audSource.isPlaying);
        if (!audSource.isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
