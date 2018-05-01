using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Material OnAttack;
    public Renderer Body;
    public float radius = 10f;
    public float life = 10;
    public float power = 2;

    Vector3 enemyInitialPos;
    Transform playerTarget;
    NavMeshAgent agent;

    private float timer = 0.3f;
    private bool removeLife = false;
    
    private Material originalMaterial;

    private bool attack = false;
    private float timerAttack = 0.9f;
   
    // Use this for initialization
    void Start () {
        originalMaterial = Body.material;
        enemyInitialPos = transform.position;
        playerTarget = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        if (removeLife)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                removeLife = false;
                Body.material = originalMaterial;
            }
        }

        if (attack)
        {
            timerAttack -= Time.deltaTime;

            if (timerAttack <= 0)
            {
                attack = false;
            }
        }

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

    public void RemoveLife(float life)
    {
        if (!removeLife)
        {
            timer = 0.3f;
            removeLife = true;

            Body.material = OnAttack;

            print(life);
            this.life -= life;

            if (this.life <= 0)
                Destroy(gameObject);
        }       
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && !attack)
        {
            other.GetComponent<CharacterStatus>().RemoveLife(power);
            attack = true;
            timerAttack = 0.9f;
        }
    }

}
