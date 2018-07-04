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
            default: break;
        }

    }

    protected virtual void EnterMoveState() { }
    protected virtual void EnterAttackState() { }
    protected virtual void EnterDashState() { }
    protected virtual void EnterActionState() { }
    protected virtual void EnterPatrolState() { }
    protected virtual void EnterFollowState() { }
    protected virtual void EnterBackState() { }
    protected virtual void EnterRiseState() { }
    protected virtual void EnterFakeDeadState() { }
    protected virtual void EnterFallState() { }
    protected virtual void EnterDeadState() { }
    #endregion


    #region UpdateState
    protected void UpdateState()
    {
        switch (state)
        {
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
            default: break;
        }
    }

    protected virtual void UpdateMoveState() { }
    protected virtual void UpdateAttackState() { }
    protected virtual void UpdateDashState() { }
    protected virtual void UpdateActionState() { }
    protected virtual void UpdatePatrolState() { }
    protected virtual void UpdateFollowState() { }
    protected virtual void UpdateBackState() { }
    protected virtual void UpdateRiseState() { }
    protected virtual void UpdateFakeDeadState() { }
    protected virtual void UpdateFallState() { }
    protected virtual void UpdateDeadState() { }
    #endregion


    #region LeaveState
    protected virtual void LeaveState()
    {
        switch (state)
        {
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
            default: break;
        }
    }

    protected virtual void LeaveMoveState() { }
    protected virtual void LeaveAttackState() { }
    protected virtual void LeaveDashState() { }
    protected virtual void LeaveActionState() { }
    protected virtual void LeavePatrolState() { }
    protected virtual void LeaveFollowState() { }
    protected virtual void LeaveBackState() { }
    protected virtual void LeaveRiseState() { }
    protected virtual void LeaveFakeDeadState() { }
    protected virtual void LeaveFallState() { }
    protected virtual void LeaveDeadState() { }
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
            || state == TypeStateCharacter.Follow
            || state == TypeStateCharacter.Move;
    }
    #endregion

}
