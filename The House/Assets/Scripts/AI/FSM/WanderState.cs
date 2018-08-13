using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public class WanderState : State<GhostAI>
{

    private static WanderState instance;

    //Wander Behaviour memeber Var's
    private NavMeshAgent navMeshAgent;
    private WanderState wander;

    [SerializeField]
    private float wanderRadius = 30f;
    public float wanderTimer = 10f;
    [HideInInspector]
    public float wanderTimerActual;
    [SerializeField]
    private float wanderTick = 1;
    private float timer;

    private WanderState()
    {
        if (instance != null)
        {
            return;

        }

        instance = this;
        stateName = "Wander";
    }

    public static WanderState GetInstance
    {
        get
        {
            if (instance == null)
            {
                new WanderState();
            }

            return instance;
        }
    }


    public override void EnterState(GhostAI owner)
    {
        //when entering wander state
        wanderRadius = owner.wanderRadius;
        wanderTimer = owner.wanderTimer;
        wanderTick = owner.wanderTick;
        navMeshAgent = owner.gameObject.GetComponent<NavMeshAgent>();
        timer = 0;
        wanderTimerActual = wanderTimer;
    }

    public override void ExitState(GhostAI owner)
    {
        //when exiting wander state
    }

    public override void UpdateState(GhostAI owner)
    {
        //when updating wander state
        timer += Time.deltaTime;
        wanderTimerActual -= Time.deltaTime;
        if (wanderTimerActual > 0.0f)
        {
            if (timer >= wanderTick)
            {
                Vector3 newPos = RandomNavSphere(owner.gameObject.transform.position, wanderRadius, -1);
                if (CalulatePathLength(newPos, owner) < 5.0f)
                {
                    navMeshAgent.SetDestination(newPos);
                    timer = 0.0f;
                }
            }
        }
    }

    //Creates a sphere around the ghost and picks a random location as the destination within the sphere
    public static Vector3 RandomNavSphere(Vector3 origin, float dst, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dst;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dst, layermask);

        return navHit.position;
    }

    //This code was takn from a youtube tutorial from here https://www.youtube.com/watch?v=mBGUY7EUxXQ
    public float CalulatePathLength(Vector3 targetPosition, GhostAI owner)
    {
        NavMeshPath path = new NavMeshPath();

        if (navMeshAgent.enabled)
        {
            navMeshAgent.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = owner.gameObject.transform.position;
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
