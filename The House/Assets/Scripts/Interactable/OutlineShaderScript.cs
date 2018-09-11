using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used to turn off the outline when a required item has been collected.
public class OutlineShaderScript : MonoBehaviour {

    private GameObject Player;
    private GameObject Dresser;

    public ItemCollection isOutlined;
    public DrawerScript dresserOutline;
  
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Dresser = GameObject.FindGameObjectWithTag("Drawer");

        isOutlined = Player.GetComponent<ItemCollection>();
        dresserOutline = Dresser.GetComponent<DrawerScript>();
    }
    
    // Update is called once per frame
    void Update () {

        // Distance the outlined object is from the player 
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = Player.transform.position;
        float dist = Vector3.Distance(playerPosition, currentPosition);
   
        // when the player is close and toggleoutline is false 
        // Disable the object outline
         if (dist < 4 && isOutlined.toggleOutline == false)
         {
             gameObject.SetActive(false);
             dresserOutline.toggleOutline = false;

         }

    }

}
