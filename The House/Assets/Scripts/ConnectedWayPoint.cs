using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Code was taken from a youtube tutorial by https://www.youtube.com/watch?v=5OkmcKtB3a4&index=4&list=PLokhY9fbx05dodzlBfYsKrUSVk5oVactQ
public class ConnectedWayPoint : MonoBehaviour
{

    //[SerializeField]
    //protected float connectivityRadius = 50f;

    [SerializeField]
    protected float debugDrawRadius = 1.0f;

    ConnectedWayPoint nextWayPoint;
    List<ConnectedWayPoint> connections;
    public GameObject[] allWayPoints;

    public void Start()
    {
        //Create a list of waypoints
        connections = new List<ConnectedWayPoint>();

        //allWayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
       
        //Check if they're a connected waypoint
        for (int i = 0; i < allWayPoints.Length; i++)
        {
            nextWayPoint = allWayPoints[i].GetComponent<ConnectedWayPoint>();

            //i.e we found a waypoint
            //TO DO change from range connection to itteration connection i.e waypoint one is connected to waypoint two
            if (nextWayPoint != null)
            {
                if (nextWayPoint != this)
                    connections.Add(nextWayPoint);

                //Old Radius connectivity code do not delete
                //if (/*Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius &&*/)
                //{
                //    
                //}
            }
        }
    }

    public ConnectedWayPoint NextWayPoint(ConnectedWayPoint previousWaypoint)
    {
        if (connections.Count == 0)
        {
            //No waypoints return null
            Debug.LogError("No Way Points");
            return null;
        }
        else if (connections.Count == 1 && connections.Contains(previousWaypoint))
        {
            //Only one way point in range
            return previousWaypoint;
        }
        else //Find a random waypoint
        {
            int nextIndex = 0;
            do
            {
                nextIndex = UnityEngine.Random.Range(0, connections.Count);
                nextWayPoint = connections[nextIndex];
               // Debug.Log("Random next Node " + nextIndex);

            } while (nextWayPoint == previousWaypoint);
            return nextWayPoint;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);

        Gizmos.color = Color.yellow;
        if (nextWayPoint != null)
            Gizmos.DrawLine(transform.position, nextWayPoint.transform.position);
        //Gizmos.DrawWireSphere(transform.position, connectivityRadius);
    }
}
