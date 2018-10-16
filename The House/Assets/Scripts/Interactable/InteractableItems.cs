using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class InteractableItems : MonoBehaviour
{

    public GameObject player;
    //public GameObject playerCam;
    //float mouseX;
    //public float throwForce;
    //public float releaseForce;
    //public bool playerHere;
    public bool throwSoundReady;
    //public bool touched;
    //
    //public AudioClip collisionClip;
    //
    //Rigidbody rb;
    AudioSource audioSource;
    ////PlayerMovement playerMovementCS;
    SpringPickup playerSpringPickUp;
    //
    //public float heading;
    //public float speed;
    //
    //private Vector3 currentPosition;
    //private Vector3 previousPosition;
    //
    //private Vector3 localOffset;
    //
    public float soundRange;
    //
    [HideInInspector]
    public bool objectHasBeenHeard = false;

    private void Awake()
    {
        if (player != null)
        {
            //playerMovementCS = player.GetComponent<PlayerMovement>();
            playerSpringPickUp = player.GetComponentInChildren<SpringPickup>();
        }
        //rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    //
    //// Use this for initialization
    //void Start()
    //{
    //    currentPosition = transform.position;
    //    previousPosition = transform.position;
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    #region Old Code
    //    //currentPosition = transform.position;
    //    //
    //    //mouseX = Input.GetAxis("Mouse X");
    //    //// if (Input.GetKeyUp(KeyCode.T))
    //    //// {
    //    ////     rb.AddForce(new Vector3(mouseX * releaseForce, 0, 0));
    //    ////
    //    //// }
    //    //if (beingCarried)
    //    //{
    //    //    rb.isKinematic = true;
    //    //    rb.transform.position = Vector3.Lerp(rb.transform.position, playerCam.transform.position + playerCam.transform.forward * heading, Time.deltaTime * speed);
    //    //    beingCarried = true;
    //    //
    //    //    Vector3 velocity = currentPosition - previousPosition;
    //    //
    //    //
    //    //    if (Input.GetMouseButtonDown(1))
    //    //    {
    //    //        Drop();
    //    //        rb.AddForce(playerCam.transform.forward * throwForce);
    //    //     
    //    //    }
    //    //     
    //    //
    //    //}
    //    //
    //    //
    //    //previousPosition = transform.position;
    //    #endregion 
    //    if (touched)
    //    {
    //        playerSpringPickUp.InputEnd();
    //        touched = false;
    //    }
    //    if (playerSpringPickUp.holdingSomething == true)
    //        throwSoundReady = true;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (playerSpringPickUp.holdingSomething)
        {
            throwSoundReady = true;
        }
        else
        {
            //playerSpringPickUp.InputEnd();
            if (throwSoundReady)
            {
                audioSource.Play();
                CreateSoundColliders();
                throwSoundReady = false;
            }
        }


    }

    //public void SetHolding(bool status)
    //{
    //    throwSoundReady = status;
    //}
    //
    //public void Drop()
    //{
    //    rb.isKinematic = false;
    //    //transform.parent = null;
    //    //throwSoundReady = false;
    //    rb.AddForce(currentPosition - previousPosition);
    //    // Debug.Log("ButtonUp");
    //    //  rb.AddForce(new Vector3(mouseX * releaseForce, 0, 0));
    //    //  rb.AddForce.ri
    //}
    //
    public void CreateSoundColliders()
    {

        Collider[] hitCollider = Physics.OverlapSphere(transform.position, soundRange);
        for (int i = 0; i < hitCollider.Length; i++)
        {
            if (hitCollider[i].gameObject.tag == "SoundTrigger")
            {

                Debug.Log("Ghost heard the sound");
                GhostAI ghostAI = hitCollider[i].gameObject.GetComponentInParent<GhostAI>();
                if (ghostAI)
                {
                    ghostAI.HearSomething(gameObject.transform.position);
                }
            }
        }
    }
    //
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }
}
