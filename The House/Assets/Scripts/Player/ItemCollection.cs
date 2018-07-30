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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.tag == "RequiredItem")
                {
                    inventory.Add(hitObject);
                    hitObject.SetActive(false);
                    currentNumberOfItems++;
                }

                if (hitObject.tag == "HorcruxManager")
                {
                    if (inventory.Count > 0)
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
