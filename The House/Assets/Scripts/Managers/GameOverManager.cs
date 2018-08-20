using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public Button m_TryAgain, m_Menu, m_Quit;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        m_TryAgain = m_TryAgain.GetComponent<Button>();
        m_Menu = m_Menu.GetComponent<Button>();
        m_Quit = m_Quit.GetComponent<Button>();

        m_TryAgain.onClick.AddListener(TryAgain);
        m_Menu.onClick.AddListener(Menu);
        m_Quit.onClick.AddListener(Exit);
    }
	
    private void TryAgain()
    {
        string sceneName = PlayerPrefs.GetString("lastLoadedScene");
        SceneManager.LoadScene(sceneName);
    }
    private void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void Exit()
    {
        Application.Quit();
    }
}
