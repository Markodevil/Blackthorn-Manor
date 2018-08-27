using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    //All the buttons in the scene
    public Button m_TryAgain, m_Menu, m_Quit;

	// Use this for initialization
	void Start () {
        //Unlocking the cursor for menu nav
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Components
        m_TryAgain = m_TryAgain.GetComponent<Button>();
        m_Menu = m_Menu.GetComponent<Button>();
        m_Quit = m_Quit.GetComponent<Button>();

        //Defining the on click function call
        m_TryAgain.onClick.AddListener(TryAgain);
        m_Menu.onClick.AddListener(Menu);
        m_Quit.onClick.AddListener(Exit);
    }
    //func for the play again button
    private void TryAgain()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        SceneManager.LoadScene(sceneName);
    }
    //func for the Menu button
    private void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    //func for the Quit button
    private void Exit()
    {
        Application.Quit();
    }
}
