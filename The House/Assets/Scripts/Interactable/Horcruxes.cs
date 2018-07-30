using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horcruxes : MonoBehaviour
{

    public int numberOfHorcruxesNeeded;
    private int currentNumberOfHorcruxes = 0;
    public bool completed = false;

    public Transform[] positions;

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
        switch (ritualItem.name)
        {
            case "frozen pizza":
                //set gameobject position here
                ritualItem.transform.position = positions[0].transform.position;
                //reenable object
                ritualItem.SetActive(true);

                break;
            case "pepsi":
                //set gameobject position here

                //reenable object

                break;
            case "garlic bread":
                //set gameobject position here

                //reenable object

                break;
            case "pizza box":
                //set gameobject position here

                //reenable object

                break;
            default:
                //set gameobject position here
                ritualItem.transform.position = positions[0].transform.position;
                //reenable object
                ritualItem.SetActive(true);

                break;
        }

        Debug.Log("added to ritual");
        currentNumberOfHorcruxes++;
    }
}
