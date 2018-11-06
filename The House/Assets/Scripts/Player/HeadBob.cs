using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{

    /////////////////////////////////////
    //     SCRIPT IS NOW REDUNDANT     //
    /////////////////////////////////////

    //private float timer = 0.0f;
    //public float bobbingSpeed = 0.18f;
    //public float bobbingAmount = 0.2f;
    //public float midpoint = 0.5f;
    //
    //private void Start()
    //{
    //    
    //}
    //
    //void Update()
    //{
    //    float waveslice = 0.0f;
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //
    //    Vector3 cSharpConversion = transform.localPosition;
    //
    //    if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
    //    {
    //        timer = 0.0f;
    //    }
    //    else
    //    {
    //        waveslice = Mathf.Sin(timer);
    //        timer = timer + bobbingSpeed;
    //        if (timer > Mathf.PI * 2)
    //        {
    //            timer = timer - (Mathf.PI * 2);
    //        }
    //    }
    //    if (waveslice != 0)
    //    {
    //        float translateChange = waveslice * bobbingAmount;
    //        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
    //        totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
    //        translateChange = totalAxes * translateChange;
    //        cSharpConversion.y = midpoint + translateChange;
    //    }
    //    else
    //    {
    //        cSharpConversion.y = midpoint;
    //    }
    //
    //    transform.localPosition = cSharpConversion;
    //}

    public Vector3 restPosition; //local position where your camera would rest when it's not bobbing.
    public float transitionSpeed = 20f; //smooths out the transition from moving to not moving.
    public float bobSpeed = 4.8f;
    public float bobAmount = 0.05f;

    public float bobSpeedRunning = 5.6f;
    public float bobAmountRunning = 0.1f;

    float usedBobSpeed;
    float usedBobAmount;

    public bool sprinting = false;
    public bool isTouchingSomething = false;

    float timer = Mathf.PI / 2;
    public Vector3 camPos;

    void Awake()
    {
        camPos = transform.localPosition;
        usedBobSpeed = bobSpeed;
        usedBobAmount = bobAmount;
    }

    void Update()
    {
        if (isTouchingSomething)
        {
            timer = Mathf.PI / 2; //reinitialize

            Vector3 newPosition = new Vector3(Mathf.Lerp(camPos.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            transform.localPosition = newPosition;
            return;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //moving
        {
            timer += usedBobSpeed * Time.deltaTime;

            switch (sprinting)
            {
                case true:
                    usedBobSpeed = bobSpeedRunning;
                    usedBobAmount = bobAmountRunning;
                    break;
                case false:
                    usedBobSpeed = bobSpeed;
                    usedBobAmount = bobAmount;
                    break;
            }
            //use the timer value to set the position
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * usedBobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * usedBobAmount)), restPosition.z); //abs val of y for a parabolic path
            transform.localPosition = newPosition;
        }
        else
        {
            timer = Mathf.PI / 2; //reinitialize

            Vector3 newPosition = new Vector3(Mathf.Lerp(camPos.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camPos.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            transform.localPosition = newPosition;
        }

        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;
    }
}
