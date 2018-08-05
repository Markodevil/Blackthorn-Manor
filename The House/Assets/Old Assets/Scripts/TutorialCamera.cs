using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCamera : MonoBehaviour
{
    public GameObject tutorialObjectF;
    public GameObject tutorialObjectAD;

    void Start()
    {
        tutorialObjectF.SetActive(true);
        StartCoroutine("WaitForDelay");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (tutorialObjectAD != null)
            {
                Destroy(tutorialObjectF);
                tutorialObjectAD.SetActive(true);
                StartCoroutine("DestroyDelay");
            }
        }
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(tutorialObjectF);
        //Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
       
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(tutorialObjectAD);
    }
}