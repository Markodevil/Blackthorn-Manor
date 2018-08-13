using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

public class GhostAI : MonoBehaviour
{

    private FiniteStateMachine<GhostAI> FSM { get; set; }

    /*   Player/Item inputs   */
    private bool hasHeardSomething;

    /*   My Things   */
    private NavMeshAgent NMA;

    /*   Public Variables   */
    public float patrolSpeed;

    /*   Wander Variables   */
    [Header("Wander Variables")]
    [SerializeField]
    public float wanderRadius = 30f;
    public float wanderTimer = 10f;
    [SerializeField]
    public float wanderTick = 1;

    /*   Patrol Variables   */

    // Use this for initialization
    void Start()
    {
        FSM = new FiniteStateMachine<GhostAI>(this);

        //start in wander state
        FSM.ChangeState(WanderState.GetInstance);

        NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHeardSomething)
        {
            FSM.ChangeState(SeekState.GetInstance);
        }
        if(FSM.currentState == PatrolState.GetInstance)
        {
            NMA.speed = patrolSpeed;
        }

        Debug.Log(FSM.currentState.stateName);
        //update current state
        FSM.Update();
    }

    public void HearSomething()
    {
        hasHeardSomething = true;
    }
}
