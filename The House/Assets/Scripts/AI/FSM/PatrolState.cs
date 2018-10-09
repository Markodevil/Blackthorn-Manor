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
    ConnectedWayPoint currentWayPoint;
    ConnectedWayPoint previousWayPoint;

    bool travelling;
    bool waiting;
    float waitTimer;
    int waypointsVisited;

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
            Debug.Log("OnEnter");
        }
    }

    public override void ExitState(GhostAI owner)
    {

    }

    public override void UpdateState(GhostAI owner)
    {
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
                    Debug.Log("Random Roll = " + randomChance);
                    if (randomChance <= 5)
                    {
                        waiting = true;
                        waitTimer = 0;
                    }
                    else
                    {
                        //Swaping to new track
                        if (owner.ReadyToSwapTrack == true)
                        {
                            currentWayPoint = owner.normalTrackWayPoint;
                            //deeons test thingy
                            owner.currentWayPoint = currentWayPoint;
                            owner.ReadyToSwapTrack = false;
                        }
                        Debug.Log("Random Chance");
                        SetDestination(owner);
                        Debug.Log(navMeshAgent.remainingDistance);
                    }
                }
                else
                {
                    //Swaping to new track
                    if (owner.ReadyToSwapTrack == true)
                    {
                        currentWayPoint = owner.normalTrackWayPoint;
                        //deeons test thingy
                        owner.currentWayPoint = currentWayPoint;
                        owner.ReadyToSwapTrack = false;
                    }
                    //This can never fire because we are unless we arnt waiting
                    Debug.Log("Not PartrolWaiting");
                    SetDestination(owner);
                    Debug.Log(navMeshAgent.remainingDistance);
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
                Debug.Log("WaitTimer >=");
                SetDestination(owner);
                Debug.Log(navMeshAgent.remainingDistance);
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
        Debug.Log(currentWayPoint.name + " " + targetVector);

        travelling = true;
    }
}
