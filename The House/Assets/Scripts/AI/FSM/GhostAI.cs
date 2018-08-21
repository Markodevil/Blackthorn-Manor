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
    [HideInInspector]
    public Sight sight;
    public float dist;
    /*   My Things   */
    [HideInInspector]
    public NavMeshAgent NMA;

    /*   Public Variables   */
    public float patrolSpeed = 1.5f;
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
    private GameObject player;
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

    private string ghostName;
    private GameObject singleton;
    private MenuManager mm;

    // Use this for initialization
    void Start()
    {
        singleton = GameObject.FindGameObjectWithTag("Singleton");
        if (singleton)
            mm = GameObject.FindGameObjectWithTag("Singleton").GetComponent<MenuManager>();
        FSM = new FiniteStateMachine<GhostAI>(this);
        player = GameObject.FindGameObjectWithTag("Player");

        NMA = this.GetComponent<NavMeshAgent>();
        sight = this.GetComponent<Sight>();
        if (player != null)
        {
            itemsCollectionCS = player.GetComponent<ItemCollection>();
            playerMovementCS = player.GetComponent<PlayerMovement>();
        }
        hearingTrigger = this.GetComponent<SphereCollider>();

        hearingTrigger.radius = hearingRange;

        ghostName = GameObject.FindGameObjectWithTag("Ghost").name;

        //start in wander state
        FSM.ChangeState(PatrolState.GetInstance);
    }

    //For assigning var's on re-try
    //private void OnLevelWasLoaded(int level)
    //{
    //    FSM = new FiniteStateMachine<GhostAI>(this);
    //
    //    //Grabing the players components
    //    player = GameObject.FindGameObjectWithTag("Player");
    //
    //    if (player != null)
    //    {
    //        itemsCollectionCS = player.GetComponent<ItemCollection>();
    //        playerMovementCS = player.GetComponent<PlayerMovement>();
    //    }
    //
    //    //start in wander state
    //    FSM.ChangeState(PatrolState.GetInstance);
    //
    //    NMA = this.GetComponent<NavMeshAgent>();
    //    sight = this.GetComponent<Sight>();
    //    hearingTrigger = this.GetComponent<SphereCollider>();
    //
    //    hearingTrigger.radius = hearingRange;
    //}
    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        Vector3 Direction = currentPosition - playerPosition;

        Direction.Normalize();

        dist = Vector3.Distance(playerPosition, currentPosition);
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
                time += Time.deltaTime;
                if (time >= 3f)
                {
                    gameObject.transform.position = NMA.destination;
                    time = 0;
                }
                break;
            case 4:
                //Ghost duplicates it self
                if (this.name != ghostName + "(Clone)")
                {
                    if (stage4 == false)
                        Instantiate(Clone, new Vector3(-6.7f, 0, -12.18f), transform.rotation);
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
        if (CalulatePathLength(position) <= hearingRange)
        {
            hasHeardSomething = true;
            destination = position;
            heardSomethingAnim.SetTrigger("hasHeardSomething");
        }
    }

   private void OnTriggerEnter(Collider other)
   {
       //Ryan's totaly bestest game over trigger
       if (other.gameObject.layer == 8 && dist < 4)
       {
           PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
           SceneManager.LoadScene("GameOver");
           Debug.Log("Touched le ghost");
       }
   }

   // private void OnCollisionEnter(Collision collision)
   // {
   //     if (collision.gameObject.layer == 8)
   //     {
   //         Cursor.visible = true;
   //         Cursor.lockState = CursorLockMode.Confined;
   //         PlayerPrefs.SetString("lastLoadedScene", SceneManager.GetActiveScene().name);
   //         if (mm)
   //         {
   //             mm.sceneName = "GameOver";
   //             mm.fade.ResetTrigger("FadeIn");
   //             mm.fade.SetTrigger("FadeOut");
   //
   //         }
   //         else
   //         {
   //             SceneManager.LoadScene("GameOver");
   //
   //         }
   //         Debug.Log("Touched le ghost");
   //     }
   // }

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
