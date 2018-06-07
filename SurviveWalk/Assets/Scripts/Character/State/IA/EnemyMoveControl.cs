using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveControl : MonoBehaviour {

    private NavMeshAgent agent;
    


    
    private Vector3 enemyInitialPos;


    // Use this for initialization
    void Start () {
        
        enemyInitialPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        

    }


    public void move(Vector3 pointReference)
    {
        
        agent.isStopped = false;
        agent.SetDestination(pointReference);
        //navAg.speed = speed;
    }

    public void stop()
    {

        agent.isStopped = true;
    }


    

    

}
