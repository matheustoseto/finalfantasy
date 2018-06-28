using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateStateControl : DeviceState {

    #region Attributes - Components
    private DeviceAnimationControl aniControl = null;
    #endregion

    #region Attributes - General
    [Header("General Settings:")]
    [SerializeField] private bool isNeedItem = false;
    [SerializeField] private int idItem = -1;
    #endregion


    void Awake()
    {
        aniControl = GetComponent<DeviceAnimationControl>();

    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        UpdateState();
        aniControl.ExecuteAnimations();
    }


    #region StateMachine

    protected override void LeaveState()
    {
        aniControl.Release();
        base.LeaveState();
    }


    #region IdleState
    protected override void EnterIdleState()
    {
        
    }

    protected override void UpdateIdleState()
    {

    }

    protected override void LeaveIdleState()
    {

    }

    #endregion

    #region OpenState
    protected override void EnterOpenState()
    {
        aniControl.IsOpen = true;
    }

    protected override void UpdateOpenState()
    {
        if (aniControl.IsAnimationFinish(state.ToString()))
        {
            EnterState(TypeStateDevice.Idle);
        }
    }

    protected override void LeaveOpenState()
    {

    }
    #endregion

    #region CloseState
    protected override void EnterCloseState()
    {
        aniControl.IsClose = true;
    }

    protected override void UpdateCloseState()
    {

    }

    protected override void LeaveCloseState()
    {

    }
    #endregion


    public void EventDevice(TypeStateDevice newState)
    {
        switch (newState)
        {
            case TypeStateDevice.Open : EnterState(TypeStateDevice.Open);  break;
            case TypeStateDevice.Close: EnterState(TypeStateDevice.Close); break;
            default: break;
        }
    }

    public void EventOpenGate(int otherIdItem, TypeStateDevice newState)
    {
        if (isNeedItem)
        {
            if(otherIdItem == idItem)
            {
                EventDevice(newState);
            }
        }
        else
        {
            EventDevice(newState);
        }
    }

    #endregion
}
