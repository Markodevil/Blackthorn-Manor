using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public Transform PlayerBody;

    public float Sensitivity;
    public float PeekSpeed;
    float XAxisClamp = 0;
    public GameObject leanPivot;
    public bool IsPeeking = false;
    public float leanAngle;
    private bool isTouchingSomething = false;

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!IsPeeking && !isTouchingSomething)
        {
            RotateCamera();
        }

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    if (!CheckDirections(false))
        //    {
        //        IsPeeking = true;
        //        PeekLeft();
        //
        //    }
        //
        //}
        //else if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    NoPeek();
        //    IsPeeking = false;
        //
        //}
        //
        //if (Input.GetKey(KeyCode.E))
        //{
        //    if (!CheckDirections(true))
        //    {
        //        IsPeeking = true;
        //        PeekRight();
        //    }
        //}
        //else if (Input.GetKeyUp(KeyCode.E))
        //{
        //
        //    NoPeek();
        //    IsPeeking = false;
        //
        //
        //}
        //
        //if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        //{
        //    NoPeek();
        //}
    }

    void PeekRight()
    {
        leanPivot.transform.rotation = Quaternion.Lerp(leanPivot.transform.rotation, Quaternion.Euler(leanPivot.transform.rotation.eulerAngles.x, leanPivot.transform.rotation.eulerAngles.y, -leanAngle), Time.deltaTime * 1.5f);
    }

    void PeekLeft()
    {
        leanPivot.transform.rotation = Quaternion.Lerp(leanPivot.transform.rotation, Quaternion.Euler(leanPivot.transform.rotation.eulerAngles.x, leanPivot.transform.rotation.eulerAngles.y, leanAngle), Time.deltaTime * 1.5f);
    }

    void NoPeek()
    {
        //leanPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        leanPivot.transform.rotation = Quaternion.Lerp(leanPivot.transform.rotation, Quaternion.Euler(leanPivot.transform.rotation.eulerAngles.x, leanPivot.transform.rotation.eulerAngles.y, 0), Time.deltaTime * 1.5f);
    }
    void RotateCamera()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        float RotAmountX = MouseX * Sensitivity;

        float RotAmountY = MouseY * Sensitivity;

        Vector3 TargetRotationCamera = transform.rotation.eulerAngles;
        Vector3 TargetRotationBody = PlayerBody.rotation.eulerAngles;

        XAxisClamp -= RotAmountY;

        TargetRotationCamera.x -= RotAmountY;

        //TargetRotationCamera.z = 0;  --- changed to \/ to fix leaning problem
        TargetRotationCamera.z = transform.rotation.eulerAngles.z;

        TargetRotationBody.y += RotAmountX;

        if (XAxisClamp > 90)
        {
            XAxisClamp = 90;
            TargetRotationCamera.x = 90;
        }
        else if (XAxisClamp < -90)
        {
            XAxisClamp = -90;
            TargetRotationCamera.x = 270;
        }


        transform.rotation = Quaternion.Euler(TargetRotationCamera);
        PlayerBody.rotation = Quaternion.Euler(TargetRotationBody);
    }

    //--------------------------------------------------------------------------------------
    // Raycasts to the left or right of the player and returns true if there is a wall 
    // right next to the player
    // 
    // Param
    //		Direction: The direction in which I want to check for a wall 
    //		 
    // Return:
    //		A bool which correlates to whether or not there is a wall next to the player
    //--------------------------------------------------------------------------------------

    bool CheckDirections(bool direction)
    {
        RaycastHit hit;
        switch (direction)
        {
            case true:
                if (Physics.Raycast(PlayerBody.transform.position, PlayerBody.transform.right, out hit, 1.5f))
                {
                    if (hit.collider.gameObject.tag == "Wall")
                    {
                        return true;
                    }
                }

                break;
            case false:
                if (Physics.Raycast(PlayerBody.transform.position, -PlayerBody.transform.right, out hit, 1.5f))
                {
                    if (hit.collider.gameObject.tag == "Wall")
                    {
                        return true;
                    }
                }

                break;
        }

        return false;
    }


    public void SetTouching(bool yeah)
    {
        isTouchingSomething = yeah; 
    }
}
