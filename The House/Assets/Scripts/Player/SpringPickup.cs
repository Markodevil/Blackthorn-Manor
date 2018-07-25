using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpringPickup : MonoBehaviour
{

    public float throwForce = 600;
    public float damping = 6;
    Transform jointTransform;
    float dragDepth;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputStart();
        }
        if (Input.GetMouseButton(0))
        {
            InputUpdate();
        }
        if (Input.GetMouseButtonUp(0))
        {
            InputEnd();
        }
    }


    //--------------------------------------------------------------------------------------
    // on mouse click checks to see if there is an item infront of the camera
    // and if there is a joint is made from the player to the new item
    // so it can be moved with physics
    // 
    // Param
    //        N/A
    // Return:
    //        new joint is created and used if there is an interactable object 
    //        infront of the camera
    //--------------------------------------------------------------------------------------
    private void InputStart()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2.5f))
        {
            //if the collider is hit and the gameobject is tagged "interactable"
            if (hit.transform.gameObject.tag == "Interactable")
            {
                Debug.Log("touched something");
                dragDepth = 6.0f;
                //create new joint at the point of the hit connected to the player
                jointTransform = AttachJoint(hit.rigidbody, hit.point);
            }
        }
    }


    //--------------------------------------------------------------------------------------
    // updates position of joint transform to so you can pick up and drag items around
    // 
    // Param
    //        N/A
    // Return:
    //        objects are carried around with mouse movement
    //--------------------------------------------------------------------------------------
    public void InputUpdate()
    {
        //if there is no joint, return
        if (jointTransform == null)
            return;
        //else set the joints transform to equal a place just infront of the camera
        jointTransform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
    }


    //--------------------------------------------------------------------------------------
    // Destroys joint gameobject when you let go of the mouse
    // 
    // Param
    //        N/A
    // Return:
    //        destroys gameobject
    //--------------------------------------------------------------------------------------
    public void InputEnd()
    {
        //if jointTransform isn't null destroy it
        if (jointTransform)
            Destroy(jointTransform.gameObject);
    }


    //--------------------------------------------------------------------------------------
    // creates new joint to handle picking up items
    // 
    // Param
    //        rb: rigidbody
    //        attachPos: Vector3
    // Return:
    //        transform of the new joints game object
    //--------------------------------------------------------------------------------------
    Transform AttachJoint(Rigidbody rb, Vector3 attachPos)
    {
        //create new GameObject
        GameObject go = new GameObject("Attachment Point");
        //hide new GameObject in hierarchy
        go.hideFlags = HideFlags.HideInHierarchy;
        //set the position to the attach position
        go.transform.position = attachPos;

        //give the GameObject a RigidBody
        Rigidbody newRb = go.AddComponent<Rigidbody>();
        //set the RigidBody to kinematic
        newRb.isKinematic = true;

        //give the GameObject a ConfigurableJoint
        ConfigurableJoint joint = go.AddComponent<ConfigurableJoint>();
        //make the connected body the players rigidbody
        joint.connectedBody = rb;
        //use world space instead of local
        joint.configuredInWorldSpace = true;

        //define all joint movements
        joint.xDrive = NewJointDrive(throwForce, damping);
        joint.yDrive = NewJointDrive(throwForce, damping);
        joint.zDrive = NewJointDrive(throwForce, damping);

        //define rotation movements
        joint.slerpDrive = NewJointDrive(throwForce, damping);
        //set rotation mode
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        return go.transform;
    }


    //--------------------------------------------------------------------------------------
    // Creates new joint drive to put into configurable joint
    // 
    // Param
    //        force: float
    //        damping: float
    // Return:
    //        new JointDrive with force and damping input into drives variables
    //--------------------------------------------------------------------------------------
    public JointDrive NewJointDrive(float force, float damping)
    {
        //create new JointDrive
        JointDrive drive = new JointDrive();
        //set new drives rubber band pull force
        drive.positionSpring = force;
        //set rubber band resistance
        drive.positionDamper = damping;
        //set max force
        drive.maximumForce = Mathf.Infinity;
        return drive;
    }
}
