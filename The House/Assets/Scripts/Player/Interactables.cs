using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    public GameObject playerCam;
    bool isHoldingItem = false;
    GameObject heldItem;
    public SpringPickup sp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHoldingItem)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 2))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.tag == "Interactable")
                    {
                        heldItem = hit.collider.gameObject;
                        heldItem.GetComponent<InteractableItems>().SetHolding(true);
                        isHoldingItem = true;
                    }

                }
            }
        }
        else
        {
            if(Input.GetMouseButtonUp(0))
            {
                heldItem.GetComponent<InteractableItems>().Drop();
                isHoldingItem = false;
            }
        }

        if(!sp.holdingSomething)
        {
            isHoldingItem = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
