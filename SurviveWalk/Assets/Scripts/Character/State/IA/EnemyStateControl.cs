using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateControl : CharacterState {
    private Transform playerTarget = null;
    private CharacterStatus playerStatus = null;
    private BoxCollider boxCol = null;

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

    

    [Header("Actual Distances:")]
    [SerializeField] private float distancePlayerTest = 0;
    [SerializeField] private float distanceMonitoringPoint = 0;

    private EnemyAnimationControl aniControl = null;

    private void Awake()
    {
        gameObject.name = transform.GetInstanceID() + "-" +gameObject.name;

        aniControl = GetComponentInChildren<EnemyAnimationControl>();
        moveControl = GetComponentInChildren<EnemyMoveControl>();
        boxCol = GetComponentInChildren<BoxCollider>();

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
            bool isError = false;
            listWayPoints = path.WayPoints;
            if (listWayPoints == null)
            {
                isError = true;
                Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins nula.");
            }
            else
            {
                if (listWayPoints.Count == 0)
                {
                    Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins vazia.");
                    isError = true;
                }
                else { 
                    for (int i = 0; i < listWayPoints.Count; i++)
                    {
                        if (listWayPoints[i] == null)
                        {
                            listWayPoints.Clear();
                            listWayPoints.Add(transform);
                            Debug.Log("[EnemyState][" + gameObject.name + "]: A posição ("+i+") da listWayPoins está nula.");
                            isError = true;
                            break;
                        }
                    }
                }
            }

            if(isError)
            {
                listWayPoints = new List<Transform>();
                listWayPoints.Add(transform);
            }

        }
        monitoringPoint = listWayPoints[actualWp].position;
        #endregion

        playerTarget = IcarusPlayerController.GetInstance();
        playerStatus = playerTarget.gameObject.GetComponent<CharacterStatus>();

        if (state != TypeStateCharacter.FakeDead)
        {
            if (state == TypeStateCharacter.Patrol)
                isPatrolStart = true;
            EnterState(TypeStateCharacter.Rise);
        }
        else
            EnterState(state);
    }
	
	// Update is called once per frame
	void Update () {

        // Teste //
        distancePlayerTest = Distance(playerTarget.position, transform.position);
        distanceMonitoringPoint = Distance(monitoringPoint, transform.position);
        // Fim teste // */

        UpdateState();
        aniControl.ExecuteAnimations();
    }

    private void LateUpdate()
    {
        if(playerStatus.lifeProgress <= 0)
        {
            EnterState(TypeStateCharacter.Back);
        }
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

        Activated();
    }
    protected override void EnterFakeDeadState()
    {
        aniControl.IsFakeDead = true;
    }
    protected override void EnterFallState()
    {
        aniControl.IsFall = true;
        Desactivated();
    }

    protected override void EnterDeadState()
    {
        aniControl.IsDead = true;
        Desactivated();
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
        if (aniControl.IsAnimationFinish(state.ToString()))
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
        if (aniControl.IsAnimationFinish(state.ToString()))
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
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            EnterState(TypeStateCharacter.FakeDead);
            return;
        }
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
        //Destroy(gameObject);
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


    private void Activated()
    {
        moveControl.BodyMiniMap.SetActive(true);
        boxCol.enabled = false;
    }


    private void Desactivated()
    {
        moveControl.BodyMiniMap.SetActive(false);
        isPatrolCompleted = false;
        boxCol.enabled = false;
    }
}
