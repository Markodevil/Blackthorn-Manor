using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    [Header("Main Menu Items")]
    public GameObject mainMenuItems;
    public string sceneName;

    [Header("Options Menu Items")]
    public GameObject optionsMenuItems;

    [Header("Audio Related")]
    public bool globalMute = true;
    public Slider volumeSlider;

    [Header("Fade in/out")]
    [HideInInspector]
    public Animator fade;

    private AsyncOperation AsyncOp;

    public AudioSource audSource;
    public AudioClip scream;

    [Header("menu options")]
    public bool fullscreen;
    public int textureQuality;
    public int aa;
    public int vSync;
    public string resolution;
    public Dropdown textureQualityDD;
    public Dropdown aaDD;
    public Dropdown vSyncDD;
    public Dropdown resolutionDD;

    private float brightness;
    public Slider brightnessSlider;

    public bool loadedMainScene = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }


    private void Awake()
    {
        fade = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        textureQuality = QualitySettings.GetQualityLevel();
        aa = QualitySettings.antiAliasing;
        vSync = QualitySettings.vSyncCount;
        resolution = Screen.currentResolution.ToString();

        textureQualityDD.value = textureQuality;
        aaDD.value = aa;
        vSyncDD.value = vSync;
        switch (resolution)
        {
            case "1920 x 1080 @ 60Hz":
                resolutionDD.value = 0;
                break;
            case "1280 x 720 @ 60Hz":
                resolutionDD.value = 1;
                break;
        }

        //dont destroy this thing
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //control global volume
        SetGlobalVolume();
        //SetBrightness();
        Debug.Log("Brightness: " + brightness);
        Debug.Log("Actual brightness: " + RenderSettings.ambientIntensity);
        //if (AsyncOp != null)
        //{
        //    if (fade.GetCurrentAnimatorStateInfo(0).IsName("New State"))
        //    {
        //        AsyncOp.allowSceneActivation = true;
        //    }
        //}

        if(SceneManager.GetActiveScene().name == "Menu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1;
        }
    }

    public void SetGlobalVolume()
    {
        //if global mute is not active
        if (!AudioListener.pause)
        {
            if (volumeSlider)
                //update volume
                AudioListener.volume = volumeSlider.value;
        }
    }

    //public void SetBrightness()
    //{
    //    if (brightnessSlider)
    //        brightness = brightnessSlider.value;
    //}

    public void SetGamma()
    {

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
        fade.ResetTrigger("FadeIn");
        fade.SetTrigger("FadeOut");

    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution()
    {
        switch (resolutionDD.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
        }

    }

    public void SetVSync()
    {
        switch (vSyncDD.value)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                QualitySettings.vSyncCount = 1;
                break;
            case 2:
                QualitySettings.vSyncCount = 2;
                break;
        }
    }

    public void SetAntialiasing()
    {
        switch (aaDD.value)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;
            case 1:
                QualitySettings.antiAliasing = 1;
                break;
            case 2:
                QualitySettings.antiAliasing = 2;
                break;
            case 3:
                QualitySettings.antiAliasing = 3;
                break;
        }
    }

    public void SetTextureQuality()
    {
        switch (textureQualityDD.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                break;
            case 3:
                QualitySettings.SetQualityLevel(3);
                break;
            case 4:
                QualitySettings.SetQualityLevel(4);
                break;
            case 5:
                QualitySettings.SetQualityLevel(5);
                break;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnFadeComplete()
    {
        AsyncOp = SceneManager.LoadSceneAsync(sceneName);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        fade.ResetTrigger("FadeOut");
        fade.SetTrigger("FadeIn");
        if(SceneManager.GetActiveScene().name != "Deeon")
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        //RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1);
    }

    public void PlaySoundOnClick()
    {
        audSource.PlayOneShot(scream);
    }
}
