using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float radius = 10f;

    Vector3 enemyInitialPos;
    Transform playerTarget;
    NavMeshAgent agent;    
   
    // Use this for initialization
    void Start () {

        enemyInitialPos = transform.position;
        playerTarget = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(playerTarget.position, transform.position);

        //Player in range = chase!
        if(distance <= radius)
        {
            agent.SetDestination(playerTarget.position);
            lookToPlayer();

            if (distance <= agent.stoppingDistance)
            {
                //PUT Attack the player HERE!!
                lookToPlayer();
            }
        }

        //Return to original spawn point + put full HP
        else
        {
            agent.SetDestination(enemyInitialPos);
            //PUT FULL HP HERE!!
        }           
	}

    //Looks to player
    void lookToPlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
