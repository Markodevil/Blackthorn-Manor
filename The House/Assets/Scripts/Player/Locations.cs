using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Locations : MonoBehaviour
{

    public Text locationsText;

    private void OnTriggerEnter(Collider other)
    {
        //Get name and assign name to location UI
        if (other.tag == "Player")
        {
            locationsText.text = gameObject.name;
        }
    }
}
