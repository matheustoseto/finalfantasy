using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateControl : CharacterState {

    private Transform playerTarget = null;

    private EnemyMoveControl moveControl;

    // Way Points //
    [Header("Patrol:")]
    [SerializeField] private Vector3 monitoringPoint = Vector3.zero;
    [SerializeField] private bool isPatrolStart = false;
    private bool isPatrolCompleted = false;
    private Path path = null;
    private List<Transform> listWayPoints = new List<Transform>();
    private int actualWp = 0;

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
    void Start()
    {
        #region WayPoints
        path = GetComponent<Path>();
        if (path != null)
        {
            if (path.WayPoints != null && path.WayPoints.Count > 0 && path.WayPoints[0] != null)
            {
                listWayPoints = path.WayPoints;
            }
            else
            {
                listWayPoints.Clear();
                listWayPoints.Add(transform);
            }
        }
        #endregion

        playerTarget = IcarusPlayerController.GetInstance();

        if (state != TypeStateCharacter.FakeDead)
        {
            if (state == TypeStateCharacter.Patrol)
                isPatrolStart = true;
            EnterState(TypeStateCharacter.Rise);
        }
        else
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
        monitoringPoint = listWayPoints[actualWp].position;
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
        #region Action
        // Stop - Idle //
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        //float pointDistance = Distance(wayPoint.position, transform.position);
        //if (pointDistance > radiusLimitMonitoringPoint)
        //{
        //    // Personagem muito longe do ponto de origem //
        //    EnterState(TypeStateCharacter.Follow);
        //    return;
        //}

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
        #endregion

    }

    protected override void UpdateAttackState()
    {
        #region Action
        // Olhar para o player //
        moveControl.LookAt(playerTarget.position);
        #endregion

        #region Transtions
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);
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
        #endregion
    }

    protected override void UpdateFollowState()
    {
        #region Action
        moveControl.Move(playerTarget.position);
        moveControl.LookAt(playerTarget.position);
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);

        if (pointDistance > radiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Back);
            return;
        }

        float playerDistance = Distance(playerTarget.position, transform.position);
        if (playerDistance > radiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Back);
            return;
        }

        if (playerDistance <= radiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            EnterState(TypeStateCharacter.Attack);
            return;
        }
        #endregion
    }

    protected override void UpdatePatrolState()
    {

        #region Action
        moveControl.Move(listWayPoints[actualWp].position);
        moveControl.LookAt(listWayPoints[actualWp].position);
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float playerDistance = Distance(playerTarget.position, transform.position);
        if (playerDistance <= radiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Follow);
            return;
        }

        float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);
        if (pointDistance <= radiusMonitoringPoint)
        {
            // Chegou no destino //
            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void UpdateBackState()
    {

        #region Action
        moveControl.Move(listWayPoints[actualWp].position);
        moveControl.LookAt(listWayPoints[actualWp].position);
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);
        Debug.Log("pointDistance: " + pointDistance);
        if (pointDistance <= radiusMonitoringPoint)
        {
            // Chegou a ponto de origem //
            // radiusMonitoringPoint precisa ser igual ao navAgent.StoppingDistance //

            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void UpdateRiseState()
    {
        #region Action
        // Rise //
        #endregion

        #region Transtions
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void UpdateFakeDeadState()
    {
        #region Action
        // Fake Dead //
        #endregion

        #region Transtions
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            float playerDistance = Distance(playerTarget.position, transform.position);
            if (playerDistance <= radiusDetectPlayer)
            {
                EnterState(TypeStateCharacter.Rise);
                return;
            }

            if (isPatrolStart)
            {
                timer += Time.deltaTime;
                if (timer >= timerWait)
                {
                    timer = 0;
                    EnterState(TypeStateCharacter.Rise);
                }
            }
        }
        #endregion

    }

    protected override void UpdateFallState()
    {  
        #region Action
        // Fall //
        #endregion

        #region Transtions
        if (aniControl.IsAnimationCurrentName(state.ToString()) && aniControl.IsAnimationCurrentOver())
        {
            EnterState(TypeStateCharacter.FakeDead);
            return;
        }
        #endregion
    }

    protected override void UpdateDeadState()
    {
        #region Action
        // Dead //
        #endregion

        #region Transtions
        // No transtions //
        #endregion
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
        actualWp++;
        if (actualWp >= listWayPoints.Count)
        {
            actualWp = 0;
            if (!isPatrolStart)
                isPatrolCompleted = true;
        }
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
        actualWp = 0;
        isPatrolCompleted = false;
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
