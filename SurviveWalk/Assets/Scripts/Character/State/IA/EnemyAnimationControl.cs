using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : CharacterAnimationControl {

    //private bool isLocomotion = false;
    //private bool isAttack     = false;
    private bool isRise       = false;
    private bool isFakeDead   = false;
    private bool isFall       = false;
    private bool isDead = false;


    #region Properties
    //public Weapon Weapon { get { return weapon; } set { weapon = value; } }

    //public float SpeedPercent { get { return speedPercent; } set { speedPercent = value; } }
    //public bool IsLocomotion { get { return isLocomotion; } set { isLocomotion = value; } }
    //public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public bool IsRise { get { return isRise; } set { isRise = value; } }
    public bool IsFakeDead { get { return isFakeDead; } set { isFakeDead = value; } }
    public bool IsFall { get { return isFall; } set { isFall = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    #endregion




    public override void Release()
    {
        base.Release();
        IsLocomotion = false;
        IsAttack = false;
        IsRise = false;
        IsFakeDead = false;
        IsFall = false;
        IsDead = false;
    }


    public override void ExecuteAnimations()
    {
        animator.SetFloat("Speed", SpeedPercent, LocomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(), IsAttack);
        animator.SetBool(TypeStateCharacter.Rise.ToString(), IsRise);
        animator.SetBool(TypeStateCharacter.FakeDead.ToString(), IsFakeDead);
        animator.SetBool(TypeStateCharacter.Fall.ToString(), IsFall);
        animator.SetBool(TypeStateCharacter.Dead.ToString(), IsDead);
    }


}
