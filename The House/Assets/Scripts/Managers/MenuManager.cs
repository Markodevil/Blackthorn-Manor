using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Audio Related")]
    float globalVolume;
    public bool globalMute = true;
    public Slider volumeSlider;

    [Header("Main Menu Items")]
    public GameObject mainMenuItems;

    [Header("Options Menu Items")]
    public GameObject optionsMenuItems;


    // Use this for initialization
    void Start()
    {
        //dont destroy this thing
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //control global volume
        SetGlobalVolume();

    }

    public void SetGlobalVolume()
    {
        //if global mute is not active
        if (!AudioListener.pause)
            //update volume
            AudioListener.volume = volumeSlider.value;
    }

    //function for a button to toggle a global mute
    public void ToggleMute()
    {
        // if (globalMute)
        //     globalMute = false;
        // else
        //     globalMute = true;
        if (!AudioListener.pause)
            AudioListener.pause = true;
        else
            AudioListener.pause = false;
    }

    //function for a button to go to an options menu
    public void ToOptions()
    {
        mainMenuItems.SetActive(false);
        optionsMenuItems.SetActive(true);
    }

    //function for a button to return to main menu
    public void ToMenu()
    {
        mainMenuItems.SetActive(true);
        optionsMenuItems.SetActive(false);
    }

    public void ToGame()
    {
        SceneManager.LoadSceneAsync("Mark");
    }
}
