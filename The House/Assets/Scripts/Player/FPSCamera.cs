using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

    public Transform PlayerBody; 
    public float Sensitivity;

    float XAxisClamp = 0; 
    // Use this for initialization
    

    // Update is called once per frame
    void Update () {
        Cursor.lockState = CursorLockMode.Locked;

        RotateCamera();
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
