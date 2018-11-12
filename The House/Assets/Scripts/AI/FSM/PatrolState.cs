using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Ryan's mostly
public class PatrolState : State<GhostAI>
{
    private static PatrolState instance;

    //Dictates wheter the agent waits on each node
    bool partrolWaiting = false;

    //Total time we wait at each node
    private float totalWaitTime = 3f;
    private int randomChance;

    //private variables for base behaviour
    NavMeshAgent navMeshAgent;
    GameObject[] normalWayPoints;
    Vector3[] reletivepos;
    ConnectedWayPoint currentWayPoint;
    ConnectedWayPoint previousWayPoint;

    bool travelling;
    bool waiting;
    float waitTimer;
    int waypointsVisited;
    GameObject thingy;
    float lowestMag;

    private float floatTimer = 5.0f;
    private float floatTimeTimer = 5.0f;
    private bool floating = false;

    private PatrolState(GhostAI owner)
    {
        //if (instance != null)
        //{
        //    return;
        //
        //}


        instance = this;
        owner.patrolState = this;
        stateName = "Patrol";
    }

    public static PatrolState GetInstance(GhostAI owner)
    {
        //if (instance == null)
        //{
        if (owner.patrolState == null)
        {
            new PatrolState(owner);
        }
        //}

        return instance;
    }


    public override void EnterState(GhostAI owner)
    {
        navMeshAgent = owner.gameObject.GetComponent<NavMeshAgent>();
        currentWayPoint = owner.currentWayPoint;
        partrolWaiting = owner.wait;
        totalWaitTime = owner.totalWaitTime;
        owner.heardSomethingAnim.SetBool("WaitBool", false);


        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav mesh agent component is not attached to " + owner.gameObject.name);
        }
        else
        {
            SetDestination(owner);
            //Debug.Log("OnEnter");
        }
    }

    public override void ExitState(GhostAI owner)
    {

    }

    public override void UpdateState(GhostAI owner)
    {
        owner.heardSomethingAnim.SetBool("Floating", floating);
        
        if(!waiting && !floating)
        {
            floatTimeTimer -= Time.deltaTime;
            if(floatTimeTimer <= 0)
            {
                floating = true;
                floatTimeTimer = 5.0f;
            }
        }
        else if(floating && !waiting)
        {
            floatTimer -= Time.deltaTime;
            if(floatTimer <= 0)
            {
                floating = false;
                floatTimer = 5.0f;
            }
        }
        owner.wait = waiting;
        //Check if the path is ready
        if (navMeshAgent.pathPending != true)
        {
            //Check if we're close to the destination
            if (/*(*/travelling && navMeshAgent.remainingDistance <= 0.10f)// || (ghostCS.stageThree == true))
            {
                travelling = false;
                waypointsVisited++;

                //If you whant waiting
                if (partrolWaiting && !travelling)
                {
                    randomChance = Random.Range(1, 10);
                    //Debug.Log("Random Roll = " + randomChance);
                    if (randomChance <= 5)
                    {
                        //Swaping to new track
                        if (owner.ReadyToSwapTrack == true)
                        {
                            //Grab a list of waypoints
                            normalWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
                            //Assign a distance to each waypoint from the ghost
                            for (int i = 0; i < normalWayPoints.Length; i++)
                            {
                                //if compare gameobject thing is null
                                if (!thingy)
                                {
                                    //set the stuff
                                    thingy = normalWayPoints[i];
                                    lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                                }
                                //otherwise
                                else
                                {
                                    //compare magnitude of current waypoint to the lowest magnitude
                                    if ((normalWayPoints[i].transform.position - owner.transform.position).magnitude < lowestMag)
                                    {
                                        //set the stuff if current magnitude is less than lowestmag
                                        thingy = normalWayPoints[i];
                                        lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                                    }
                                }
                            }
                            //Assign the lowest distance waypoint to a temp var 
                            currentWayPoint = thingy.GetComponent<ConnectedWayPoint>();
                            //Assign the currentwaypoint to the owner's currentwaypoint wich will be set as the new way point
                            owner.currentWayPoint = currentWayPoint;
                            owner.ReadyToSwapTrack = false;
                        }

                        waiting = true;
                        waitTimer = 0;
                    }
                    else
                    {
                        //Swaping to new track
                        if (owner.ReadyToSwapTrack == true)
                        {
                            //Grab a list of waypoints
                            normalWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
                            //Assign a distance to each waypoint from the ghost
                            for (int i = 0; i < normalWayPoints.Length; i++)
                            {
                                //if compare gameobject thing is null
                                if (!thingy)
                                {
                                    //set the stuff
                                    thingy = normalWayPoints[i];
                                    lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                                }
                                //otherwise
                                else
                                {
                                    //compare magnitude of current waypoint to the lowest magnitude
                                    if ((normalWayPoints[i].transform.position - owner.transform.position).magnitude < lowestMag)
                                    {
                                        //set the stuff if current magnitude is less than lowestmag
                                        thingy = normalWayPoints[i];
                                        lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                                    }
                                }
                            }
                            currentWayPoint = thingy.GetComponent<ConnectedWayPoint>();
                            owner.currentWayPoint = currentWayPoint;
                            owner.ReadyToSwapTrack = false;
                        }

                        //Debug.Log("Random Chance");
                        SetDestination(owner);
                        //Debug.Log(navMeshAgent.remainingDistance);
                    }
                }
                else
                {
                    //Swaping to new track
                    if (owner.ReadyToSwapTrack == true)
                    {
                        //Grab a list of waypoints
                        normalWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
                        //Assign a distance to each waypoint from the ghost
                        for (int i = 0; i < normalWayPoints.Length; i++)
                        {
                            //if compare gameobject thing is null
                            if (!thingy)
                            {
                                //set the stuff
                                thingy = normalWayPoints[i];
                                lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                            }
                            //otherwise
                            else
                            {
                                //compare magnitude of current waypoint to the lowest magnitude
                                if ((normalWayPoints[i].transform.position - owner.transform.position).magnitude < lowestMag)
                                {
                                    //set the stuff if current magnitude is less than lowestmag
                                    thingy = normalWayPoints[i];
                                    lowestMag = (normalWayPoints[i].transform.position - owner.transform.position).magnitude;
                                }
                            }
                        }
                        currentWayPoint = thingy.GetComponent<ConnectedWayPoint>();
                        owner.currentWayPoint = currentWayPoint;
                        owner.ReadyToSwapTrack = false;
                    }

                    //This can never fire if we are waiting
                    //Debug.Log("Not PartrolWaiting");
                    SetDestination(owner);
                    //Debug.Log(navMeshAgent.remainingDistance);
                }
            }
        }


        //Check If were waiting
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            //Debug.Log(waitTimer);
            //Start an idle animation
            owner.heardSomethingAnim.SetBool("WaitBool", true);
            if (waitTimer >= totalWaitTime)
            {
                owner.heardSomethingAnim.SetBool("WaitBool", false);
                waiting = false;
                //Debug.Log("WaitTimer >=");
                SetDestination(owner);
                //Debug.Log(navMeshAgent.remainingDistance);
                travelling = true;
                waitTimer = 0;
            }

        }
    }


    public void SetDestination(GhostAI owner)
    {
        if (waypointsVisited > 0)
        {
            ConnectedWayPoint nextWaypoint = currentWayPoint.NextWayPoint(previousWayPoint);
            previousWayPoint = currentWayPoint;
            currentWayPoint = nextWaypoint;

            //deeons test thingy
            owner.currentWayPoint = currentWayPoint;
        }

        Vector3 targetVector = currentWayPoint.transform.position;
        if (!navMeshAgent.SetDestination(targetVector))
        {
            Debug.Log("Error setting destination");
        }
        //Debug.Log(currentWayPoint.name + " " + targetVector);

        travelling = true;
    }
}
