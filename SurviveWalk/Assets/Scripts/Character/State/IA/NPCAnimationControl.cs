using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationControl : CharacterAnimationControl {

    // Use this for initialization

    #region Properties
        // Parâmetros //
    #endregion


    public override void Release()
    {
        base.Release();
        IsLocomotion = false;
        IsAttack = false;
    }


    public override void ExecuteAnimations()
    {
        animator.SetFloat("Speed", SpeedPercent, LocomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(), IsAttack);
    }
}
