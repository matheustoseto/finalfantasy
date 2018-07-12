using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationControl : CharacterAnimationControl
{

    private bool isSpecialAttack1Start = false;
    private bool isSpecialAttack1Mid   = false;
    private bool isSpecialAttack1End   = false;
    private bool isSpecialAttack2      = false;



    #region Properties
    public bool IsSpecialAttack1Start { get { return isSpecialAttack1Start; } set { isSpecialAttack1Start = value; } }
    public bool IsSpecialAttack1Mid   { get { return isSpecialAttack1Mid; } set { isSpecialAttack1Mid = value; } }
    public bool IsSpecialAttack1End   { get { return isSpecialAttack1End; } set { isSpecialAttack1End = value; } }
    public bool IsSpecialAttack2      { get { return isSpecialAttack2; } set { isSpecialAttack2 = value; } }
    #endregion




    public override void Release()
    {
        base.Release();
        IsLocomotion          = false;
        IsAttack              = false;
        IsSpecialAttack1Start = false;
        IsSpecialAttack1Mid   = false;
        IsSpecialAttack1End   = false;
        IsSpecialAttack2      = false;
        IsDead                = false;
    }


    public override void ExecuteAnimations()
    {
        animator.SetFloat("Speed", SpeedPercent, LocomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(),              IsAttack);
        animator.SetBool(TypeStateCharacter.SpecialAttack1Start.ToString(), IsSpecialAttack1Start);
        animator.SetBool(TypeStateCharacter.SpecialAttack1Mid.ToString(),   IsSpecialAttack1Mid);
        animator.SetBool(TypeStateCharacter.SpecialAttack1End.ToString(),   IsSpecialAttack1End);
        animator.SetBool(TypeStateCharacter.SpecialAttack2.ToString(),      IsSpecialAttack2);
        animator.SetBool(TypeStateCharacter.Dead.ToString(),                IsDead);
    }

    #region Event

    public override void AttackOn()
    {
        Weapon.AttackOn();
        Weapon.TrailRenderActivated(true);
    }

    public override void AttackOff()
    {
        Weapon.AttackOff();
        Weapon.TrailRenderActivated(false);
    }

    public override void SoundAttack()
    {
        //SoundControl.GetInstance().ExecuteEffect(TypeSound.EnemyAttack);
    }

    public void SpecialAttackOn()
    {
        Weapon.AttackOn();
        Weapon.TrailRenderActivated(true);
    }

    public void SpecialAttackOff()
    {
        Weapon.AttackOff();
        Weapon.TrailRenderActivated(false);
    }

    public void SpecialAttack2On()
    {
        Weapon.SpecialAttackOn_2();
        Weapon.TrailRenderActivated(true);
    }

    public void SpecialAttack2Off()
    {
        Weapon.SpecialAttackOff_2();
        Weapon.TrailRenderActivated(false);
    }

    public void TrailOn()
    {
        //Weapon.TrailRenderActivated(true);
    }

    public void TrailOff()
    {
        //Weapon.TrailRenderActivated(false);
    }

    #endregion
}
