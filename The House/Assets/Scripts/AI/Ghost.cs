using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using SIGHT;
public class Ghost : MonoBehaviour
{

    //Sets the player/patrol point as the desired destination
    [SerializeField]
    public Transform destination;

    NavMeshAgent navMeshAgent;

    //access to the sight.cs
    Sight enemySight;

    
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemySight = this.GetComponent<Sight>();

        //Nav mesh failed
        if (navMeshAgent == null)
        {
            Debug.LogError("The Nav Mesh agent component is not attached to " + gameObject.name);
            return;
        }
    }

    
    void Update()
    {
        //Checks if the Player is within the Ghosts vision
        if (enemySight.visibleTargets.Capacity > 0)
            SetDestination();
        else
        {
            //partrol / Search
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Game Over on player collision
        if (collision.gameObject.layer == 8)
        {
            destination = null;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

}
