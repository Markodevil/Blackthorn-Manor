﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using FSM;
using SIGHT;

public class GhostAI : MonoBehaviour
{

    public FiniteStateMachine<GhostAI> FSM { get; set; }

    /*   Player/Item/Sight inputs   */
    [HideInInspector]
    public bool hasHeardSomething { get; set; }
    private Sight sight;

    /*   My Things   */
    [HideInInspector]
    public NavMeshAgent NMA;

    /*   Public Variables   */
    public float patrolSpeed;
    public Animator heardSomethingAnim;

    /*   Wander Variables   */
    [Header("Wander Variables")]
    [SerializeField]
    public float wanderRadius = 30f;
    public float wanderTimer = 10f;
    [SerializeField]
    public float wanderTick = 1;

    /*   Patrol Variables   */

    /*   Seek Variables   */
    [HideInInspector]
    public Vector3 destination;

    /*   Ghost Upgrade Variables   */
    [Header("ItemCollection related")]
    public GameObject player;
    private ItemCollection itemsCollectionCS;
    private bool stage1 = false;
    private bool stage2 = false;
    private bool stage3 = false;
    private bool stage4 = false;
    private PlayerMovement playerMovementCS;

    // Use this for initialization
    void Start()
    {
        FSM = new FiniteStateMachine<GhostAI>(this);

        //start in wander state
        FSM.ChangeState(WanderState.GetInstance);

        NMA = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
        itemsCollectionCS = player.GetComponent<ItemCollection>();
        playerMovementCS = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHeardSomething)
        {
            FSM.ChangeState(SeekState.GetInstance);
        }
        if(sight.visibleTargets.Count > 0)
        {
            destination = sight.visibleTargets[0].gameObject.transform.position;
            FSM.ChangeState(SeekState.GetInstance);
        }
        if(FSM.currentState == PatrolState.GetInstance)
        {
            NMA.speed = patrolSpeed;
        }


        //Ryan's totaly awsome bool toggling ghost buffs
        switch (itemsCollectionCS.currentNumberOfItems)
        {
            case 1:
                //Increase ghost speed
                if (stage1 == false)
                {
                    patrolSpeed *= 2;
                    NMA.speed = patrolSpeed;
                }
                stage1 = true;
                break;
            case 2:
                //Increase ghost hearing
                if (stage2 == false)
                    playerMovementCS.ghostSoundResponceLvl *= 2;
                stage2 = true;
                break;
            case 3:
                //Ghost starts teleporting to its waypoints
                //set the next waypoint as the destination
                //destination = connectedWayPatrol.currentWayPoint.transform;
                //Ignore remaining distance to next target
                //stageThree = true;
                //Wait before teleporting
                //connectedWayPatrol.waiting = true;
                //Teleport
                //gameObject.transform.position = connectedWayPatrol.currentWayPoint.transform.position;
                break;
            case 4:
                //Ghost duplicates it self
                //if (stage4 == false)
                //    Instantiate(Clone, transform.position, transform.rotation);
                //stage4 = true;
                break;
            default:
                break;
        }

        Debug.Log(FSM.currentState.stateName);
        //update current state
        FSM.Update();
    }

    public void HearSomething(Vector3 position)
    {
        hasHeardSomething = true;
        destination = position;
        heardSomethingAnim.SetTrigger("hasHeardSomething");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ryan's totaly bestest game over trigger
        if(other.gameObject.layer == 8)
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Touched le ghost");
        }
    }
}
