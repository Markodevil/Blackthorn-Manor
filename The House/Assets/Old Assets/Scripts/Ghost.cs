using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
	public float deathDistance = 1.1f;
	public float distanceAway;
	public Transform thisObject;
	public Transform target;
    private NavMeshAgent navComponent;
    public GameObject gameoverCanvas;
    public GameObject player;
    public GameObject deathCam;
    public AudioClip scareSound;
    public AudioClip gameoverSound;
	bool playAudio = false;
	public float Volume;

    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navComponent = this.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (target)
        {
            navComponent.SetDestination(target.position);
        }
        else
        {
            if (target = null)
            {
                target = this.gameObject.GetComponent<Transform>();
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

		if (dist <= deathDistance)
        {

            
            player.GetComponent<FirstPersonController>().enabled = false;
            deathCam.SetActive(true);

            Invoke("FadeOut", 4.2f);
            Invoke("ShowGameOver", 5f);


        } 
	}
    void ShowGameOver()
    {
        SceneManager.LoadScene("GameOver");

        //gameoverCanvas.SetActive(true);
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(gameoverSound);
    }

    void FadeOut()
    {
        GameObject.Find("SceneManager").GetComponent<Animator>().SetTrigger("FadeOut");
    }

}

