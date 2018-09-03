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

    [Header("Req. Items")]
    public GameObject[] requiredItems;
    public TransformArray[] itemSpawns;
    public Transform ritualItemsParent;

    [Header("UI Tings")]
    private MenuManager menuManager;
    public GameObject ingameUI;
    public GameObject menuUI;
    public GameObject gameoverUI;

    [Header("Debug stuff")]
    public bool useGhost;
    public GameObject Ghost;
    private GameObject Player;

    public Transform ghostLookAt;

    [Header("Intro stuff")]
    public string[] prompts;
    private bool hasEnteredState = false;
    public Animator textAnimation;
    public GameObject items;
    public Text tutorialText;
    int tutorialState = 0;
    bool hasFinishedTutorial = false;


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
        CS = FindObjectOfType<CameraSwitch>();
    }

    // Use this for initialization
    void Start()
    {
        currentState = GameStates.Intro;
        SpawnItems();
        //postProcessing.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            case GameStates.Intro:
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                switch (tutorialState)
                {
                    //open phone
                    case 0:
                        tutorialText.text = prompts[0];
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            textAnimation.SetTrigger("FadeOut");
                            tutorialState++;
                            hasEnteredState = false;
                            break;
                        }
                        hasEnteredState = true;
                        break;
                    //put phone away
                    case 1:
                        if (!hasEnteredState)
                        {
                            textAnimation.SetTrigger("FadeIn");
                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            textAnimation.SetTrigger("FadeOut");
                            tutorialState++;
                            hasEnteredState = false;
                            break;
                        }
                        hasEnteredState = true;
                        break;
                    //open drawer
                    case 2:
                        RaycastHit rayHit;
                        if (!hasEnteredState)
                        {
                            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, 5.0f))
                            {
                                if (rayHit.collider.gameObject.tag == "Dresser")
                                {
                        
                                    textAnimation.SetTrigger("FadeIn");
                                    hasEnteredState = true;
                                }
                            }
                        
                        }
                        if (FindObjectOfType<FPSCamera>().GetIsTouchingSomething())
                        {
                            textAnimation.SetTrigger("FadeOut");
                            tutorialState++;
                            hasEnteredState = false;
                            break;
                        }
                        break;
                    //pick up item
                    case 3:
                        if (!hasEnteredState)
                        {
                            textAnimation.SetTrigger("FadeIn");
                        }
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward * 2.5f, out hit))
                            {
                                if (hit.collider.gameObject.tag == "RequiredItem")
                                {
                                    textAnimation.SetTrigger("FadeOut");
                                    tutorialState++;
                                    hasEnteredState = false;
                                    break;
                                }
                            }
                        }
                        hasEnteredState = true;
                        break;
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

                //turn all the scripts back on
                //if coming from game over
                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = true;
                }
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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeGameState();
                }

                break;
            case GameStates.GameOver:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;


                //Vector3 relativePos = (Ghost.transform.position) - Player.transform.position;
                //Quaternion rotation = Quaternion.LookRotation(relativePos);
                //Quaternion targetRot = Quaternion.Euler(Vector3.Slerp(Camera.main.transform.rotation.eulerAngles, rotation.eulerAngles, 0.1f));
                //Camera.main.transform.rotation = targetRot;

                Vector3 direction = ghostLookAt.position - Camera.main.transform.position;
                //Quaternion toRotation = Quaternion.FromToRotation(Camera.main.transform.forward, direction);
                //Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, toRotation, 1.0f * Time.deltaTime);

                //Camera.main.transform.LookAt(ghostLookAt);
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                    Quaternion.LookRotation(direction), 0.01f * Time.time);
                Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation,
                    Quaternion.LookRotation(direction), 0.01f * Time.time);


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

    public void RestartGame()
    {
        if (menuManager)
            menuManager.ToGame();
        else
            SceneManager.LoadScene("Mark");
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
        currentState = GameStates.Playing;
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
        tutorialText.text = prompts[tutorialState];
    }
}
