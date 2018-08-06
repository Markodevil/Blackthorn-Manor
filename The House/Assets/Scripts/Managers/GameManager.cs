using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("secret stuff")]
    public string codeInit;
    public int codeIndex = 0;
    private string code1;
    //public PostProcessingBehaviour postProcessing;
    public AudioSource secretMusic;

    [Header("End Game Stuff")]
    public GameObject clickyWinThing;
    public MonoBehaviour[] scriptsToTurnOff;
    public GameObject ghost;

    [Header("Req. Items")]
    public GameObject[] RequiredItems;
    public Transform[] RequiredItemSpawns;

    [Header("UI Tings")]
    public GameObject gameOverText;

    private MenuManager menuManager;


    private enum GameStates
    {
        Playing,
        Pause,
        GameOver,
    }

    private GameStates currentState;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Use this for initialization
    void Start()
    {
        currentState = GameStates.Playing;
        SpawnItems();
        //postProcessing.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////////
        // Game Logic Here//
        ////////////////////
        switch (currentState)
        {
            case GameStates.Playing:
                //set timescale to 1
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                CheckSecretCode();

                //if you've brought all items to the spot
                //or you have been killed by the ghost
                //set currentState to game over
                if (clickyWinThing.GetComponent<Horcruxes>().completed || KilledByGhost())
                {
                    currentState = GameStates.GameOver;
                }


                //turn all the scripts back on
                //if coming from game over
                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = true;
                }
                gameOverText.SetActive(false);
                break;
            case GameStates.Pause:
                //set timescale to 0
                Time.timeScale = 0;

                break;
            case GameStates.GameOver:
                //set timescale to 0
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;


                foreach (MonoBehaviour mon in scriptsToTurnOff)
                {
                    mon.enabled = false;
                }
                gameOverText.SetActive(true);
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
        //initialize temp list of spawn points that have been chosen
        List<int> chosenSpots = new List<int>();
        //initialize temp int for random position index
        int randPlace = int.MaxValue;
        //for each item required in the game
        for (int i = 0; i < RequiredItems.Length; i++)
        {
            //start do while 
            do
            {
                //set rand place to a random int between 0 and length of item spawns array
                randPlace = Random.Range(0, RequiredItemSpawns.Length);

                //keep doing this if randplace is withing chosenspots
            } while (chosenSpots.Contains(randPlace));

            //add randplace to chosenspots list
            chosenSpots.Add(randPlace);
            //instantiate required item at random position
            Instantiate(RequiredItems[i], RequiredItemSpawns[randPlace].transform.position, Quaternion.identity);
        }
    }
}
