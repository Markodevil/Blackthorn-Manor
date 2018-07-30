using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SetGlobalVolume();

    }

    public void SetGlobalVolume()
    {
        if (!globalMute)
            globalVolume = volumeSlider.value;
    }

    public void ToggleMute()
    {
        if (globalMute)
            globalMute = false;
        else
            globalMute = true;
    }

    public void ToOptions()
    {
        mainMenuItems.SetActive(false);
        optionsMenuItems.SetActive(true);
    }

    public void ToMenu()
    {
        mainMenuItems.SetActive(true);
        optionsMenuItems.SetActive(false);
    }
}
