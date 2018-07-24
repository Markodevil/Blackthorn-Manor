using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorcruxManager : MonoBehaviour
{

    public GameObject playerCamera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 2.0f))
        {
            if (hit.collider.gameObject.tag == "HorcruxManager")
            {
                if (Input.GetMouseButtonDown(0))
                    hit.collider.gameObject.GetComponent<Horcruxes>().AddHorcruxToRitual();
            }
        }
    }

    void AddHorcrux()
    {

    }
}
