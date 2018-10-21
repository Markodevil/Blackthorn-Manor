using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class TransformArray
{
    public Transform[] transforms;
}

public class GameManager : MonoBehaviour
{
    [Header("secret stuff")]
    public string codeInit;
    public int codeIndex = 0;
    private string code1;
    public AudioSource secretMusic;

    [Header("End Game Stuff")]
    public GameObject clickyWinThing;
    public MonoBehaviour[] scriptsToTurnOff;
    private CameraSwitch CS;
    private GameObject deathCamera;
    private GameObject playerCamera;
    private bool isDead;
    public Animator deathAnim;

    public GameObject playerMesh;
    [Header("Req. Items")]
    public GameObject[] requiredItems;
    public TransformArray[] itemSpawns;
    public Transform ritualItemsParent;

    [Header("UI Tings")]
    private MenuManager menuManager;
    public GameObject ingameUI;
    public GameObject menuUI;
    public GameObject gameoverUI;
    private GameObject camView;
    private CameraPositions camPosScript;
    [Header("Debug stuff")]
    public bool useGhost;
    public GameObject Ghost;
    private GameObject Player;

    public Transform ghostLookAt;

    [Header("Intro stuff")]

    public bool runTutorial = false;
    public string[] prompts;
    private bool hasEnteredState = false;
    public Animator textAnimation;
    public GameObject items;
    private DrawerScript SetOutline;
    private GameObject Dresser;
    public Text tutorialText;
    int tutorialState = 0;
    public bool hasTouchedDresser = false;
    public OpenDoorScript doorScript;
    public GameObject tutorialPageThing;

    float dumbTimer = 2.0f;


    private bool stuffInMyFace = false;
    private float inMyFaceTimer = 5.0f;

    class ResetObjects
    {
        public GameObject item;
        public Transform itemPosition;
        public ResetObjects(GameObject item, Transform itemPosition)
        {
            this.item = item;
            this.itemPosition = itemPosition;
        }
    }

    private List<ResetObjects> resetItems;

    public enum GameStates
    {
        Intro,
        Playing,
        Pause,
        GameOver,
        ChangingScene,
    }


    public GameStates currentState { get; private set; }

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
        Player = GameObject.FindGameObjectWithTag("Player");
        deathCamera = GameObject.FindGameObjectWithTag("DeathCam");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        camView = GameObject.FindGameObjectWithTag("camView");
        camPosScript = camView.GetComponent<CameraPositions>();

        resetItems = new List<ResetObjects>();

        CS = FindObjectOfType<CameraSwitch>();
        doorScript = FindObjectOfType<OpenDoorScript>();
    }

    // Use this for initialization
    void Start()
    {
        if (runTutorial)
        {
            if (menuManager)
            {
                if (!menuManager.hasCompletedTutorial)
                {
                    currentState = GameStates.Intro;
                    doorScript.isLocked = true;
                }
                else
                {
                    currentState = GameStates.Playing;
                    doorScript.isLocked = false;
                }
            }
            else
            {
                currentState = GameStates.Playing;
                doorScript.isLocked = true;
            }
        }
        else
        {
            currentState = GameStates.Playing;
            doorScript.isLocked = true;
        }
        //currentState = GameStates.Intro;
        SpawnItems();

        Dresser = GameObject.FindGameObjectWithTag("TutorialOutlined");
        Dresser.SetActive(false);



        FindObjectOfType<PlayerMovement>().SetTouchingSomething(true);
        FindObjectOfType<FPSCamera>().SetTouching(true);
        deathCamera.SetActive(false);
        //postProcessing.enabled = false;

        GameObject[] items = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in items)
        {
            ResetObjects tempObj = new ResetObjects(go, go.transform);
            resetItems.Add(tempObj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(tutorialState);
        Debug.Log(currentState);
        /////////////////////
        // Game Logic Here //
        /////////////////////
        if (!useGhost)
        {
            Ghost.SetActive(false);
            foreach (MonoBehaviour mb in Ghost.GetComponents<MonoBehaviour>())
            {
                mb.enabled = false;
            }
        }
        else
        {
            Ghost.SetActive(true);
            foreach (MonoBehaviour mb in Ghost.GetComponents<MonoBehaviour>())
            {

                mb.enabled = true;
            }
        }

        switch (currentState)
        {
            //In intro state
            case GameStates.Intro:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menuUI.SetActive(false);
                ingameUI.SetActive(true);
                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = true;
                }
                CS.enabled = true;

                //iterate dumbTimer for safety 
                dumbTimer -= Time.deltaTime;

                //what state in the tutorial we are in
                //switch (tutorialState)
                //{
                //    //open phone
                //    case 0:
                //        tutorialText.text = prompts[0];
                //        if (Player.GetComponent<Animator>().enabled)
                //        {
                //            CS.enabled = false;
                //        }
                //        else
                //        {
                //            CS.enabled = true;
                //        }
                //
                //        if (CS.enabled)
                //        {
                //            if (Input.GetKeyDown(KeyCode.F))
                //            {
                //                textAnimation.SetTrigger("FadeOut");
                //                //textAnimation.SetBool("bFadeOut", true);
                //                tutorialState++;
                //                hasEnteredState = false;
                //                dumbTimer = 2.0f;
                //                break;
                //            }
                //        }
                //        hasEnteredState = true;
                //        break;
                //    //put phone away
                //    case 1:
                //        if (!hasEnteredState)
                //        {
                //            textAnimation.SetTrigger("FadeIn");
                //            //textAnimation.SetBool("bFadeOut", false);
                //            //textAnimation.SetBool("bFadeIn", true);
                //        }
                //        if (dumbTimer <= 0)
                //        {
                //            if (Input.GetKeyDown(KeyCode.F))
                //            {
                //                textAnimation.SetTrigger("FadeOut");
                //                //textAnimation.SetBool("bFadeIn", false);
                //                //textAnimation.SetBool("bFadeOut", true);
                //                tutorialState++;
                //                hasEnteredState = false;
                //                FindObjectOfType<PlayerMovement>().SetTouchingSomething(false);
                //                FindObjectOfType<FPSCamera>().SetTouching(false);
                //                dumbTimer = 2.0f;
                //                Dresser.SetActive(true);
                //                break;
                //            }
                //        }
                //        hasEnteredState = true;
                //        break;
                //    //open drawer
                //    case 2:
                //        RaycastHit rayHit;
                //        if (!hasEnteredState)
                //        {
                //            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, 5.0f))
                //            {
                //                if (rayHit.collider.gameObject.tag == "Dresser")
                //                {
                //                    textAnimation.SetTrigger("FadeIn");
                //                    //textAnimation.SetBool("bFadeOut", false);
                //                    //textAnimation.SetBool("bFadeIn", true);
                //                    hasEnteredState = true;
                //                }
                //            }
                //
                //        }
                //        if (hasTouchedDresser)
                //        {
                //            textAnimation.SetTrigger("FadeOut");
                //            //textAnimation.SetBool("bFadeIn", false);
                //            //textAnimation.SetBool("bFadeOut", true);
                //            tutorialState++;
                //            hasEnteredState = false;
                //            hasTouchedDresser = false;
                //            break;
                //        }
                //        break;
                //    //pick up item
                //    case 3:
                //        if (!hasEnteredState)
                //        {
                //            textAnimation.SetTrigger("FadeIn");
                //            dumbTimer = 5.0f;
                //        }
                //
                //        RaycastHit hit;
                //        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
                //        {
                //            if (hit.collider.gameObject.name == "TutorialBook")
                //            {
                //                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                //                {
                //                    textAnimation.SetTrigger("FadeOut");
                //                    tutorialState++;
                //                    hasEnteredState = false;
                //                    Destroy(hit.collider.gameObject);
                //                    //menuManager.hasCompletedTutorial = true;
                //                    //doorScript.isLocked = false;
                //                    //currentState = GameStates.Playing;
                //                    Dresser.SetActive(false);
                //
                //                    break;
                //                }
                //            }
                //        }
                //        //if (dumbTimer <= 0)
                //        //{
                //        //    textAnimation.SetTrigger("FadeOut");
                //        //    tutorialState++;
                //        //    hasEnteredState = false;
                //        //    menuManager.hasCompletedTutorial = true;
                //        //    currentState = GameStates.Playing;
                //        //    break;
                //        //}
                //        hasEnteredState = true;
                //        break;
                //
                //    case 4:
                //        FindObjectOfType<PlayerMovement>().SetTouchingSomething(true);
                //        FindObjectOfType<FPSCamera>().SetTouching(true);
                //        CS.enabled = false;
                //        tutorialPageThing.SetActive(true);
                //
                //
                //        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                //        {
                //            tutorialState++;
                //            hasEnteredState = false;
                //            menuManager.hasCompletedTutorial = true;
                //            doorScript.isLocked = false;
                //            currentState = GameStates.Playing;
                //
                //            tutorialPageThing.SetActive(false);
                //
                //            FindObjectOfType<PlayerMovement>().SetTouchingSomething(false);
                //            FindObjectOfType<FPSCamera>().SetTouching(false);
                //            CS.enabled = true;
                //            break;
                //        }
                //        break;
                //
                //    default:
                //        foreach (MonoBehaviour mon in scriptsToTurnOff)
                //        {
                //            mon.enabled = true;
                //        }
                //        break;
                //}
                switch (tutorialState)
                {
                    case 0:
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
                        {
                            if (hit.collider.gameObject.name == "TutorialBook")
                            {
                                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                                {
                                    tutorialState++;
                                    Destroy(hit.collider.gameObject);
                                    Dresser.SetActive(false);
                                    break;
                                }
                            }
                        }
                        break;
                    case 1:
                        FindObjectOfType<PlayerMovement>().SetTouchingSomething(true);
                        FindObjectOfType<FPSCamera>().SetTouching(true);
                        CS.enabled = false;
                        tutorialPageThing.SetActive(true);

                        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            tutorialState++;
                            hasEnteredState = false;
                            menuManager.hasCompletedTutorial = true;
                            doorScript.isLocked = false;
                            currentState = GameStates.Playing;

                            tutorialPageThing.SetActive(false);

                            FindObjectOfType<PlayerMovement>().SetTouchingSomething(false);
                            FindObjectOfType<FPSCamera>().SetTouching(false);
                            CS.enabled = true;
                            break;
                        }
                        break;
                    default:
                        break;
                }

                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = GameStates.Pause;
                }
                break;
            case GameStates.Playing:
                //set timescale to 1
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                CheckSecretCode();
                menuUI.SetActive(false);
                ingameUI.SetActive(true);
                items.SetActive(false);
                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = true;
                }

                //if you've brought all items to the spot
                //or you have been killed by the ghost
                //set currentState to game over
                if (clickyWinThing.GetComponent<Horcruxes>().completed/* || KilledByGhost()*/)
                {
                    LoadGameOver();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = GameStates.Pause;
                }

                RaycastHit hitty;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitty, 2.5f))
                {
                    if (hitty.collider.gameObject.name == "TutorialBook")
                    {
                        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            Destroy(hitty.collider.gameObject);
                            tutorialPageThing.SetActive(true);
                            FindObjectOfType<PlayerMovement>().SetTouchingSomething(true);
                            FindObjectOfType<FPSCamera>().SetTouching(true);
                            CS.enabled = false;

                            stuffInMyFace = true;

                            break;
                        }
                    }
                }

                if (stuffInMyFace)
                {
                    inMyFaceTimer -= Time.deltaTime;
                    if (inMyFaceTimer <= 0 || Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        tutorialPageThing.SetActive(false);
                        FindObjectOfType<PlayerMovement>().SetTouchingSomething(false);
                        FindObjectOfType<FPSCamera>().SetTouching(false);
                        CS.enabled = true;
                        doorScript.isLocked = false;
                        stuffInMyFace = false;
                    }
                }

                if (Player.GetComponent<Animator>().enabled)
                {
                    CS.enabled = false;
                }
                else
                    CS.enabled = true;
                break;
            case GameStates.Pause:
                //set timescale to 0
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = false;
                }

                CS.enabled = false;
                menuUI.SetActive(true);
                ingameUI.SetActive(false);
                camPosScript.isNotPaused = false;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    camPosScript.isNotPaused = true;
                    ChangeGameState();
                }

                break;
            case GameStates.GameOver:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                deathCamera.SetActive(true);
                playerCamera.SetActive(false);

                //get direction vector from camera to ghost 
                Vector3 direction = ghostLookAt.transform.position - deathCamera.transform.position;
                //make quaternion using direction vector
                Quaternion rotation = Quaternion.LookRotation(direction);

                //slerp rotation from current to desired
                deathCamera.transform.rotation = Quaternion.Lerp(deathCamera.transform.rotation,
                      Quaternion.LookRotation(direction), 0.01f * Time.time);

                //deactivate mesh 
                playerMesh.SetActive(false);
                isDead = true;
                deathAnim.SetBool("DeathTrigger", isDead);



                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = false;
                }
                CS.phoneThing.SetActive(false);
                CS.lookingAtPhone = false;
                CS.enabled = false;
                break;
            case GameStates.ChangingScene:
                Time.timeScale = 1;
                break;
        }

    }

    //--------------------------------------------------------------------------------------
    // Checks for keyboard inputs to find a secret code
    // 
    // Param
    //        N/A
    // Return:
    //        If the code is put in character for character something cool will happen
    //--------------------------------------------------------------------------------------
    void CheckSecretCode()
    {
        //check for any key input
        if (Input.anyKeyDown)
        {
            //if the key pressed is equal to the key of the codes current index
            if (Input.GetKeyDown(codeInit[codeIndex].ToString()))
            {
                //add to index
                codeIndex++;
            }
            else
            {
                //reset index
                codeIndex = 0;
            }
        }

        //if the index is equal to the code length
        if (codeIndex == codeInit.Length)
        {
            //do secret stuff
            secretMusic.Play();
            //postProcessing.enabled = true;
            codeIndex = 0;
        }
    }

    //--------------------------------------------------------------------------------------
    // Checks if the player has been killed by the ghost
    // 
    // Param
    //        N/A
    // Return:
    //        True if player has been killed by ghost
    //        False if player is still amongst the living
    //--------------------------------------------------------------------------------------
    bool KilledByGhost()
    {
        //put ghost kill check thing in here

        return false;
    }

    //restarts the game
    public void RestartGame()
    {
        if (menuManager)
            menuManager.ToGame();
        else
            SceneManager.LoadScene("Sandbox");
    }


    //--------------------------------------------------------------------------------------
    // Spawns required items at random spawn points in the house
    // 
    // Param
    //        N/A
    // Return:
    //        spawns all required items in random positions in the house
    //        from an array of chosen transforms
    //--------------------------------------------------------------------------------------
    private void SpawnItems()
    {
        /*
        ////initialize temp list of spawn points that have been chosen
        //List<int> chosenSpots = new List<int>();
        ////initialize temp int for random position index
        //int randPlace = int.MaxValue;
        ////for each item required in the game
        //for (int i = 0; i < requiredItems.Length; i++)
        //{
        //    //start do while 
        //    do
        //    {
        //        //set rand place to a random int between 0 and length of item spawns array
        //        randPlace = Random.Range(0, requiredItemSpawns.Length);
        //
        //        //keep doing this if randplace is withing chosenspots
        //    } while (chosenSpots.Contains(randPlace));
        //
        //    //add randplace to chosenspots list
        //    chosenSpots.Add(randPlace);
        //    //instantiate required item at random position
        //    Instantiate(requiredItems[i], requiredItemSpawns[randPlace].transform.position, Quaternion.identity);
        //}

        //for(int i = 0; i < requiredItems.Length; i++)
        //{
        //    List<int> chosenSpots = new List<int>();
        //    //initialize temp int for random position index
        //    int randPlace = int.MaxValue;
        //
        //    switch (requiredItems[i].name)
        //    {
        //        case "Mug":
        //
        //            do
        //            {
        //                //set rand place to a random int between 0 and length of item spawns array
        //                randPlace = Random.Range(0, item1Spawns.Length);
        //
        //                //keep doing this if randplace is withing chosenspots
        //            } while (chosenSpots.Contains(randPlace));
        //
        //            //add randplace to chosenspots list
        //            chosenSpots.Add(randPlace);
        //            //instantiate required item at random position
        //            Instantiate(requiredItems[i], item1Spawns[randPlace].transform.position, Quaternion.identity);
        //
        //            break;
        //        case "Plate":
        //
        //            do
        //            {
        //                //set rand place to a random int between 0 and length of item spawns array
        //                randPlace = Random.Range(0, item2Spawns.Length);
        //
        //                //keep doing this if randplace is withing chosenspots
        //            } while (chosenSpots.Contains(randPlace));
        //
        //            //add randplace to chosenspots list
        //            chosenSpots.Add(randPlace);
        //            //instantiate required item at random position
        //            Instantiate(requiredItems[i], item2Spawns[randPlace].transform.position, Quaternion.identity);
        //
        //            break;
        //        case "Pot":
        //
        //            do
        //            {
        //                //set rand place to a random int between 0 and length of item spawns array
        //                randPlace = Random.Range(0, item3Spawns.Length);
        //
        //                //keep doing this if randplace is withing chosenspots
        //            } while (chosenSpots.Contains(randPlace));
        //
        //            //add randplace to chosenspots list
        //            chosenSpots.Add(randPlace);
        //            //instantiate required item at random position
        //            Instantiate(requiredItems[i], item3Spawns[randPlace].transform.position, Quaternion.identity);
        //
        //            break;
        //        case "Short Cup":
        //
        //            do
        //            {
        //                //set rand place to a random int between 0 and length of item spawns array
        //                randPlace = Random.Range(0, item4Spawns.Length);
        //
        //                //keep doing this if randplace is withing chosenspots
        //            } while (chosenSpots.Contains(randPlace));
        //
        //            //add randplace to chosenspots list
        //            chosenSpots.Add(randPlace);
        //            //instantiate required item at random position
        //            Instantiate(requiredItems[i], item4Spawns[randPlace].transform.position, Quaternion.identity);
        //
        //            break;
        //    }
        //}
        */

        //foreach required item
        for (int i = 0; i < requiredItems.Length; i++)
        {
            //create empty list of ints
            List<int> chosenSpots = new List<int>();
            //initialize temp int for random position index
            int randPlace = int.MaxValue;

            //make sure to do this at least once
            do
            {
                //set rand place to a random int between 0 and length of item spawns array
                randPlace = Random.Range(0, itemSpawns[i].transforms.Length);

                //keep doing this if randplace is withing chosenspots
            } while (chosenSpots.Contains(randPlace));

            //add randplace to chosenspots list
            chosenSpots.Add(randPlace);
            //instantiate required item at random position
            Instantiate(requiredItems[i], itemSpawns[i].transforms[randPlace].transform.position, Quaternion.identity);
        }
    }

    public void ToMenu()
    {
        currentState = GameStates.ChangingScene;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (menuManager)
        {
            menuManager.sceneName = "Menu";
            menuManager.fade.ResetTrigger("FadeIn");
            menuManager.fade.SetTrigger("FadeOut");

        }
    }

    //change gamestate to playing
    public void ChangeGameState()
    {
        if (menuManager)
        {

            if (menuManager.hasCompletedTutorial)
            {
                camPosScript.isNotPaused = true;
                currentState = GameStates.Playing;
            }
            else
            {
                camPosScript.isNotPaused = true;

                currentState = GameStates.Intro;
            }
        }
        else
        {
            camPosScript.isNotPaused = true;
            currentState = GameStates.Playing;
        }
    }

    //change state to whatever state you put as argument
    public void ChangeGameStates(GameStates state)
    {
        currentState = state;
    }

    //change ghost status to whatever it isnt
    public void ChangeGhostStatus()
    {
        useGhost = !useGhost;
    }

    //load gameover scene with fades
    public void LoadGameOver()
    {
        menuManager.sceneName = "WinnerWinnerChickenDinner";
        menuManager.fade.ResetTrigger("FadeIn");
        menuManager.fade.SetTrigger("FadeOut");
    }

    public void UpdateTutorialUI()
    {
        if (tutorialState == prompts.Length)
            items.SetActive(false);
        else
            tutorialText.text = prompts[tutorialState];
    }

    public void EnableMovement()
    {
        switch (currentState)
        {
            case GameStates.Intro:

                break;
            case GameStates.Playing:
                FindObjectOfType<PlayerMovement>().SetTouchingSomething(false);
                FindObjectOfType<FPSCamera>().SetTouching(false);
                break;
        }

    }

    public void ResetEverything()
    {
        //for (int i = 0; i < resetItems.Count; i++)
        //{
        //    resetItems[i].item.transform.position = resetItems[i].itemPosition.position;
        //    resetItems[i].item.transform.rotation = resetItems[i].itemPosition.rotation;
        //}

        if (menuManager)
            SceneManager.LoadScene(menuManager.sceneName);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
