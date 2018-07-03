using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : CharacterAnimationControl {

    private bool isRise       = false;
    private bool isFakeDead   = false;
    private bool isFall       = false;



    #region Properties
    public bool IsRise { get { return isRise; } set { isRise = value; } }
    public bool IsFakeDead { get { return isFakeDead; } set { isFakeDead = value; } }
    public bool IsFall { get { return isFall; } set { isFall = value; } }
    #endregion




    public override void Release()
    {
        base.Release();
        IsLocomotion = false;
        IsAttack     = false;
        IsRise       = false;
        IsFakeDead   = false;
        IsFall       = false;
        IsDead       = false;
    }


    public override void ExecuteAnimations()
    {
        animator.SetFloat("Speed", SpeedPercent, LocomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(),   IsAttack);
        animator.SetBool(TypeStateCharacter.Rise.ToString(),     IsRise);
        animator.SetBool(TypeStateCharacter.FakeDead.ToString(), IsFakeDead);
        animator.SetBool(TypeStateCharacter.Fall.ToString(),     IsFall);
        animator.SetBool(TypeStateCharacter.Dead.ToString(),     IsDead);
    }

    #region Event
    public void EventAnimation(string nameEvent, int type, bool eventActive)
    {

    }

    public override void AttackOn()
    {
        Weapon.AttackOn();
    }

    public override void AttackOff()
    {
        
    }

    public override void SoundAttack()
    {
        //SoundControl.GetInstance().ExecuteEffect(TypeSound.EnemyAttack);
    }

    #endregion
}
