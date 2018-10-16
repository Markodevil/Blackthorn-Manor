using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FancyTextWriter : MonoBehaviour
{

    public Text UIText;
    [Multiline]
    public string desiredString;
    [Multiline]
    string currentString;
    public float timeBetweenCharacters;
    float timer;
    int index = 0;
    public GameObject myCollider;
    bool doTheThing = false;
    public Animator anim;
    public GameObject other;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start()
    {
        currentString = desiredString.Substring(0, index);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitty;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitty, 2.5f))
        {
            if (hitty.collider.gameObject == myCollider)
            {
                doTheThing = true;
            }
        }

        if (doTheThing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                index++;
                if (index <= desiredString.Length)
                {
                    currentString = desiredString.Substring(0, index);
                    UIText.text = currentString;
                    timer = timeBetweenCharacters;

                }
            }
        }

        if(index >= desiredString.Length)
        {
            anim.SetTrigger("FadeText");
        }
    }

    public void FadeThingies()
    {
        anim.SetTrigger("FadeStuff");
    }
}
