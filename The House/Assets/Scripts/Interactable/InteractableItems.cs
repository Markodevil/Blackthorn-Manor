using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableItems : MonoBehaviour
{

    public GameObject player;
    public GameObject playerCam;
    public float throwForce;
    public bool playerHere;
    public bool beingCarried;
    public bool touched;

    public AudioClip collisionClip;

    Rigidbody rb;
    AudioSource audioSource;

    public float heading;
    public float speed;

    private Vector3 currentPosition;
    private Vector3 previousPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        currentPosition = transform.position;
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;

        if (beingCarried)
        {
            rb.isKinematic = true;
            //transform.parent = playerCam.transform;
            transform.position = Vector3.Lerp(transform.position, playerCam.transform.position + playerCam.transform.forward * heading, Time.deltaTime * speed);
            beingCarried = true;

            Vector3 velocity = currentPosition - previousPosition;

            if (touched)
            {
                Drop();
                touched = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Drop();
                rb.AddForce(playerCam.transform.forward * throwForce);
            }
        }

        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (beingCarried)
        {
            touched = true;

        }

        audioSource.pitch = Random.Range(1, 3);
        audioSource.PlayOneShot(collisionClip);
    }

    public void SetHolding(bool status)
    {
        beingCarried = status;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        transform.parent = null;
        beingCarried = false;
        rb.AddForce(currentPosition - previousPosition);
    }
}
