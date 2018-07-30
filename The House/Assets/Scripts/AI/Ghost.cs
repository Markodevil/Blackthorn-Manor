using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using SIGHT;
public class Ghost : MonoBehaviour
{
    //Sets the player/patrol point as the desired destination
    private bool hasBeenSpotted = false;
    private float chaseTimer = 0f;

    public float chaseTime = 10f;
    public Transform destination;
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 7f;


    //Access to the navMeshAgent and ConnectedPatrol 
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public ConnectedPartol connectedWayPatrol;

    //Access to the Sight.cs
    Sight enemySight;
    //Access to the Wander.cs
    [HideInInspector]
    public Wander wanderBehavior;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemySight = this.GetComponent<Sight>();
        wanderBehavior = this.GetComponent<Wander>();
        wanderBehavior.enabled = false;

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
        //Wander failed
        if (wanderBehavior == null)
        {
            Debug.LogError("The Wander.cs component is not attached to" + gameObject.name);
        }
    }

    void Update()
    {
        //Checks if the Player is within the Ghosts vision
        if (enemySight.visibleTargets.Count > 0)
        {
            //Sets the chase timer to 10s and the player as the navAgent's target
            chaseTimer = chaseTime;
            hasBeenSpotted = true;
            SetDestination();
            connectedWayPatrol.enabled = false;
            navMeshAgent.speed = chaseSpeed;
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
            else if (chaseTimer <= 0 && hasBeenSpotted == true)
            {
                //Enable wandering for 10s before toggling patrol back on, Completing the loop
                wanderBehavior.enabled = true;
                if (wanderBehavior.wanderTimer <= -1)
                {
                    //Once the timer has hit zero go back to patroling
                    hasBeenSpotted = false;
                    wanderBehavior.enabled = false;
                    connectedWayPatrol.enabled = true;
                    connectedWayPatrol.SetDestination();
                    navMeshAgent.speed = patrolSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Game Over on player collision
        if (other.gameObject.layer == 8)
        {
            destination = null;
            SceneManager.LoadScene("GameOver");
            Debug.Log("You Got touched by a ghost, How do you feel?");
        }
    }

    public void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    public float CalulatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (navMeshAgent.enabled)
        { 
            bool temp = navMeshAgent.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0.0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

}
