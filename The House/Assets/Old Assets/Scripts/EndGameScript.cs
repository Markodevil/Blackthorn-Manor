using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public GameObject canvasObject;

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //canvasObject.SetActive(true);
            Invoke("FadeOut", .2f);
            Invoke("ShowEndGame", 1f);
        }
    }

    void ShowEndGame()
    {
        SceneManager.LoadScene("EndScreen");
        //Cursor.lockState = CursorLockMode.Confined;
        

    }

    void FadeOut()
    {
        GameObject.Find("SceneManager").GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
