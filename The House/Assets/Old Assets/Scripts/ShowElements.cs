 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowElements : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject itemToDestroy;
    public GameObject triggerObject;
    public RitualScript ritualObject;
    public AudioSource collectSource;
    public AudioClip collectSound;

 


    void Start()
    {
       
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvasObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            collectSource.PlayOneShot(collectSound);
            itemToDestroy.SetActive(false);
            ritualObject.NumberOfObjects += 1;
            Debug.Log("Item Collected");
            canvasObject.SetActive(false);
            Destroy(triggerObject);


        }
    }

    void OnTriggerExit(Collider other)
    {
        canvasObject.SetActive(false);
    }

}

