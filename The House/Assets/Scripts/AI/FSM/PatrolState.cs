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
        //if (instance != null)
        //{
        //    return;
        //
        //}

        instance = this;
        stateName = "Patrol";
    }

    public static PatrolState GetInstance
    {
        get
        {
            //if (instance == null)
            //{
                new PatrolState();
            //}

            return instance;
        }
    }


    public override void EnterState(GhostAI owner)
    {
        navMeshAgent = owner.gameObject.GetComponent<NavMeshAgent>();
        currentWayPoint = owner.currentWayPoint;

        
        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav mesh agent component is not attached to " + owner.gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    public override void ExitState(GhostAI owner)
    {
        
    }

    public override void UpdateState(GhostAI owner)
    {
        //Debug.Log(navMeshAgent.remainingDistance);
        //Check if we're close to the destination
        if (/*(*/travelling && navMeshAgent.remainingDistance <= 1.0f)// || (ghostCS.stageThree == true))
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

        //Check If were waiting
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
