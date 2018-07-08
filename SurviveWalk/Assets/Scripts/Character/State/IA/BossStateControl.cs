using UnityEngine;

public enum TypeAttack { Attack, SpecialAttack1, Random}

public class BossStateControl : EnemyStateControl {

    [System.Serializable]
    private class BossAttack : System.Collections.Generic.IComparer<BossAttack>
    {
        public static bool IsDescending = false;
        public string name = "";
        public TypeAttack type;
        [Range(0,1)]
        public float percentMin;
        [Range(0, 1)]
        public float percentMax;
        //public bool activated = true;

        public int Compare(BossAttack x, BossAttack y)
        {
            if (!IsDescending)
                return compareNumbers(x.percentMin, y.percentMin);
            else
                return compareNumbers(y.percentMin, x.percentMin);

        }

        private int compareNumbers(float x, float y)
        {
            if (x > y)
                return 1;
            else if (x < y)
                return -1;
            return 0;
        }
    }


    private BossAnimationControl aniControlBoss = null;

    [Header("Boss Settings - Attacks:")]
    [SerializeField] private System.Collections.Generic.List<BossAttack> listAttacks = new System.Collections.Generic.List<BossAttack>();
    [SerializeField] private bool isRandomAttack = false;
    [SerializeField] private float HpPercentTest = 0;

    private bool isPlayerHere = false;

    #region Unity
    void Awake()
    {
        InitAwake();
    }
    protected override void InitAwake()
    {
        base.InitAwake();

        //enemyStatus = GetComponentInChildren<EnemyController>();
        //gameObject.name = transform.GetInstanceID() + "-" + gameObject.name;
        //moveControl = GetComponentInChildren<EnemyMoveControl>();
        //boxCol = GetComponentInChildren<BoxCollider>();
        //villagePoint = GameObject.Find("VillagePoint").GetComponent<Transform>();

        #region StartPoint
        GameObject gobj = new GameObject();
        gobj.name = "BossStartPoint - " + gameObject.name;
        gobj.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
        gobj.transform.rotation = transform.rotation;
        MonitoringPoint = gobj.transform;
        #endregion

    }

    protected override void GetComponentAniControl()
    {
        aniControlBoss = GetComponentInChildren<BossAnimationControl>();
        if (aniControlBoss != null)
            aniControlBoss.Weapon = Weapon;
        else
            Debug.Log("[BossStateControl][GetComponentAniControl]: BossAnimationControl is null!");
    }


    // Use this for initialization
    void Start () {
        InitStart();
	}

    protected override void IniState()
    {
        EnterState(TypeStateCharacter.Move);
    }

    protected override void InitStart()
    {
        base.InitStart();
        #region WayPoints
        //path = GetComponent<Path>();
        //if (path != null)
        //{
        //    bool isError = false;
        //    listWayPoints = path.WayPoints;
        //    if (listWayPoints == null)
        //    {
        //        isError = true;
        //        Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins nula.");
        //    }
        //    else
        //    {
        //        if (listWayPoints.Count == 0)
        //        {
        //            Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins vazia.");
        //            isError = true;
        //        }
        //        else
        //        {
        //            for (int i = 0; i < listWayPoints.Count; i++)
        //            {
        //                if (listWayPoints[i] == null)
        //                {
        //                    listWayPoints.Clear();
        //                    listWayPoints.Add(transform);
        //                    Debug.Log("[EnemyState][" + gameObject.name + "]: A posição (" + i + ") da listWayPoins está nula.");
        //                    isError = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    if (isError)
        //    {
        //        listWayPoints = new List<Transform>();
        //        listWayPoints.Add(transform);
        //    }

        //}
        //monitoringPoint = listWayPoints[actualWp].position;
        #endregion

        //moveControl.BodyMiniMap.gameObject.SetActive(false);
        //playerTarget = IcarusPlayerController.GetInstance();
        //playerStatus = playerTarget.gameObject.GetComponent<CharacterStatus>();

        //if (state != TypeStateCharacter.FakeDead)
        //{
        //    if (state == TypeStateCharacter.Patrol)
        //        isPatrolStart = true;
        //    EnterState(TypeStateCharacter.Rise);
        //}
        //else
        //    EnterState(state);

        Activated();

        #region Attacks
        for (int i = 0; i < listAttacks.Count; i++)
        {
            listAttacks[i].name = (i + 1) + ": " + listAttacks[i].type.ToString();
            if (listAttacks[i].percentMin > listAttacks[i].percentMax)
            {
                float aux = listAttacks[i].percentMin;
                listAttacks[i].percentMin = listAttacks[i].percentMax;
                listAttacks[i].percentMax = aux;
            }
        }
        BossAttack.IsDescending = true;
        listAttacks.Sort(listAttacks[0]);
        for (int i = 0; i < listAttacks.Count; i++)
        {
            listAttacks[i].name = (i + 1) + ": " + listAttacks[i].type.ToString();
        }
        #endregion
    }

    protected override void TestMethold()
    {
        base.TestMethold();


        //distancePlayerTest = Distance(playerTarget.position, transform.position);
        //distanceMonitoringPointTest = Distance(monitoringPoint.position, transform.position);
        //distanceVillagePointTest = Distance(villagePoint.position, transform.position);

        //if (isNewStateTest)
        //{
        //    // Test //
        //    aniControlBoss.StateTest = stateTest;
        //    isNewStateTest = false;
        //    EnterState(stateTest);
        //}

        //if (isAnaliseStateTest)
        //{
        //    // Test //
        //    aniControlBoss.StateTest = stateTest;
        //}
        HpPercentTest = EnemyStatus.HPPercent;
    }

    // Update is called once per frame
    void Update () {

        TestMethold();


        UpdateState();
        aniControlBoss.ExecuteAnimations();

        //if (Distance(villagePoint.position, transform.position) < RadiusVillage)
        //{
        //    EnterState(TypeStateCharacter.Back);
        //}
    }
    #endregion


    #region StateMachine
    protected override void LeaveState()
    {
        aniControlBoss.Release();
    }


    #region MoveState
    protected override void EnterMoveState()
    {
        aniControlBoss.IsLocomotion = true;
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
    }

    protected override void UpdateMoveState()
    {
        #region Action
        // Stop - Idle //
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float playerDistance = 0;

        float pointDistance = Distance(MonitoringPoint.position, transform.position);
        if (pointDistance > RadiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Follow);
            return;
        }


        playerDistance = Distance(PlayerTarget.position, transform.position);
        if (playerDistance <= RadiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            //EnterState(TypeStateCharacter.Attack);
            AttacksVerification();
            return;
        }


        if (!isPlayerHere)
        {
            playerDistance = Distance(PlayerTarget.position, transform.position);
            if (playerDistance <= RadiusDetectPlayer)
            {
                EnterState(TypeStateCharacter.Follow);
                isPlayerHere = true;
                return;
            }
        }
        else
        {
            if (playerDistance <= RadiusPlayerDistance)
            {
                // Detectou o jogador //
                EnterState(TypeStateCharacter.Follow);
                return;
            }
        }
        #endregion

    }

    protected override void LeaveMoveState()
    {

    }

    #endregion

    #region AttacksStates

    private void AttacksVerification()
    {
        if (isRandomAttack)
        {
            if (listAttacks.Count > 1)
            {
                float rand = Random.Range(0f, 1f);
                Debug.Log("Ramdom: " + rand);

                TypeStateCharacter typeState = VerifyAttackState(rand);
                EnterState(typeState);
            }
            else if (listAttacks.Count == 1)
            {
                TypeStateCharacter typeState = AttackToState(listAttacks[0].type);
                EnterState(typeState);
            }
            else
                EnterState(TypeStateCharacter.Attack);
        }
        else
        {
            float hpPercent = EnemyStatus.HPPercent;
            TypeStateCharacter typeState = VerifyAttackState(hpPercent);
            EnterState(typeState);
        }

    }

    private TypeStateCharacter AttackToState(TypeAttack typeAttack)
    {
        switch (typeAttack)
        {
            case TypeAttack.Attack: return TypeStateCharacter.Attack;
            case TypeAttack.SpecialAttack1: return TypeStateCharacter.SpecialAttack1Start;
            default: return TypeStateCharacter.Attack;
        }
    }

    private TypeStateCharacter VerifyAttackState(float percent)
    {
        for (int i = 0; i < listAttacks.Count; i++)
        {
            if (percent >= listAttacks[i].percentMin && percent < listAttacks[i].percentMax)
                return AttackToState(listAttacks[i].type);
        }
        return TypeStateCharacter.Attack;
    }

    #region AttackState

    protected override void EnterAttackState()
    {
        aniControlBoss.IsAttack = true;
        MoveControl.Stop();
    }

    protected override void UpdateAttackState()
    {
        #region Action
        //// Olhar para o player //
        //moveControl.LookAt(playerTarget.position);
        #endregion

        #region Transtions
        if (aniControlBoss.IsAnimationFinish(state.ToString()))
        {
            TransitionsAttack();
            return;
        }
        #endregion
    }

    protected override void LeaveAttackState()
    {
        MoveControl.Stop();
    }

    private bool TransitionsAttack()
    {
        float pointDistance = Distance(MonitoringPoint.position, transform.position);
        if (pointDistance > RadiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Follow);
            return true;
        }

        float playerDistance = Distance(PlayerTarget.position, transform.position);
        if (playerDistance <= RadiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            EnterState(TypeStateCharacter.Move);
            return true;
        }
        else
        //if (playerDistance <= RadiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Follow);
            return true;
        }
    }

    #endregion

    #region SpecialAttack1StartState
    protected override void EnterSpecialAttack1StartState()
    {
        aniControlBoss.IsSpecialAttack1Start = true;
        MoveControl.Stop();
    }

    protected override void UpdateSpecialAttack1StartState()
    {
        #region Action
        //// Olhar para o player //
        //moveControl.LookAt(playerTarget.position);
        #endregion

        #region Transtions
        if (aniControlBoss.IsAnimationFinish(state.ToString()))
        {
            //TransitionsAttack();
            EnterState(TypeStateCharacter.SpecialAttack1Mid);
            return;
        }
        #endregion
    }

    protected override void LeaveSpecialAttack1StartState()
    {
        
    }
    #endregion

    #region SpecialAttack1MidState
    protected override void EnterSpecialAttack1MidState()
    {
        aniControlBoss.IsSpecialAttack1Mid = true;
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        timer = 0;

    }

    protected override void UpdateSpecialAttack1MidState()
    {
        #region Action
        //// Olhar para o player //
        //moveControl.LookAt(playerTarget.position);
        MoveControl.Move(PlayerTarget.position);
        MoveControl.LookAt(PlayerTarget.position);
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        #endregion

        #region Transtions
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            //if (aniControlBoss.IsAnimationFinish(state.ToString()))
            //{
            //TransitionsAttack();
            timer = 0;
            EnterState(TypeStateCharacter.SpecialAttack1End);
            return;
            //}
        }
        #endregion
    }

    protected override void LeaveSpecialAttack1MidState()
    {
        timer = 0;
    }
    #endregion

    #region SpecialAttack1EndState
    protected override void EnterSpecialAttack1EndState()
    {
        aniControlBoss.IsSpecialAttack1End = true;
    }

    protected override void UpdateSpecialAttack1EndState()
    {
        #region Action
        //// Olhar para o player //
        //moveControl.LookAt(playerTarget.position);
        #endregion

        #region Transtions
        if (aniControlBoss.IsAnimationFinish(state.ToString()))
        {
            //TransitionsAttack();
            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void LeaveSpecialAttack1EndState()
    {

    }

    #endregion

    #endregion

    #region FollowState

    protected override void EnterFollowState()
    {
        aniControlBoss.IsLocomotion = true;
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
    }


    protected override void UpdateFollowState()
    {
        #region Action
        MoveControl.Move(PlayerTarget.position);
        MoveControl.LookAt(PlayerTarget.position);
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        #endregion

        #region Transtions

        float pointDistance = Distance(MonitoringPoint.position, transform.position);
        if (pointDistance > RadiusLimitMonitoringPoint)
        {
            // Personagem muito longe do ponto de origem //
            EnterState(TypeStateCharacter.Back);
            return;
        }

        float playerDistance = Distance(PlayerTarget.position, transform.position);
        if (playerDistance > RadiusPlayerDistance)
        {
            // Detectou o jogador //
            EnterState(TypeStateCharacter.Back);
            return;
        }

        if (playerDistance <= RadiusAttack)
        {
            // Detectou o jogador e está em área de ataque //
            //EnterState(TypeStateCharacter.Attack);
            AttacksVerification();
            return;
        }
        #endregion
    }


    protected override void LeaveFollowState()
    {
        MoveControl.Stop();
    }

    #endregion

    #region BackState

    protected override void EnterBackState()
    {
        aniControlBoss.IsLocomotion = true;
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        EnemyStatus.ResetLife();
    }

    protected override void UpdateBackState()
    {

        #region Action
        MoveControl.Move(MonitoringPoint.position);
        MoveControl.LookAt(MonitoringPoint.position);
        aniControlBoss.SpeedPercent = MoveControl.Magnitude / 2;
        #endregion

        #region Transtions
        float pointDistance = Distance(MonitoringPoint.position, transform.position);
        //Debug.Log("pointDistance: " + pointDistance);
        if (pointDistance <= RadiusMonitoringPoint)
        {
            // Chegou a ponto de origem //
            // radiusMonitoringPoint precisa ser igual ao navAgent.StoppingDistance //

            EnterState(TypeStateCharacter.Move);
            return;
        }
        #endregion
    }

    protected override void LeaveBackState()
    {
        //aniControlBoss.Release();
        isPlayerHere = false;
        MoveControl.RotatePosition(MonitoringPoint);
    }

    #endregion

    #region DeadState

    protected override void EnterDeadState()
    {
        aniControlBoss.IsDead = true;
        Desactivated();
        timer = 0;
    }


    protected override void UpdateDeadState()
    {
        #region Action
        //// Dead //
        #endregion

        #region Transtions
        // No transtions //
        if (aniControlBoss.IsAnimationFinish(state.ToString()))
        {
            if (IsAutomaticResurrection)
            {
                timer += Time.deltaTime;
                if (timer >= TimeToResurrection)
                    EnterState(TypeStateCharacter.Rise);
            }
            else
                EnterState(TypeStateCharacter.Dead);
        }
        #endregion
    }

    protected override void LeaveDeadState()
    {
        if (IsDestroy)
        {
            Destroy(gameObject);
            return;
        }

        EnemyStatus.ResetLife();
        timer = 0;
    }

    #endregion

    #region ___DiscartedState

    #region PatrolState

    protected override void EnterPatrolState()
    {
        //AnimationMove();
        //monitoringPoint = listWayPoints[actualWp];
    }


    protected override void UpdatePatrolState()
    {

        #region Action
        //moveControl.Move(listWayPoints[actualWp].position);
        //moveControl.LookAt(listWayPoints[actualWp].position);
        //aniControlBoss.SpeedPercent = moveControl.Magnitude / 2;
        #endregion

        #region Transtions
        //float playerDistance = Distance(playerTarget.position, transform.position);
        //if (playerDistance <= radiusPlayerDistance)
        //{
        //    // Detectou o jogador //
        //    EnterState(TypeStateCharacter.Follow);
        //    return;
        //}

        //float pointDistance = Distance(listWayPoints[actualWp].position, transform.position);
        //if (pointDistance <= radiusMonitoringPoint)
        //{
        //    // Chegou no destino //
        //    EnterState(TypeStateCharacter.Move);
        //    return;
        //}
        #endregion
    }

    protected override void LeavePatrolState()
    {
        //actualWp++;
        //if (actualWp >= listWayPoints.Count)
        //{
        //    actualWp = 0;
        //    if (!isPatrolStart)
        //        isPatrolCompleted = true;
        //}
    }
    #endregion

    #region RiseState

    protected override void EnterRiseState()
    {
        //aniControlBoss.IsRise = true;

        //Activated();
    }

    protected override void UpdateRiseState()
    {
        //#region Action
        //// Rise //
        //#endregion

        //#region Transtions

        //if (aniControlBoss.IsAnimationFinish(state.ToString()))
        //{
        //    EnterState(TypeStateCharacter.Move);
        //    return;
        //}
        //#endregion
    }

    protected override void LeaveRiseState()
    {

    }

    #endregion

    #region FakeDeadState
    protected override void EnterFakeDeadState()
    {
        //aniControlBoss.IsFakeDead = true;
    }

    protected override void UpdateFakeDeadState()
    {
        //#region Action
        //// Fake Dead //
        //#endregion

        //#region Transtions
        //if (aniControlBoss.IsAnimationFinish(state.ToString()))
        //{
        //    float playerDistance = Distance(playerTarget.position, transform.position);
        //    if (playerDistance <= radiusDetectPlayer)
        //    {
        //        EnterState(TypeStateCharacter.Rise);
        //        return;
        //    }

        //    if (isPatrolStart)
        //    {
        //        timer += Time.deltaTime;
        //        if (timer >= timerWait)
        //        {
        //            timer = 0;
        //            EnterState(TypeStateCharacter.Rise);
        //        }
        //    }
        //}
        //#endregion

    }

    protected override void LeaveFakeDeadState()
    {

    }
    #endregion

    #region FallState

    protected override void EnterFallState()
    {
        //aniControlBoss.IsFall = true;
        //Desactivated();
    }

    protected override void UpdateFallState()
    {
        //#region Action
        //// Fall //
        //#endregion

        //#region Transtions
        //if (aniControlBoss.IsAnimationFinish(state.ToString()))
        //{
        //    EnterState(TypeStateCharacter.FakeDead);
        //    return;
        //}
        //#endregion
    }

    protected override void LeaveFallState()
    {
        //actualWp = 0;
        //isPatrolCompleted = false;
    }

    #endregion
    
    #endregion

    #endregion

    #region Util Metholds

    //protected float Distance(Vector3 pointA, Vector3 pointB)
    //{
    //    pointA.y = 0;
    //    pointB.y = 0;
    //    return Vector3.Distance(pointA, pointB);
    //}


    protected override void Activated()
    {
        MoveControl.BodyMiniMap.SetActive(true);
        BoxCol.enabled = false;
    }


    protected override void Desactivated()
    {
        MoveControl.BodyMiniMap.SetActive(false);
        IsPatrolCompleted = false;
        BoxCol.enabled = false;
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
