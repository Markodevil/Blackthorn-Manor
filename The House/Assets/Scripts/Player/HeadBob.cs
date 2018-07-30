using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

    public Animator headBobAnim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            headBobAnim.SetBool("isRunning", false);
            return;
        }
        
        if (Input.GetAxis("Vertical") != 0)
        {
            headBobAnim.SetBool("isRunning", true);
        }
        else
        {
            headBobAnim.SetBool("isRunning", false);
        }
    }
}
