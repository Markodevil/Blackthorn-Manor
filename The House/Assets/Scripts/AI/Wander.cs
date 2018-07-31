using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//This code was taken and adapted from this webpage https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
public class Wander : MonoBehaviour {

    //Wander Behaviour memeber Var
    private NavMeshAgent navMeshAgent;
    private ConnectedPartol connectedWayPatrol;
    private Wander wander;

    [SerializeField]
    private float wanderRadius = 30f;
    public float wanderTimer = 10f;
    private float wanderTimerActual;
    [SerializeField]
    private float wanderTick = 1;
    private float timer;


    // Use this for initialization
    void OnEnable () {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        connectedWayPatrol = this.GetComponent<ConnectedPartol>();
        wander = this.GetComponent<Wander>();

        connectedWayPatrol.enabled = false;
        timer = 0;
        wanderTimerActual = wanderTimer;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        wanderTimer -= Time.deltaTime;
        if (wanderTimer > 0.0f)
        {
            if (timer >= wanderTick)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                if (wander.CalulatePathLength(newPos) < 5.0f)
                {
                    navMeshAgent.SetDestination(newPos);
                    timer = 0.0f;
                }
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dst, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dst;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dst, layermask);

        return navHit.position;
    }

    //This code was takn from a youtube tutorial from here https://www.youtube.com/watch?v=mBGUY7EUxXQ
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
