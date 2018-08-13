using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State<GhostAI>
{
    private static PatrolState instance;

    //Dictates wheter the agent waits on each node
    [SerializeField]
    bool partrolWaiting = false;

    //Total time we wait at each node
    [SerializeField]
    float totalWaitTime = 3f;

    //private variables for base behaviour
    NavMeshAgent navMeshAgent;
    ConnectedWayPoint currentWayPoint;
    ConnectedWayPoint previousWayPoint;

    bool travelling;
    bool waiting;
    float waitTimer;
    int waypointsVisited;

    private PatrolState()
    {
        if (instance != null)
        {
            return;

        }

        instance = this;
        stateName = "Patrol";
    }

    public static PatrolState GetInstance
    {
        get
        {
            if (instance == null)
            {
                new PatrolState();
            }

            return instance;
        }
    }


    public override void EnterState(GhostAI owner)
    {
        navMeshAgent = owner.gameObject.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav mesh agent component is not attached to " + owner.gameObject.name);
        }
        else
        {
            if (currentWayPoint == null)
            {
                //set it at random
                //grab all waypoint objects in scene
                GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");

                if (allWayPoints.Length > 0)
                {
                    while (currentWayPoint == null)
                    {
                        int random = UnityEngine.Random.Range(0, allWayPoints.Length);
                        ConnectedWayPoint startingWayPoint = allWayPoints[random].GetComponent<ConnectedWayPoint>();

                        //i.e WE GOT ONE
                        if (startingWayPoint != null)
                        {
                            currentWayPoint = startingWayPoint;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to find any waypoint for use in the scene");
                }
            }
            SetDestination();
        }
    }

    public override void ExitState(GhostAI owner)
    {
        
    }

    public override void UpdateState(GhostAI owner)
    {
        //Check if we're close to the destination
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;
            waypointsVisited++;

            //If you whant waiting
            if (partrolWaiting)
            {
                waiting = true;
                waitTimer = 0;
            }
            else
            {
                SetDestination();
            }
        }

        //Instead of we're waiting
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                SetDestination();
            }
        }
    }


    public void SetDestination()
    {
        if (waypointsVisited > 0)
        {
            ConnectedWayPoint nextWaypoint = currentWayPoint.NextWayPoint(previousWayPoint);
            previousWayPoint = currentWayPoint;
            currentWayPoint = nextWaypoint;
        }

        Vector3 targetVector = currentWayPoint.transform.position;
        navMeshAgent.SetDestination(targetVector);
        travelling = true;
    }
}
