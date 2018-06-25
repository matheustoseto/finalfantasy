using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateControl : CharacterState {

    #region Attributes - Components
    //private Transform playerTarget = null;
    //private CharacterStatus playerStatus = null;
    private NPCAnimationControl aniControl = null;
    private EnemyMoveControl moveControl = null;
    private Path path = null;
    //private BoxCollider boxCol = null;
    //private EnemyController enemyStatus = null;
    #endregion

    #region Attributes - General
    [Header("Radius Distance Detect:")]
    [SerializeField] private float radiusMonitoringPoint = 1f;

    [Header("Monitoring:")]
    [SerializeField] private Vector3 stopPoint = Vector3.zero;
    private Transform prevStopPoint = null;
    private List<Transform> listWayPoints = new List<Transform>();
    private int actualWp = 0;
    #endregion

    #region Attributes - Test
    [SerializeField] private float distanceMonitoringPointTest = 0;
    #endregion

    #region Unity
    void Awake()
    {
        InitAwake();
    }

    protected override void InitAwake()
    {
        base.InitAwake();
        prevStopPoint = transform;
        //enemyStatus = GetComponent<EnemyController>();
        gameObject.name = transform.GetInstanceID() + "-" + gameObject.name;
        aniControl = GetComponentInChildren<NPCAnimationControl>();
        moveControl = GetComponentInChildren<EnemyMoveControl>();
        //boxCol = GetComponentInChildren<BoxCollider>();


        if (aniControl != null)
            aniControl.Weapon = Weapon;
        else
            Debug.Log("[EnemyStateControl][InitAwake]: EnemyAnimationControl is null!");
    }
    // Use this for initialization
    void Start () {
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
        stopPoint = listWayPoints[actualWp].position;
        #endregion

        moveControl.BodyMiniMap.gameObject.SetActive(false);
        //playerTarget = IcarusPlayerController.GetInstance();
        //playerStatus = playerTarget.gameObject.GetComponent<CharacterStatus>();

        EnterState(state);
    }
	
	// Update is called once per frame
	void Update () {
        #region Teste
        distanceMonitoringPointTest = Distance(stopPoint, transform.position);
        #endregion

        UpdateState();
        aniControl.ExecuteAnimations();
    }
    #endregion

    #region StateMachine

    protected override void LeaveState()
    {
        base.LeaveState();
        aniControl.Release();
    }
    
    #region MoveState
    protected override void EnterMoveState()
    {
        AnimationMove();
    }

    protected override void UpdateMoveState()
    {
        #region Action
        // Stop - Idle //
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion


        #region Transtions

        #endregion
    }

    protected override void LeaveMoveState()
    {

    }

    #endregion

    #region AttackState
    protected override void EnterAttackState()
    {
        aniControl.IsAttack = true;
        moveControl.Stop();
    }

    protected override void UpdateAttackState()
    {
        #region Action
        // Olhar para o player // moveControl.LookAt(playerTarget.position);
        #endregion

        #region Transtions
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void LeaveAttackState()
    {

    }


    #endregion

    #region PatrolState

    protected override void EnterPatrolState()
    {
        AnimationMove();
        stopPoint = listWayPoints[actualWp].position;
    }

    protected override void UpdatePatrolState()
    {

        #region Action
        moveControl.Move(listWayPoints[actualWp].position);
        moveControl.LookAt(listWayPoints[actualWp].position);
        aniControl.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);
        if (pointDistance <= radiusMonitoringPoint)
        {
            // Chegou no destino //
            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void LeavePatrolState()
    {
        moveControl.RotatePosition(listWayPoints[actualWp]);
        NextPoint();
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

    private void NextPoint()
    {
        prevStopPoint = listWayPoints[actualWp];
        actualWp++;
        if (actualWp >= listWayPoints.Count)
            actualWp = 0;
    }
    #endregion

    #region Events
    public override void EventDead()
    {
        
    }

    #endregion
}
