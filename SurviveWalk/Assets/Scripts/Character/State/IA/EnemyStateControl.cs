using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateControl : CharacterState {

    private Transform playerTarget = null;

    private EnemyMoveControl moveControl;

    [SerializeField] private Transform monitoringPoint = null;

    [Header("Radius Distance Detect:")]
    [SerializeField] private float radiusPlayer    = 10f;
    [SerializeField] private float radiusMonitoringPoint = 30f;
    [SerializeField] private float radiusAttack = 1f;


    [Header("Tools / Weapon:")]
    [SerializeField] private Weapon weapon;

    [Header("Test")]
    [SerializeField] private TypeStateCharacter newState = TypeStateCharacter.Rise;
    [SerializeField] private bool isChangeState = false;

    private EnemyAnimationControl aniControl = null;

    private void Awake()
    {
        aniControl = GetComponentInChildren<EnemyAnimationControl>();
        moveControl = GetComponentInChildren<EnemyMoveControl>();

        if (weapon == null)
        {
            weapon = GetComponentInChildren<Weapon>();
            aniControl.Weapon = weapon;
            //try
            //{
            //}catch(Exception e)
            //{
            //    Debug.LogError("["+gameObject.name + "][EnemyStateControl][Awake]: " + e.Message);
            //}

        }
    }

    // Use this for initialization
    void Start () {
        playerTarget = IcarusPlayerController.GetInstance();

        EnterState(state);
    }
	

    void Actions()
    {

    }

	// Update is called once per frame
	void Update () {
        Actions();

        if (isChangeState)
        {
            isChangeState = false;
            EnterState(newState);
        }

        UpdateState();
        aniControl.ExecuteAnimations();
    }


    #region EnterState
    protected override void EnterMoveState()
    {
        AnimationMove();

    }

    protected override void EnterAttackState()
    {
        aniControl.IsAttack = true;
        moveControl.Stop();
    }


    protected override void EnterFollowState()
    {
        AnimationMove();
    }


    protected override void EnterPatrolState()
    {
        AnimationMove();
    }

    protected override void EnterBackState()
    {
        AnimationMove();
        
    }

    protected override void EnterRiseState()
    {
        aniControl.IsRise = true;
        moveControl.BodyMiniMap.SetActive(true);

    }

    protected override void EnterFallState()
    {
        aniControl.IsFall = true;
        moveControl.BodyMiniMap.SetActive(false);
    }

    protected override void EnterDeadState()
    {
        aniControl.IsDead = true;
    }

    #endregion

    #region UpdateState

    protected override void UpdateMoveState()
    {
        //// Ficar de guarda
        //// Ao detectar o jogador, passa para o estado Follow


        //float distance = Vector3.Distance(playerTarget.position, transform.position);

        ////Player in range = chase!
        //if (distance <= radiusPlayer)
        //{
        //    EnterState(TypeStateCharacter.Follow);
        //}

        ////Return to original spawn point + put full HP
        //else
        //{

        //}
    }

    protected override void UpdateAttackState()
    {

        //if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        //{
        //    EnterState(TypeStateCharacter.Follow);
        //    return;
        //}
    }

    protected override void UpdateFollowState()
    {
        //float distance = Vector3.Distance(monitoringPoint.position, transform.position);

        //if (distance <= radiusMonitoringPoint)
        //{
        //    EnterState(TypeStateCharacter.Back);
        //    return;
        //}

        //distance = Vector3.Distance(playerTarget.position, transform.position);
        //if (distance <= radiusAttack)
        //{
        //    EnterState(TypeStateCharacter.Attack);
        //    return;
        //}

    }

    protected override void UpdatePatrolState()
    {
        
    }

    protected override void UpdateBackState()
    {
        //moveControl.Move(monitoringPoint.position);
        //float distance = Vector3.Distance(monitoringPoint.position, transform.position);

        //if (distance <= 0.5f)
        //{
        //    EnterState(TypeStateCharacter.Move);
        //}
    }

    protected override void UpdateRiseState()
    {
        
    }

    protected override void UpdateFallState()
    {
        
    }

    protected override void UpdateDeadState()
    {
        
    }

    #endregion

    #region LeaveState
    protected override void LeaveState()
    {
        base.LeaveState();
        aniControl.Release();
    }

    protected override void LeaveMoveState()
    {

    }

    protected override void LeaveAttackState()
    {

    }

    
    protected override void LeaveFollowState()
    {
        
    }

    protected override void LeavePatrolState()
    {
        
    }

    protected override void LeaveBackState()
    {
        base.LeaveState();
        aniControl.Release();
    }

    protected override void LeaveRiseState()
    {
        
    }

    protected override void LeaveFallState()
    {
        
    }

    protected override void LeaveDeadState()
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

    private void AnimationMove()
    {
        aniControl.IsLocomotion = true;
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
    }
}
