using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimationControl : AnimationControl {


    private Weapon weapon;

    float speedPercent = 0;

    private bool isLocomotion = false;
    private bool isAttack     = false;
    private bool isDash       = false;
    private bool isAction     = false;
    private bool isDead = false;


    [Header("Smooth:")]
    [SerializeField] [Range(-1, 1)] private float locomotionTime = 0.1f;


    #region Properties
    public float LocomotionTime { get { return locomotionTime; } }
    public Weapon Weapon { get { return weapon; } set { weapon = value; } }

    public float SpeedPercent { get { return speedPercent; } set { speedPercent = value; } }
    public bool IsLocomotion  { get { return isLocomotion; } set { isLocomotion = value; } }
    public bool IsAttack      { get { return isAttack;     } set { isAttack = value;     } }
    public bool IsDash        { get { return isDash;       } set { isDash = value;       } }
    public bool IsAction      { get { return isAction;     } set { isAction = value;     } }
    public bool IsDead        { get { return isDead;       } set { isDead = value;       } }

    #endregion



    public override void Release()
    {
        isLocomotion = false;
        isAttack     = false;
        isDash       = false;
        isAction     = false;
        isDead       = false;
    }


    public override void ExecuteAnimations()
    {
        animator.SetFloat("Speed", speedPercent, locomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(), isAttack);
        animator.SetBool(TypeStateCharacter.Dash.ToString()  , isDash  );
        animator.SetBool(TypeStateCharacter.Action.ToString(), isAction);
        animator.SetBool(TypeStateCharacter.Dead.ToString(),   isDead);
    }




       #region Event
    public void EventAnimation(string nameEvent, int type, bool eventActive)
    {

    }

    public virtual void AttackOn()
    {
        if (weapon != null)
            weapon.AttackOn();
    }

    public virtual void AttackOff()
    {
        if (weapon != null)
            weapon.AttackOff();
    }

    public virtual void SoundAttack()
    {
        SoundControl.GetInstance().ExecuteEffect(TypeSound.PlayerAttack);
    }

    #endregion
}
