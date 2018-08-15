using System.Collections;
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
    public float hearingRange;
    private SphereCollider hearingTrigger;
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
    public ConnectedWayPoint currentWayPoint;
    ConnectedWayPoint previousWayPoint;

    /*   Seek Variables   */
    [HideInInspector]
    public Vector3 destination;

    /*   Ghost Upgrade Variables   */
    [Header("ItemCollection related")]
    public GameObject player;
    public GameObject Clone;
    private ItemCollection itemsCollectionCS;
    private float time = 0;
    private bool stage1 = false;
    private bool stage2 = false;
    private bool stage3 = false;
    private bool stage4 = false;
    private bool CloneBuffs = false;
    [SerializeField]
    private float speedMultiplyer = 2;
    [SerializeField]
    private float soundResponceMultiplyer = 2;
    private PlayerMovement playerMovementCS;

    // Use this for initialization
    void Start()
    {
        FSM = new FiniteStateMachine<GhostAI>(this);

        //start in wander state
        FSM.ChangeState(PatrolState.GetInstance);

        NMA = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
        itemsCollectionCS = player.GetComponent<ItemCollection>();
        playerMovementCS = player.GetComponent<PlayerMovement>();
        hearingTrigger = GetComponent<SphereCollider>();

        hearingTrigger.radius = hearingRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHeardSomething)
        {
            FSM.ChangeState(SeekState.GetInstance);
        }
        if (sight.visibleTargets.Count > 0)
        {
            destination = sight.visibleTargets[0].gameObject.transform.position;
            FSM.ChangeState(SeekState.GetInstance);
        }
        if (FSM.currentState == PatrolState.GetInstance)
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
                    patrolSpeed *= speedMultiplyer;
                    NMA.speed = patrolSpeed;
                }
                stage1 = true;
                break;
            case 2:
                //Increase ghost hearing
                if (stage2 == false)
                    hearingRange *= soundResponceMultiplyer;
                stage2 = true;
                break;
            case 3:
                //Ghost starts teleporting to its waypoints
                //TO DO FOR DEEEEEEEON get reference to the currentWayPoint
                //time += Time.deltaTime;
                ////Teleport
                //if (time >= 10f)
                //{
                //    gameObject.transform.position = currentWayPoint.transform.position;
                //    time = 0;
                //}
                //break;
            case 4:
                //Ghost duplicates it self
                if (this.name != "BestGhost(Clone)")
                {
                    if (stage4 == false)
                        Instantiate(Clone, transform.position, transform.rotation);
                    stage4 = true;
                }
                else
                {
                    //Ghost Clone Buffs
                    if (CloneBuffs == false)
                    {
                        patrolSpeed *= speedMultiplyer;
                        NMA.speed = patrolSpeed;
                    }
                    CloneBuffs = true;
                }
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
        //if (CalulatePathLength(position) <= hearingRange)
        //{
            hasHeardSomething = true;
            destination = position;
            heardSomethingAnim.SetTrigger("hasHeardSomething");
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //Ryan's totaly bestest game over trigger
    //    if (other.gameObject.layer == 8)
    //    {
    //        SceneManager.LoadScene("GameOver");
    //        Debug.Log("Touched le ghost");
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("Touched le ghost");
        }
    }

    //This code was taken from a youtube tutorial from here https://www.youtube.com/watch?v=mBGUY7EUxXQ
    //It calculates the total distance to the player taking into account the amount of corners. 
    public float CalulatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (NMA.enabled)
        {
            NMA.CalculatePath(targetPosition, path);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
