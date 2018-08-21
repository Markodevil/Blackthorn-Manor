using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShaderScript : MonoBehaviour {

    private GameObject Player;
    public ItemCollection isOutlined;

  
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isOutlined = Player.GetComponent<ItemCollection>();
    }
    
    // Update is called once per frame
    void Update () {

        // Distance the outlined object is from the player 
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = Player.transform.position;
        float dist = Vector3.Distance(playerPosition, currentPosition);

        // when the player is close and toggleoutline is false 
        // Disable the object outline
         if (dist < 2 && isOutlined.toggleOutline == false)
         {
             gameObject.SetActive(false);
         }

       // if (dist <= 3)
       // {
       //     Debug.Log("Close to dresser");
       //     gameObject.SetActive(false);
       //
       // }
       // 
    }

}
