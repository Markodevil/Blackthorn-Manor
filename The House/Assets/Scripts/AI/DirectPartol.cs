using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This Code was taken from a youtube tutorial by https://www.youtube.com/watch?v=5OkmcKtB3a4&index=4&list=PLokhY9fbx05dodzlBfYsKrUSVk5oVactQ
public class ConnectedPartol : MonoBehaviour
{

    //Dictates wheter the agent waits on each node
    [SerializeField]
    bool partrolWaiting = false;

    //Total time we wait at each node
    [SerializeField]
    float totalWaitTime = 3f;

    //private variables for base behaviour
    NavMeshAgent navMeshAgent;
    public ConnectedWayPoint currentWayPoint;
    ConnectedWayPoint previousWayPoint;
    Ghost ghostCS;
    GameObject ghost;

    bool travelling;
    public bool waiting;
    float waitTimer;
    int waypointsVisited;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        ghost = GameObject.FindGameObjectWithTag("Ghost");

        if (ghost != null)
        {
            ghostCS = ghost.GetComponent<Ghost>();
        }

        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    [HideInInspector]
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

    // Update is called once per frame
    void Update()
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
}
