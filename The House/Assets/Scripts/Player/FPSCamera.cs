using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float interactableDistance;

    public GameObject hand;
    public Sprite touchableSprite;
    public Sprite pickUpSprite;
    public Sprite closedHandSprite;
    public SpringPickup pickupBool;

    private bool touchingDrawer = false;

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //fix to rotation bug - dont know if its the best way to do it
        //transform.localRotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0));
        if (!IsPeeking && !isTouchingSomething)
        {
            RotateCamera();
        }

        //if you aren't touching anything
        if (!isTouchingSomething && !touchingDrawer)
        {
            //so long as you aren't holding an interactable object
            if (!pickupBool.holdingSomething)
            {
                //not touching a drawer
                if (!touchingDrawer)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.forward, out hit, interactableDistance))
                    {
                        GameObject hitObj = hit.collider.gameObject;
						if (hitObj.tag == "Interactable" || hitObj.tag == "Door" || hitObj.tag == "HorcruxManager" || hitObj.tag == "Drawer" || hitObj.tag == "Closet" || hitObj.tag == "KitchenDoor")
                        {
                            UpdateSprite(touchableSprite);
                        }
                        else if (hitObj.tag == "RequiredItem" || hitObj.tag == "TutorialBook")
                        {
                            UpdateSprite(pickUpSprite);
                        }
                        else
                        {
                            if (hand)
                                hand.SetActive(false);
                        }
                    }
                    else
                    {
                        if (hand)
                            hand.SetActive(false);
                    }
                }
                //if you are touching a drawer
                else
                {
                    UpdateSprite(closedHandSprite);
                }
            }
        }
        //you're touching something
        else
        {
            if (hand)
                hand.SetActive(false);
        }

    }
    // moves the camera around with mouse
    void RotateCamera()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        // Sets mouse sensitivity 
        float RotAmountX = MouseX * Sensitivity;
        float RotAmountY = MouseY * Sensitivity;

        // Target rotation of the camera 
        Vector3 TargetRotationCamera = transform.rotation.eulerAngles;
        //Target rotation of the player
        Vector3 TargetRotationBody = PlayerBody.rotation.eulerAngles;


        // Clamps the cameras rotation 
        XAxisClamp -= RotAmountY;
        TargetRotationCamera.x -= RotAmountY;
        TargetRotationCamera.z = transform.rotation.eulerAngles.z;
        TargetRotationBody.y += RotAmountX;

        // Keeps the camera from snapping 
        if (XAxisClamp > 75)
        {
            XAxisClamp = 75;
            TargetRotationCamera.x = 75;
        }
        else if (XAxisClamp < -75)
        {
            XAxisClamp = -75;
            TargetRotationCamera.x = -75;
        }

        // Rotates the camera 
        transform.rotation = Quaternion.Euler(TargetRotationCamera);
        // Rotates the player
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

    public bool GetIsTouchingSomething()
    {
        return isTouchingSomething;
    }

    //--------------------------------------------------------------------------------------
    //  Updates the sprite of hand UI
    //                                                        
    // 
    // Param
    //		spriteToUse: what sprite to use 
    //		 
    // Return:
    //		nothing if hand UI element isn't present
    //      changes sprite to spriteToUse if it is present
    //--------------------------------------------------------------------------------------
    private void UpdateSprite(Sprite spriteToUse)
    {
        if (!hand)
            return;

        hand.SetActive(true);
        hand.GetComponent<Image>().sprite = spriteToUse;
    }

    public void SetTouchingDrawer(bool state)
    {
        touchingDrawer = state;
    }
}
