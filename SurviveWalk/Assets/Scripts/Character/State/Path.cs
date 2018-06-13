using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    [SerializeField] private Color sphereColor = new Color(1, 1, 0);
    [SerializeField] private Color lineColor   = new Color(1, 1, 0);
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    public List<Transform> WayPoints { get { return wayPoints; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnDrawGizmos()
    {

        if (wayPoints.Count == 0)
        {
            return;
        }

        float radiusSphere = 1;

        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(wayPoints[0].position, radiusSphere);
        for (int i = 1; i < wayPoints.Count; i++)
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawWireSphere(wayPoints[i].position, radiusSphere);
            Gizmos.color = lineColor;
            Gizmos.DrawLine(wayPoints[i-1].position, wayPoints[i].position);
        }

        if (wayPoints.Count > 2)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(wayPoints[0].position, wayPoints[wayPoints.Count-1].position);
        }
        
    }


}
