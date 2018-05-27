using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public Material OnAttack;
    public Renderer Body;
    public float radius = 10f;
    public GameObject lifeBar;
    public GameObject hitPopUp;
    public Utils.EnemyType enemyType;

    Vector3 enemyInitialPos;
    Transform playerTarget;
    NavMeshAgent agent;
    EnemyAnimator enemy;
    Animator animator;

    Enemy enemyStats;

    private float timer = 0.3f;
    private bool removeLife = false;
    
    private Material originalMaterial;

    private bool attack = false;
    private float timerAttack = 0.9f;

    private float lifeTotal;
   
    // Use this for initialization
    void Start () {
        originalMaterial = Body.material;
        enemyInitialPos = transform.position;
        playerTarget = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyAnimator>();
        animator = GetComponent<Animator>();

        enemyStats = Inventory.Instance.GetEnemyData(enemyType.GetHashCode());
        lifeTotal = enemyStats.Life;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemy.realrised)
        {
            if(!lifeBar.activeSelf)
                lifeBar.SetActive(true);

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
            if (distance <= radius)
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
	}

    //Looks to player
    void lookToPlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public bool RemoveLife(int life)
    {
        if (enemy.realrised && !removeLife)
        {
            timer = 0.3f;
            removeLife = true;

            Body.material = OnAttack;

            enemyStats.Life -= life;
            lifeBar.GetComponent<Image>().fillAmount = enemyStats.Life / lifeTotal;
   
            GameObject hit = Instantiate(hitPopUp, transform.position + new Vector3(0,6,0), hitPopUp.transform.rotation);
            hit.GetComponent<HitPopUp>().SetText(life.ToString());

            if (enemyStats.Life <= 0)
                Destroy(gameObject);

            return true;
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if ("Player".Equals(other.tag) && !attack && enemy.realrised)
        {         
            other.GetComponent<CharacterStatus>().RemoveLife(enemyStats.Power);
            attack = true;
            animator.SetTrigger("isAttack");
            timerAttack = 0.9f;
        }
    }

}
