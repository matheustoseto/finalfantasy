using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceAnimationControl : AnimationControl
{
    private bool isIdle = false;
    private bool isOpen = false;
    private bool isClose = false;


    #region Properties
    public bool IsIdle  { get { return isIdle; }  set { isIdle = value; } }
    public bool IsOpen  { get { return isOpen; }  set { isOpen = value; } }
    public bool IsClose { get { return isClose; } set { isClose = value; } }
    #endregion

    public override void Release()
    {
        IsIdle  = false;
        IsOpen  = false;
        IsClose = false;
    }

    public override void ExecuteAnimations() {
        animator.SetBool(TypeStateDevice.Idle.ToString(),  IsIdle );
        animator.SetBool(TypeStateDevice.Open.ToString(),  IsOpen );
        animator.SetBool(TypeStateDevice.Close.ToString(), IsClose);
    }
}
