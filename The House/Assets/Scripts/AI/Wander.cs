using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour {

    //Wander Behaviour memeber Var
    private NavMeshAgent agent;
    private ConnectedPartol connectedWayPatrol;

    [SerializeField]
    private float wanderRadius = 30f;
    [HideInInspector]
    public float wanderTimer;
    private float wanderTick;
    private float timer;


    // Use this for initialization
    void OnEnable () {
        agent = this.GetComponent<NavMeshAgent>();
        connectedWayPatrol = this.GetComponent<ConnectedPartol>();

        connectedWayPatrol.enabled = false;
        timer = wanderTick;
        wanderTimer = 10;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        wanderTimer -= Time.deltaTime;
        if (wanderTimer > 0)
        {
            if (timer >= wanderTick)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
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
}
