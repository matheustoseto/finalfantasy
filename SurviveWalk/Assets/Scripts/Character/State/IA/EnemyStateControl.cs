using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateControl : CharacterState {

    private Transform playerTarget = null;

    private EnemyMoveControl moveControl;

    [SerializeField] private Transform monitoringPoint = null;

    [Header("Radius Distance Detect:")]
    [SerializeField] private float radiusDetectPlayer    = 10f;
    [SerializeField] private float radiusPlayerDistance  = 40f;
    [SerializeField] private float radiusMonitoringPoint = 1f;
    [SerializeField] private float radiusLimitMonitoringPoint = 60f;
    [SerializeField] private float radiusAttack = 1f;

    [Header("Timer:")]
    [SerializeField] private float timerWait = 3;
    private float timer = 0;


    [Header("Tools / Weapon:")]
    [SerializeField] private Weapon weapon;

    // Estados //
    bool isPatrolCompleted = false;


    [Header("Test")]
    [SerializeField] private TypeStateCharacter newState = TypeStateCharacter.Rise;
    [SerializeField] private bool isChangeState = false;
    [SerializeField] private float distancePlayerTest = 0;
    [SerializeField] private Transform wayPoint;
    [SerializeField] private float distancewayPoint = 0;

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

        // Teste //
        distancePlayerTest = Distance(playerTarget.position, transform.position);
        distancewayPoint = Distance(wayPoint.position, transform.position);
        // Fim teste // */

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
        timer = 0;

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
    protected override void EnterFakeDeadState()
    {
        aniControl.IsFakeDead = true;
    }
    protected override void EnterFallState()
    {
        aniControl.IsFall = true;

        // Reset
        moveControl.BodyMiniMap.SetActive(false);
        isPatrolCompleted = false;
    }

    protected override void EnterDeadState()
    {
        aniControl.IsDead = true;
    }

    #endregion

    #region UpdateState

    protected override void UpdateMoveState()
    {



        // Transições //

        float pointDistance = Distance(wayPoint.position, transform.position);
        if (pointDistance > radiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Follow);
            return;
        }

        float playerDistance = Distance(playerTarget.position, transform.position);
        if (playerDistance <= radiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            EnterState(TypeStateCharacter.Attack);
            return;
        }

        if (playerDistance <= radiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Follow);
            return;
        }

        isPatrolCompleted = true;
        timer += Time.deltaTime;
        if (timer >= timerWait)
        {
            timer = 0;
            if (isPatrolCompleted)
            {
                // Completou a patrulha e aguardou no ponto de origem //
                EnterState(TypeStateCharacter.Fall);
                return;
            }
            else
            {
                // pega o NextPoint
                // e segue para o estado de Patrulha
                EnterState(TypeStateCharacter.Patrol);
            }
        }

    }

    protected override void UpdateAttackState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            float pointDistance = Distance(wayPoint.position, transform.position);
            if (pointDistance > radiusLimitMonitoringPoint)
            {
                // Personagem muito longe do ponto de origem //
                EnterState(TypeStateCharacter.Follow);
                return;
            }

            float playerDistance = Distance(playerTarget.position, transform.position);
            if (playerDistance <= radiusAttack)
            {
                // Detectou o jogador e está em área de ataque //
                EnterState(TypeStateCharacter.Move);
                return;
            }

            if (playerDistance <= radiusPlayerDistance)
            {
                // Detectou o jogador //
                EnterState(TypeStateCharacter.Follow);
                return;
            }
            return;
        }
    }

    protected override void UpdateFollowState()
    {

        float pointDistance = Distance(wayPoint.position, transform.position);
        if (pointDistance > radiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Back);
            return;
        }

        float playerDistance = Distance(playerTarget.position, transform.position);
        if (playerDistance <= radiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            EnterState(TypeStateCharacter.Attack);
            return;
        }

    }

    protected override void UpdatePatrolState()
    {

        float playerDistance = Distance(playerTarget.position, transform.position);
        if (playerDistance <= radiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Follow);
            return;
        }

        float pointDistance = Distance(wayPoint.position, transform.position);
        if (pointDistance <= radiusMonitoringPoint)
        {
            // Chegou no destino //
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateBackState()
    {
        
        // Transições //
        float pointDistance = Distance(wayPoint.position, transform.position);
        if (pointDistance <= radiusMonitoringPoint)
        {
            // Chegou a ponto de origem //
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateRiseState()
    {

        // Transições //
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
    }

    protected override void UpdateFakeDeadState()
    {

        // Transições //
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            float playerDistance = Distance(playerTarget.position, transform.position);
            if (playerDistance <= radiusDetectPlayer)
            {
                EnterState(TypeStateCharacter.Rise);
                return;
            }

        }

    }

    protected override void UpdateFallState()
    {

        // Transições //
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.FakeDead);
            return;
        }
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
        aniControl.Release();
    }

    protected override void LeaveRiseState()
    {
        
    }

    protected override void LeaveFakeDeadState()
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

    private float Distance(Vector3 pointA, Vector3 pointB)
    {
        pointA.y = 0;
        pointB.y = 0;
        return Vector3.Distance(pointA, pointB);
    }
}
