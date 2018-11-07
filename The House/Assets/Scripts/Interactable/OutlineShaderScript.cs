﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used to turn off the outline when a required item has been collected.
public class OutlineShaderScript : MonoBehaviour
{

    private GameObject Player;
    private GameObject Dresser;

    private ItemCollection isOutlined;
    public bool toggleOutline;
    bool hasTouchedTheThing = false;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isOutlined = Player.GetComponent<ItemCollection>();

        toggleOutline = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Distance the outlined object is from the player 
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = Player.transform.position;
        float dist = Vector3.Distance(playerPosition, currentPosition);


        // when the player is close and toggleoutline is false 
        // Disable the object outline
       // if (dist < 4 && isOutlined.toggleOutline == false)
       // {
       //     toggleOutline = false;
       // }
        // If toggle outline is true the object outline is on
        if (toggleOutline)
        {

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
    //--------------------------------------------------------------------------------------
    // Checks if there is a collision between the dresser and a required item
    //
    // Param 
    //      Gameobject: determines if the outlined gameobject is on or off
    // Return 
    //     if theres a collision between the dresser and required item the dressers outline
    //     will be enabled
    //--------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTouchedTheThing)
        {

            if (other.tag == "RequiredItem")
            {

                toggleOutline = true;
                hasTouchedTheThing = true;

            }
            else
            {

                toggleOutline = false;

            }

        }
    }
}
