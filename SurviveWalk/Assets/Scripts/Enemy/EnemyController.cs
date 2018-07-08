using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public EnemyStateControl enemy;
    //public NavMeshAgent agent;
    public Material OnAttack;
    public Renderer Body;
    public float radius = 10f;
    public GameObject lifeBar;
    public GameObject hitPopUp;
    public Utils.EnemyType enemyType;

    Vector3 enemyInitialPos;
    Transform playerTarget;
      
    Enemy enemyStats;

    private float timer = 0.5f;
    private bool removeLife = false;
    
    private Material originalMaterial;

    private bool attack = false;
    private float timerAttack = 0.9f;

    [SerializeField] private int lifeTotal;


    public float HPPercent { get { return (float)enemyStats.Life / (float)lifeTotal; } }
    

    // Use this for initialization
    void Start () {
        originalMaterial = Body.material;
        enemyInitialPos = transform.position;
        playerTarget = PlayerManager.Instance.player.transform;
        enemyStats = Inventory.Instance.GetEnemyData(enemyType.GetHashCode());
        lifeTotal = enemyStats.Life;
    }
	
	// Update is called once per frame
	void Update () {
        if (!TypeStateCharacter.FakeDead.Equals(enemy.State))
        {
            if (!lifeBar.activeSelf)
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

            ////Player in range = chase!
            //if (distance <= radius)
            //{
            //    agent.SetDestination(playerTarget.position);
            //    lookToPlayer();

            //    if (distance <= agent.stoppingDistance)
            //    {
            //        //PUT Attack the player HERE!!
            //        lookToPlayer();
            //    }
            //}

            ////Return to original spawn point + put full HP
            //else
            //{
            //    agent.SetDestination(enemyInitialPos);
            //    //PUT FULL HP HERE!!
            //}
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
        if (!TypeStateCharacter.FakeDead.Equals(enemy.State) && !removeLife && enemyStats.Life > 0)
        {
            timer = 0.3f;
            removeLife = true;

            Body.material = OnAttack;

            enemyStats.Life -= life;
            lifeBar.GetComponent<Image>().fillAmount = (float) enemyStats.Life / (float) lifeTotal;
   
            GameObject hit = Instantiate(hitPopUp, transform.position + new Vector3(0,6,0), hitPopUp.transform.rotation);
            hit.GetComponent<HitPopUp>().SetText(life.ToString());

            if (enemyStats.Life <= 0)
            {
                enemy.EventDead();

                if (NpcController.Instance.npcType.Equals(Utils.NpcType.Npc5))
                {
                    CompletQuest completQuest = new CompletQuest();
                    completQuest.questId = 2;
                    completQuest.taskId = 0;

                    NpcController.Instance.questNpc.CompletTaskQuest(completQuest);
                }
            }
                
            return true;
        }
        return false;
    }

    public void Attack()
    {
        if (!attack && TypeStateCharacter.Attack.Equals(enemy.State))
        {
            PlayerManager.Instance.player.GetComponent<CharacterStatus>().RemoveLife(enemyStats.Power);
            attack = true;
            timerAttack = 0.9f;
        }  
    }

    public void ResetLife()
    {
        enemyStats.Life = lifeTotal;
        lifeBar.GetComponent<Image>().fillAmount = enemyStats.Life / lifeTotal;
    }


}
