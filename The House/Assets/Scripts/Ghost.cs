using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using SIGHT;
public class Ghost : MonoBehaviour {

    [SerializeField]
    public Transform destination;

    NavMeshAgent navMeshAgent;

    Sight enemySight;

	// Use this for initialization
	void Start () {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemySight = this.GetComponent<Sight>();

        if (navMeshAgent == null)
        {
            //Nav mesh failed
            Debug.LogError("The Nav Mesh agent component is not attached to " + gameObject.name);
            return;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (enemySight.visibleTargets.Capacity > 0)
            SetDestination();
        else
        {
            //partrol
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
