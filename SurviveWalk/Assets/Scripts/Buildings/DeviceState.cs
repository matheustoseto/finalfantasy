using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceState : MonoBehaviour {

    [Header("State")]
    [SerializeField] protected TypeStateDevice state = TypeStateDevice.Idle;
    protected TypeStateDevice prevState = TypeStateDevice.Idle;

    #region Properties
    public TypeStateDevice State { get { return state; } }
    public TypeStateDevice PrevState { get { return prevState; } }

    #endregion

    void Awake()
    {
        InitAwake();
    }

    protected virtual void InitAwake()
    {
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region StateMachine

    protected void EnterState(TypeStateDevice newState)
    {

        LeaveState();
        prevState = state;
        state = newState;

        switch (state)
        {
            case TypeStateDevice.Idle: EnterIdleState(); break;
            case TypeStateDevice.Open: EnterOpenState(); break;
            case TypeStateDevice.Close: EnterCloseState(); break;
            default: break;
        }

    }

    protected void UpdateState()
    {
        switch (state)
        {
            case TypeStateDevice.Idle: UpdateIdleState(); break;
            case TypeStateDevice.Open: UpdateOpenState(); break;
            case TypeStateDevice.Close: UpdateCloseState(); break;
            default: break;
        }
    }

    protected virtual void LeaveState()
    {
        switch (state)
        {
            case TypeStateDevice.Idle: LeaveIdleState(); break;
            case TypeStateDevice.Open: LeaveOpenState(); break;
            case TypeStateDevice.Close: LeaveCloseState(); break;
            default: break;
        }
    }

    #region StateIdle

    protected virtual void EnterIdleState() { }

    protected virtual void UpdateIdleState() { }

    protected virtual void LeaveIdleState() { }
    #endregion

    #region StateOpen

    protected virtual void EnterOpenState() { }

    protected virtual void UpdateOpenState() { }

    protected virtual void LeaveOpenState() { }
    #endregion

    #region StateClose

    protected virtual void EnterCloseState() { }

    protected virtual void UpdateCloseState() { }

    protected virtual void LeaveCloseState() { }
    #endregion

    #endregion

    #region Events
    // Eventos que trocam diretamente o estado //

    #endregion

    #region Verify
    // Verificação de estados //
    #endregion
}
