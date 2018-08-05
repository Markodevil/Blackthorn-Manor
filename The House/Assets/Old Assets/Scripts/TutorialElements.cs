 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialElements : MonoBehaviour
{
    public GameObject tutorialObject;

    void Start()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && tutorialObject != null)
        {
            tutorialObject.SetActive(true);
            StartCoroutine("WaitForDelay");
        }
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(tutorialObject);
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(tutorialObject);


        }
    }
}


