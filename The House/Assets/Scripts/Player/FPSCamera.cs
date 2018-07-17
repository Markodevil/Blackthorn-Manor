using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {
    public Transform PlayerBody;

    public float Sensitivity;
    public float PeekSpeed;
    float XAxisClamp = 0;
    public GameObject leanPivot;
    bool IsPeeking = false; 
    // Use this for initialization

    // Update is called once per frame
    void Update () {

        Cursor.lockState = CursorLockMode.Locked;
        if (IsPeeking == false)
        {
            RotateCamera();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            IsPeeking = true;
            PeekLeft();
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            NoPeek();
            IsPeeking = false;

        }

        if (Input.GetKey(KeyCode.E))
        {
            IsPeeking = true;

            PeekRight();

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            NoPeek();
            IsPeeking = false;

        }
    }

    void PeekRight()
    {
        leanPivot.transform.rotation = Quaternion.Lerp(leanPivot.transform.rotation, Quaternion.Euler(0, 0, -25), Time.deltaTime * 1.0f);
    }

    void PeekLeft()
    {
        leanPivot.transform.rotation = Quaternion.Lerp(leanPivot.transform.rotation, Quaternion.Euler(0, 0, 25), Time.deltaTime * 1.0f);
    }

    void NoPeek()
    {
        leanPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        TargetRotationCamera.z = 0;

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

}
