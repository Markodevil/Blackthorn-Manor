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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(playerCam.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 10.0f);
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 10.0f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Debug.Log("Player looking at me");
                playerHere = true;

            }
            else
            {
                Debug.Log("Player not looking at me");
                playerHere = false;
            }
        }

        if (playerHere && Input.GetMouseButtonDown(0))
        {
            rb.isKinematic = true;
            transform.parent = playerCam.transform;
            beingCarried = true;
        }

        if (beingCarried)
        {
            if (touched)
            {
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                rb.AddForce(playerCam.transform.forward * throwForce);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (beingCarried)
        {
            if (collision.gameObject.tag == "Wall")
            {
                touched = true;
            }

        }

        audioSource.pitch = Random.Range(1, 3);
        audioSource.PlayOneShot(collisionClip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if(!beingCarried)
        //{
        //    am.CreateAudioInstance(collisionClip, collision.contacts[0].point);
        //}
    }
}
