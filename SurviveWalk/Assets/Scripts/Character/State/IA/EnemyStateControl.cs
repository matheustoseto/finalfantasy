using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateControl : CharacterState {

    private Transform playerTarget = null;

    [SerializeField] private float radius = 10f;

    // Use this for initialization
    void Start () {
        playerTarget = IcarusPlayerController.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
        //PlayerInRange();

        UpdateState();
	}


    #region EnterState
    protected override void EnterMoveState()
    {
        
    }

    protected override void EnterAttackState()
    {
        
    }

    protected override void EnterActionState()
    {
        
    }

    protected override void EnterFollowState()
    {
        
    }


    protected override void EnterPatrolState()
    {
        
    }

    #endregion

    #region UpdateState

    protected override void UpdateMoveState()
    {
        //float distance = Vector3.Distance(playerTarget.position, transform.position);

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

    protected override void UpdateAttackState()
    {

    }

    protected override void UpdateActionState()
    {
        
    }
    protected override void UpdateFollowState()
    {


    }

    protected override void UpdatePatrolState()
    {
        
    }


    #endregion

    #region LeaveState
    protected override void LeaveMoveState()
    {

    }

    protected override void LeaveAttackState()
    {

    }

    
    protected override void LeaveActionState()
    {

    }

    protected override void LeaveFollowState()
    {
        
    }

    protected override void LeavePatrolState()
    {
        
    }

    #endregion


    //private void OnTriggerStay(Collider other)
    //{
    //    if ("Player".Equals(other.tag) && !attack && enemy.realrised)
    //    {
    //        other.GetComponent<CharacterStatus>().RemoveLife(enemyStats.Power);
    //        attack = true;
    //        animator.SetTrigger("isAttack");
    //        timerAttack = 0.9f;
    //    }
    //}

    

    void lookToPlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
