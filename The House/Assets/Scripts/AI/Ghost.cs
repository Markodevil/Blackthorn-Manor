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

    [Header("Ghost Chase")]
    public Transform destination;
    public float patrolSpeed = 3.5f;
    public float chaseTime = 10f;
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
    //Access to the PlayerMovement.cs
    public PlayerMovement player;
    //Acess to the ItemCollection
    private ItemCollection items;

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
        //Check if we've picked up less the 2 items, if yes use regular AI Loop
        if (items.currentNumberOfItems <= 1)
        {
            //If we we'rent spotted and we've been heard head to the sound and wander
            if (player.hasBeenHeard == true && navMeshAgent.remainingDistance <= 3.0f)
            {
                connectedWayPatrol.enabled = false;
                wanderBehavior.enabled = true;
                if (wanderBehavior.wanderTimerActual <= -1.0f)
                {
                    wanderBehavior.enabled = false;
                    player.hasBeenHeard = false;
                    connectedWayPatrol.enabled = true;
                    connectedWayPatrol.SetDestination();
                    navMeshAgent.speed = patrolSpeed;
                }
            }
            //Check if the Player is within the Ghosts vision
            if (enemySight.visibleTargets.Count > 0)
            {
                //Set the chase time and the player as the navAgent's target
                chaseTimer = chaseTime;
                hasBeenSpotted = true;
                SetDestination();
                connectedWayPatrol.enabled = false;
                navMeshAgent.speed = chaseSpeed;
            }
            else
            {
                //If the player has left the ghosts vision counts down form ?, then go back to patroling
                if (chaseTimer >= 0 && hasBeenSpotted == true)
                {
                    //Counts down and chases the player untill time == 0
                    chaseTimer -= Time.deltaTime;
                    SetDestination();
                }
                else if (chaseTimer <= 0 && hasBeenSpotted == true)
                {
                    //Enable wandering for ? before toggling patrol back on, Completing the loop
                    wanderBehavior.enabled = true;
                    if (wanderBehavior.wanderTimerActual <= -1)
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
        else if(items.currentNumberOfItems == 4)
        {
            SetDestination();
            navMeshAgent.speed = chaseSpeed;
        }
        else if(items.currentNumberOfItems >= 2)
        {
            SetDestination();
            navMeshAgent.speed = patrolSpeed;
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

    //sets the publicly assined obj pos and the target i.e the player
    public void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    //This code was taken from a youtube tutorial from here https://www.youtube.com/watch?v=mBGUY7EUxXQ
    //It calculates the total distance to the player taking into account the amount of corners. 
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
