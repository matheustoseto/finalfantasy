using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterState : MonoBehaviour {

    [Header("State")]
    [SerializeField] protected TypeStateCharacter state = TypeStateCharacter.Move;
    protected TypeStateCharacter prevState = TypeStateCharacter.Move;

    [Header("Tools / Weapon:")]
    [SerializeField] protected Weapon weapon;


    #region Properties
    public TypeStateCharacter State     { get { return state;     } }
    public TypeStateCharacter PrevState { get { return prevState; } }
    public Weapon Weapon { get { return weapon; } }

    #endregion

    void Awake()
    {
        InitAwake();
    }

    protected virtual void InitAwake()
    {
        if (weapon == null)
            weapon = GetComponentInChildren<Weapon>();
    }

    // Use this for initialization
    void Start () {
        InitStart();

    }

    protected virtual void InitStart()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}



    #region EnterState

    protected void EnterState(TypeStateCharacter newState)
    {

        LeaveState();
        prevState = state;
        state = newState;

        switch (state)
        {
            case TypeStateCharacter.Idle    : EnterIdleState();     break;
            case TypeStateCharacter.Move    : EnterMoveState();     break;
            case TypeStateCharacter.Attack  : EnterAttackState();   break;
            case TypeStateCharacter.Dash    : EnterDashState();     break;
            case TypeStateCharacter.Action  : EnterActionState();   break;
            case TypeStateCharacter.Patrol  : EnterPatrolState();   break;
            case TypeStateCharacter.Follow  : EnterFollowState();   break;
            case TypeStateCharacter.Back    : EnterBackState();     break;
            case TypeStateCharacter.Rise    : EnterRiseState();     break;
            case TypeStateCharacter.FakeDead: EnterFakeDeadState(); break;
            case TypeStateCharacter.Fall    : EnterFallState();     break;
            case TypeStateCharacter.Dead    : EnterDeadState();     break;

            case TypeStateCharacter.SpecialAttack1Start : EnterSpecialAttack1StartState(); break;
            case TypeStateCharacter.SpecialAttack1Mid   : EnterSpecialAttack1MidState();   break;
            case TypeStateCharacter.SpecialAttack1End   : EnterSpecialAttack1EndState();   break;
            case TypeStateCharacter.SpecialAttack2      : EnterSpecialAttack2State(); break;
            default: break;
        }

    }

    protected virtual void EnterIdleState()     { }
    protected virtual void EnterMoveState()     { }
    protected virtual void EnterAttackState()   { }
    protected virtual void EnterDashState()     { }
    protected virtual void EnterActionState()   { }
    protected virtual void EnterPatrolState()   { }
    protected virtual void EnterFollowState()   { }
    protected virtual void EnterBackState()     { }
    protected virtual void EnterRiseState()     { }
    protected virtual void EnterFakeDeadState() { }
    protected virtual void EnterFallState()     { }
    protected virtual void EnterDeadState()     { }

    protected virtual void EnterSpecialAttack1StartState() { }
    protected virtual void EnterSpecialAttack1MidState()   { }
    protected virtual void EnterSpecialAttack1EndState()   { }
    protected virtual void EnterSpecialAttack2State()      { }
    #endregion


    #region UpdateState
    protected void UpdateState()
    {
        switch (state)
        {
            case TypeStateCharacter.Idle     : UpdateIdleState();     break;
            case TypeStateCharacter.Move     : UpdateMoveState();     break;
            case TypeStateCharacter.Attack   : UpdateAttackState();   break;
            case TypeStateCharacter.Dash     : UpdateDashState();     break;
            case TypeStateCharacter.Action   : UpdateActionState();   break;
            case TypeStateCharacter.Patrol   : UpdatePatrolState();   break;
            case TypeStateCharacter.Follow   : UpdateFollowState();   break;
            case TypeStateCharacter.Back     : UpdateBackState();     break;
            case TypeStateCharacter.Rise     : UpdateRiseState();     break;
            case TypeStateCharacter.FakeDead : UpdateFakeDeadState(); break;
            case TypeStateCharacter.Fall     : UpdateFallState();     break;
            case TypeStateCharacter.Dead     : UpdateDeadState();     break;

            case TypeStateCharacter.SpecialAttack1Start : UpdateSpecialAttack1StartState(); break;
            case TypeStateCharacter.SpecialAttack1Mid   : UpdateSpecialAttack1MidState();   break;
            case TypeStateCharacter.SpecialAttack1End   : UpdateSpecialAttack1EndState();   break;
            case TypeStateCharacter.SpecialAttack2      : UpdateSpecialAttack2State();      break;
            default: break;
        }
    }

    protected virtual void UpdateIdleState()     { }
    protected virtual void UpdateMoveState()     { }
    protected virtual void UpdateAttackState()   { }
    protected virtual void UpdateDashState()     { }
    protected virtual void UpdateActionState()   { }
    protected virtual void UpdatePatrolState()   { }
    protected virtual void UpdateFollowState()   { }
    protected virtual void UpdateBackState()     { }
    protected virtual void UpdateRiseState()     { }
    protected virtual void UpdateFakeDeadState() { }
    protected virtual void UpdateFallState()     { }
    protected virtual void UpdateDeadState()     { }

    protected virtual void UpdateSpecialAttack1StartState() { }
    protected virtual void UpdateSpecialAttack1MidState()   { }
    protected virtual void UpdateSpecialAttack1EndState()   { }
    protected virtual void UpdateSpecialAttack2State()      { }
    #endregion


    #region LeaveState
    protected virtual void LeaveState()
    {
        switch (state)
        {
            case TypeStateCharacter.Idle    : LeaveIdleState();     break;
            case TypeStateCharacter.Move    : LeaveMoveState();     break;
            case TypeStateCharacter.Attack  : LeaveAttackState();   break;
            case TypeStateCharacter.Dash    : LeaveDashState();     break;
            case TypeStateCharacter.Action  : LeaveActionState();   break;
            case TypeStateCharacter.Patrol  : LeavePatrolState();   break;
            case TypeStateCharacter.Follow  : LeaveFollowState();   break;
            case TypeStateCharacter.Back    : LeaveBackState();     break;
            case TypeStateCharacter.Rise    : LeaveRiseState();     break;
            case TypeStateCharacter.FakeDead: LeaveFakeDeadState(); break;
            case TypeStateCharacter.Fall    : LeaveFallState();     break;
            case TypeStateCharacter.Dead    : LeaveDeadState();     break;

            case TypeStateCharacter.SpecialAttack1Start : LeaveSpecialAttack1StartState(); break;
            case TypeStateCharacter.SpecialAttack1Mid   : LeaveSpecialAttack1MidState();   break;
            case TypeStateCharacter.SpecialAttack1End   : LeaveSpecialAttack1EndState();   break;
            case TypeStateCharacter.SpecialAttack2      : LeaveSpecialAttack2State();      break;
            default: break;
        }
    }

    protected virtual void LeaveIdleState()     { }
    protected virtual void LeaveMoveState()     { }
    protected virtual void LeaveAttackState()   { }
    protected virtual void LeaveDashState()     { }
    protected virtual void LeaveActionState()   { }
    protected virtual void LeavePatrolState()   { }
    protected virtual void LeaveFollowState()   { }
    protected virtual void LeaveBackState()     { }
    protected virtual void LeaveRiseState()     { }
    protected virtual void LeaveFakeDeadState() { }
    protected virtual void LeaveFallState()     { }
    protected virtual void LeaveDeadState()     { }

    protected virtual void LeaveSpecialAttack1StartState() { }
    protected virtual void LeaveSpecialAttack1MidState()   { }
    protected virtual void LeaveSpecialAttack1EndState()   { }
    protected virtual void LeaveSpecialAttack2State()      { }

    #endregion


    #region Events
    public virtual void EventDead()
    {
        EnterState(TypeStateCharacter.Dead);
    }

    public virtual void EventPatrol()
    {
        EnterState(TypeStateCharacter.Patrol);
    }

    public virtual void EventBack()
    {

    }

    #endregion

    #region Verify
    protected bool AlertStateActivated()
    {
        return state == TypeStateCharacter.Attack
            || state == TypeStateCharacter.SpecialAttack1Start
            || state == TypeStateCharacter.SpecialAttack1Mid
            || state == TypeStateCharacter.SpecialAttack2
            || state == TypeStateCharacter.Follow
            || state == TypeStateCharacter.Move;
    }
    #endregion

}
