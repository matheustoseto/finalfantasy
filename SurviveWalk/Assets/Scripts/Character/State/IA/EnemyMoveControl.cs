using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveControl : MonoBehaviour {

    private NavMeshAgent agent;
    


    
    private Vector3 enemyInitialPos;


    public float Magnitude { get { return agent.velocity.magnitude; } }


    // Use this for initialization
    void Start () {
        
        enemyInitialPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        

    }


    public void Move(Vector3 pointReference)
    {
        
        agent.isStopped = false;
        agent.SetDestination(pointReference);
        //navAg.speed = speed;
    }

    public void Stop()
    {

        agent.isStopped = true;
    }


    

    

}
