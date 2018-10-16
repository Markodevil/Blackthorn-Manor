using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horcruxes : MonoBehaviour
{

    public int numberOfHorcruxesNeeded;
    private int currentNumberOfHorcruxes = 0;
    public bool completed = false;

    public Transform[] positions;

    public GameObject Skull;
    public GameObject Knife;
    public GameObject Candle;
    public GameObject Chalk;

    //public GameObject[] items;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumberOfHorcruxes == numberOfHorcruxesNeeded)
        {
            completed = true;
        }
    }

    //--------------------------------------------------------------------------------------
    // Adds to required item count and places items in their new positions
    // 
    // Param
    //        GameObject: the gameobject to be placed in new position
    // Return:
    //        moves items to new positions and adds to required item count
    //--------------------------------------------------------------------------------------
    public void AddHorcruxToRitual(GameObject ritualItem)
    {
        if (positions.Length == 0)
        {
            Debug.Log("You need to give me positions to put the items");
            return;
        }
        //switch (ritualItem.name)
        //{
        //    case "Item1":
        //        //set gameobject position here
        //        ritualItem.transform.position = positions[0].transform.position;
        //        //reenable object
        //        ritualItem.SetActive(true);
        //        //items[0].SetActive(true);
        //
        //        break;
        //    case "Item2":
        //        //set gameobject position here
        //        ritualItem.transform.position = positions[1].transform.position;
        //        //reenable object
        //        ritualItem.SetActive(true);
        //        //items[1].SetActive(true);
        //
        //        break;
        //    case "Item3":
        //        //set gameobject position here
        //        ritualItem.transform.position = positions[2].transform.position;
        //        //reenable object
        //        ritualItem.SetActive(true);
        //        //items[2].SetActive(true);
        //
        //        break;
        //    case "Item4":
        //        //set gameobject position here
        //        ritualItem.transform.position = positions[3].transform.position;
        //        //reenable object
        //        ritualItem.SetActive(true);
        //        //items[3].SetActive(true);
        //
        //        break;
        //    default:
        //        //set gameobject position here
        //        ritualItem.transform.position = positions[0].transform.position;
        //        //reenable object
        //        ritualItem.SetActive(true);
        //
        //        break;
        //}
        if (ritualItem.name == Skull.name + "(Clone)")
        {
            //set gameobject position here
            ritualItem.transform.position = positions[0].transform.position;
            //reenable object
            ritualItem.SetActive(true);
            //items[0].SetActive(true);
        }
        else if (ritualItem.name == Knife.name + "(Clone)")
        {
            //set gameobject position here
            ritualItem.transform.position = positions[1].transform.position;
            //reenable object
            ritualItem.SetActive(true);
            //items[1].SetActive(true);
        }
        else if (ritualItem.name == Candle.name + "(Clone)")
        {
            //set gameobject position here
            ritualItem.transform.position = positions[2].transform.position;
            //reenable object
            ritualItem.SetActive(true);
            //items[2].SetActive(true);
        }
        else if (ritualItem.name == Chalk.name + "(Clone)")
        {
            //set gameobject position here
            ritualItem.transform.position = positions[3].transform.position;
            //reenable object
            ritualItem.SetActive(true);
            //items[3].SetActive(true);
        }

        currentNumberOfHorcruxes++;
    }
}
