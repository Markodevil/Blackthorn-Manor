using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{


    public int goalNumberOfItems;
    //[HideInInspector]
    public int currentNumberOfItems;
    public bool toggleOutline;
    [SerializeField]
    private Camera playerCamera;
    [Tooltip("Interact Range in metres")]
    public float interactRange;

    [HideInInspector]
    public List<GameObject> inventory;
    [HideInInspector]
    public List<string> pickedUpItems;

    [Header("UI")]
    public Image Thing1;
    public Image Thing2;
    public Image Thing3;
    public Image Thing4;

    public Sprite filled;
    public Sprite unfilled;

    public AudioClip soundClip;
    public AudioSource audioSource;
    int thingyIndex = 0;

    // Use this for initialization
    void Start()
    {
        if (Thing1 && Thing2 && Thing3 && Thing4)
            UpdateUI();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Sets the object outline back to true
        toggleOutline = true;
        //check for key input
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
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
                    foreach (string str in pickedUpItems)
                    {
                        //if it has been picked up before
                        if (hitObject.name == str)
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
                    // Turns off the object outline 
                    toggleOutline = false;
                    //add to number of items
                    currentNumberOfItems++;
                    // hitObject.GetComponent<AudioSource>().Play();

                    hitObject.SetActive(false);
                    if (audioSource && soundClip)
                        audioSource.PlayOneShot(soundClip);

                    if (Thing1 && Thing2 && Thing3 && Thing4)
                        UpdateUI();
                }

                //if the object hit is the ritual
                if (hitObject.tag == "HorcruxManager")
                {
                    //make sure something is in the inventory
                    if (inventory.Count > 0)
                    {
                        //send the first item in the inventoy to the ritual
                        SendToRitual(inventory[thingyIndex], hitObject.GetComponent<Horcruxes>());
                        thingyIndex++;
                    }
                }
            }
            //if (Thing1 && Thing2 && Thing3 && Thing4)
            //    UpdateUI();
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
        //inventory.RemoveAt(0);
    }

    //--------------------------------------------------------------------------------------
    // Updates in game UI elements
    // 
    // Param
    //        N/A
    // Return:
    //        Updates in game UI elements
    //--------------------------------------------------------------------------------------
    private void UpdateUI()
    {
        //Thing1.color = new Color(Thing1.color.r, Thing1.color.g, Thing1.color.b, 0.25f);
        //Thing2.color = new Color(Thing2.color.r, Thing2.color.g, Thing2.color.b, 0.25f);
        //Thing3.color = new Color(Thing3.color.r, Thing3.color.g, Thing3.color.b, 0.25f);
        //Thing4.color = new Color(Thing4.color.r, Thing4.color.g, Thing4.color.b, 0.25f);
        Thing1.sprite = unfilled;
        Thing2.sprite = unfilled;
        Thing3.sprite = unfilled;
        Thing4.sprite = unfilled;


        foreach (GameObject go in inventory)
        {
            switch (go.name)
            {
                case "Cup3":
                    //Thing1.color = new Color(Thing1.color.r, Thing1.color.g, Thing1.color.b, 0.5f);
                    Thing1.sprite = filled;
                    break;
                case "Plate9":
                    //Thing2.color = new Color(Thing2.color.r, Thing2.color.g, Thing2.color.b, 0.5f);
                    Thing2.sprite = filled;
                    break;
                case "Pot":
                    //Thing3.color = new Color(Thing3.color.r, Thing3.color.g, Thing3.color.b, 0.5f);
                    Thing3.sprite = filled;
                    break;
                case "Mug4":
                    //Thing4.color = new Color(Thing4.color.r, Thing4.color.g, Thing4.color.b, 0.5f);
                    Thing4.sprite = filled;
                    break;
            }

        }
    }
}
