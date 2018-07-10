using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateControl : CharacterState {
   
    #region Attributes - Components
    private Transform playerTarget = null;
    private CharacterStatus playerStatus = null;
    private EnemyAnimationControl aniControl = null;
    private EnemyMoveControl moveControl = null;
    private Path path = null;
    private BoxCollider boxCol = null;
    private EnemyController enemyStatus = null;
    #endregion

    #region Attributes - General
    [Header("General Settings:")]
    //[SerializeField] private Weapon weapon;
    [SerializeField] private bool isDestroy = false;
    [SerializeField] private float timerWait = 3;
    protected float timer = 0;

    [Header("Resurrection:")]
    //[SerializeField] private Weapon weapon;
    [SerializeField] private bool isAutomaticResurrection = false;
    [SerializeField] private float timeToResurrection = 20;


    [Header("Radius Distance Detect:")]
    [SerializeField] private float radiusDetectPlayer    = 10f;
    [SerializeField] private float radiusPlayerDistance  = 40f;
    [SerializeField] private float radiusMonitoringPoint = 1f;
    [SerializeField] private float radiusLimitMonitoringPoint = 60f;
    [SerializeField] private float radiusAttack = 1f;
    [SerializeField] private float radiusVillage = 50f;

    [Header("Monitoring:")]
    // Patrol //
    [SerializeField] private Transform monitoringPoint = null;
    [SerializeField] private bool isPatrolStart = false;
    private List<Transform> listWayPoints = new List<Transform>();
    private Transform villagePoint = null;
    private bool isPatrolCompleted = false;
    private int actualWp = 0;
    #endregion

    #region Attributes - Test
    // Test //
    [SerializeField] private float distancePlayerTest = 0;
    [SerializeField] private float distanceMonitoringPointTest = 0;
    [SerializeField] private float distanceVillagePointTest = 0;

    [Header("State Test:")]
    [SerializeField] private bool isNewStateTest = false;
    [SerializeField] private TypeStateCharacter stateTest = TypeStateCharacter.Rise;
    [SerializeField] private bool isAnaliseStateTest = false;
    #endregion

    #region Properties
    protected Transform             PlayerTarget {get{return playerTarget;} set{playerTarget = value;} }
    protected CharacterStatus       PlayerStatus {get{return playerStatus;} set{playerStatus = value;} }
    protected EnemyAnimationControl AniControl   {get{return aniControl  ;} set{aniControl   = value;} }
    protected EnemyMoveControl      MoveControl  {get{return moveControl ;} set{moveControl  = value;} }
    protected Path                  Path         {get{return path        ;} set{path         = value;} }
    protected BoxCollider           BoxCol       {get{return boxCol      ;} set{boxCol       = value;} }
    protected EnemyController       EnemyStatus  {get{ return enemyStatus;} set { enemyStatus = value; } }

    protected bool IsDestroy {get{return isDestroy;} set{isDestroy = value;}}
    protected float TimerWait{get{return timerWait;} set{timerWait = value;}}

    protected bool  IsAutomaticResurrection { get {return isAutomaticResurrection; } set { isAutomaticResurrection = value; } }
    protected float TimeToResurrection      { get {return timeToResurrection     ; } set { timeToResurrection      = value; } }


    protected float RadiusDetectPlayer         { get { return radiusDetectPlayer        ; } set { radiusDetectPlayer         = value; } }
    protected float RadiusPlayerDistance       { get { return radiusPlayerDistance      ; } set { radiusPlayerDistance       = value; } }
    protected float RadiusMonitoringPoint      { get { return radiusMonitoringPoint     ; } set {radiusMonitoringPoint       = value; } }
    protected float RadiusLimitMonitoringPoint { get { return radiusLimitMonitoringPoint; } set {radiusLimitMonitoringPoint  = value; } }
    protected float RadiusAttack               { get { return radiusAttack              ; } set {radiusAttack                = value; } }
    protected float RadiusVillage              { get { return radiusVillage             ; } set {radiusVillage               = value; } }

    protected Transform       MonitoringPoint   {get{return monitoringPoint  ;} set{monitoringPoint   = value;}}
    protected bool            IsPatrolStart     {get{return isPatrolStart    ;} set{isPatrolStart     = value;}}
    protected List<Transform> ListWayPoints     {get{return listWayPoints    ;} set{listWayPoints     = value;}}
    protected Transform       VillagePoint      {get{return villagePoint     ;} set{villagePoint      = value;}}
    protected bool            IsPatrolCompleted {get{return isPatrolCompleted;} set{isPatrolCompleted = value;}}
    protected int             ActualWp          {get{return actualWp         ;} set{actualWp          = value;}}


    protected float              DistancePlayerTest          {get{return distancePlayerTest         ;} set{distancePlayerTest         = value;}}
    protected float              DistanceMonitoringPointTest {get{return distanceMonitoringPointTest;} set{distanceMonitoringPointTest= value;}}
    protected float              DistanceVillagePointTest    {get{return distanceVillagePointTest   ;} set{distanceVillagePointTest   = value;}}
    protected bool               IsNewStateTest              {get{return isNewStateTest             ;} set{isNewStateTest             = value;}}
    protected TypeStateCharacter StateTest                   {get{return stateTest                  ;} set{stateTest                  = value;}}
    protected bool               IsAnaliseStateTest          {get{return isAnaliseStateTest         ;} set{isAnaliseStateTest         = value;}}

    #endregion

    #region Unity
    void Awake()
    {
        InitAwake();
    }

    protected override void InitAwake()
    {
        base.InitAwake();
        enemyStatus = GetComponentInChildren<EnemyController>();
        gameObject.name = transform.GetInstanceID() + "-" + gameObject.name;
        moveControl = GetComponentInChildren<EnemyMoveControl>();
        boxCol = GetComponentInChildren<BoxCollider>();
        villagePoint = GameObject.Find("VillagePoint").GetComponent<Transform>();

        GetComponentAniControl();
    }

    protected virtual void GetComponentAniControl()
    {
        aniControl = GetComponentInChildren<EnemyAnimationControl>();
        if (aniControl != null)
            aniControl.Weapon = Weapon;
        else
            Debug.Log("[EnemyStateControl][GetComponentAniControl]: EnemyAnimationControl is null!");
    }

    // Use this for initialization
    void Start()
    {
        InitStart();
    }

    protected virtual void IniState()
    {
        if (state != TypeStateCharacter.FakeDead)
        {
            if (state == TypeStateCharacter.Patrol)
                isPatrolStart = true;
            EnterState(TypeStateCharacter.Rise);
        }
        else
            EnterState(state);
    }
    protected override void InitStart()
    {
        base.InitStart();
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
                else
                {
                    for (int i = 0; i < listWayPoints.Count; i++)
                    {
                        if (listWayPoints[i] == null)
                        {
                            listWayPoints.Clear();
                            listWayPoints.Add(transform);
                            Debug.Log("[EnemyState][" + gameObject.name + "]: A posição (" + i + ") da listWayPoins está nula.");
                            isError = true;
                            break;
                        }
                    }
                }
            }

            if (isError)
            {
                listWayPoints = new List<Transform>();
                listWayPoints.Add(transform);
            }

        }
        monitoringPoint = listWayPoints[actualWp];
        #endregion

        moveControl.BodyMiniMap.gameObject.SetActive(false);
        playerTarget = IcarusPlayerController.GetInstance();
        playerStatus = playerTarget.gameObject.GetComponent<CharacterStatus>();

        IniState();
    }
	

    protected virtual void TestMethold()
    {
        distancePlayerTest = Distance(playerTarget.position, transform.position);
        distanceMonitoringPointTest = Distance(monitoringPoint.position, transform.position);
        distanceVillagePointTest = Distance(villagePoint.position, transform.position);

        if (isNewStateTest)
        {
            // Test //
            aniControl.StateTest = stateTest;
            isNewStateTest = false;
            EnterState(stateTest);
        }

        if (isAnaliseStateTest)
        {
            // Test //
            aniControl.StateTest = stateTest;
        }

    }

    // Update is called once per frame
    void Update () {

        TestMethold();

        UpdateState();
        aniControl.ExecuteAnimations();

        if (Distance(villagePoint.position, transform.position) < radiusVillage)
        {
            EnterState(TypeStateCharacter.Back);
        }
    }

    private void LateUpdate()
    {
        if(playerStatus.lifeProgress <= 0 
            && AlertStateActivated())
        {
            EnterState(TypeStateCharacter.Back);
        }
    }
    #endregion
    
    #region StateMachine

    #region StateMachine - EnterState
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
        //AnimationMove();
    }

    protected override void EnterPatrolState()
    {
        AnimationMove();
        monitoringPoint = listWayPoints[actualWp];
    }

    protected override void EnterBackState()
    {
        AnimationMove();
        enemyStatus.ResetLife();
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
        timer = 0;
    }
    #endregion

    #region StateMachine - UpdateState

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
            else
            //if (playerDistance <= radiusPlayerDistance)
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
        //Debug.Log("pointDistance: " + pointDistance);
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
            if (isAutomaticResurrection)
            {
                timer += Time.deltaTime;
                if (timer >= timeToResurrection)
                    EnterState(TypeStateCharacter.Rise);
            }
            else
                EnterState(TypeStateCharacter.Dead);
        }
        #endregion
    }

    #endregion

    #region StateMachine - LeaveState
    protected override void LeaveState()
    {
        base.LeaveState();
        if (aniControl != null)
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
        MoveControl.Stop();
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
        if (isDestroy)
        {
            Destroy(gameObject);
            return;
        }

        enemyStatus.ResetLife();
        timer = 0;
    }

    #endregion

    #endregion

    #region Util Metholds
    private void AnimationMove()
    {
        aniControl.IsLocomotion = true;
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
    }

    protected float Distance(Vector3 pointA, Vector3 pointB)
    {
        pointA.y = 0;
        pointB.y = 0;
        return Vector3.Distance(pointA, pointB);
    }


    protected virtual void Activated()
    {
        moveControl.BodyMiniMap.SetActive(true);
        boxCol.enabled = false;
    }

    protected virtual void Desactivated()
    {
        moveControl.BodyMiniMap.SetActive(false);
        isPatrolCompleted = false;
        boxCol.enabled = false;
    }
    #endregion

    #region Events

    public override void EventPatrol()
    {
        
    }

    public override void EventBack()
    {
        //EnterState(TypeStateCharacter.Back);
    }

    #endregion
}
