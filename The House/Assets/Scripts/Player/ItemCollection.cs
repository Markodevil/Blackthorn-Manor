using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{

    public int goalNumberOfItems;
    private int currentNumberOfItems;

    [SerializeField]
    private Camera playerCamera;
    [Tooltip("Interact Range in metres")]
    public float interactRange;


    public List<GameObject> inventory;
    public List<string> pickedUpItems;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check for key input
        if (Input.GetKeyDown(KeyCode.E))
        {
            //raycast from camera forward
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
            {
                //
                GameObject hitObject = hit.collider.gameObject;
                //if the object hit by ray is a required item
                if (hitObject.tag == "RequiredItem")
                {
                    //check to see if the item has been picked up before
                    foreach(string str in pickedUpItems)
                    {
                        //if it has been picked up before
                        if(hitObject.name == str)
                        {
                            //return
                            return;
                        }
                    }
                    //add item to inventory
                    inventory.Add(hitObject);
                    //add item's name to list of picked up items
                    pickedUpItems.Add(hitObject.name);
                    //set object to inactive
                    hitObject.SetActive(false);
                    //add to number of items
                    currentNumberOfItems++;
                }

                //if the object hit is the ritual
                if (hitObject.tag == "HorcruxManager")
                {
                    //make sure something is in the inventory
                    if (inventory.Count > 0)
                        //send the first item in the inventoy to the ritual
                        SendToRitual(inventory[0], hitObject.GetComponent<Horcruxes>());
                }
            }
        }
    }


    //--------------------------------------------------------------------------------------
    // does something to do with required items
    // 
    // Param
    //        GameObject: ritual item to be sent
    //        Horcruxes: the script i'll do stuff to
    // Return:
    //        removes item from inventory and moves it to required item manager
    //--------------------------------------------------------------------------------------
    private void SendToRitual(GameObject ritualItem, Horcruxes script)
    {
        script.AddHorcruxToRitual(ritualItem);
        currentNumberOfItems--;
        inventory.RemoveAt(0);
    }
}
