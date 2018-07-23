using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using SIGHT;
public class Ghost : MonoBehaviour
{

    //Sets the player/patrol point as the desired destination
    [SerializeField]
    public Transform destination;

    NavMeshAgent navMeshAgent;
    public ConnectedPartol connectedWayPatrol;

    //access to the sight.cs
    Sight enemySight;
    [SerializeField]
    private float chaseTimer = 0f;
    private bool hasBeenSpotted = false;


    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemySight = this.GetComponent<Sight>();

        //Nav mesh failed
        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav Mesh agent component is not attached to " + gameObject.name);
            return;
        }
        //Sight failed
        if (enemySight == null)
        {
            Debug.LogError("The Sight.cs component is not attached to" + gameObject.name);
        }
    }


    void Update()
    {
        //Checks if the Player is within the Ghosts vision
        if (enemySight.visibleTargets.Count > 0)
        {
            //Sets the chase timer to 10s and the player as the navAgent's target
            chaseTimer = 10;
            hasBeenSpotted = true;
            SetDestination();
            connectedWayPatrol.enabled = false;
        }
        else
        {
            //If the player has left the ghosts vision counts down form 10, then go back to patroling
            if (chaseTimer >= 0 && hasBeenSpotted == true)
            {
                //Counts down and updates the players position
                chaseTimer -= Time.deltaTime;
                SetDestination();
            }
            else if(chaseTimer <= 0 && hasBeenSpotted == true)
            {
                //Once the timer has hit zero go back to patroling
                hasBeenSpotted = false;
                connectedWayPatrol.enabled = true;
                connectedWayPatrol.SetDestination();
            }
            //partrol / Search
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Game Over on player collision
        if (other.gameObject.layer == 8)
        {
            destination = null;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

}
