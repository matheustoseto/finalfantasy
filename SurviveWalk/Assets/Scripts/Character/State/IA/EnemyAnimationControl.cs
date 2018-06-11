using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour {

    private Animator animator = null;

    private Weapon weapon;

    float speedPercent = 0;

    private bool isLocomotion = false;
    private bool isAttack     = false;
    private bool isRise       = false;
    private bool isDead       = false;
    private bool isFall       = false;



    [Header("Smooth:")]
    [SerializeField] [Range(-1, 1)] private float locomotionTime = 0.1f;


    #region Properties
    public Weapon Weapon { get { return weapon; } set { weapon = value; } }

    public float SpeedPercent { get { return speedPercent; } set { speedPercent = value; } }
    public bool IsLocomotion { get { return isLocomotion; } set { isLocomotion = value; } }
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public bool IsRise { get { return isRise; } set { isRise = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    public bool IsFall { get { return isFall; } set { isFall = value; } }

    #endregion


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

    }




    public void Release()
    {
        isLocomotion = false;
        isAttack = false;
        isRise = false;
        isDead = false;
        isFall = false;
    }


    public void ExecuteAnimations()
    {
        animator.SetFloat("Speed", speedPercent, locomotionTime, Time.deltaTime);
        animator.SetBool(TypeStateCharacter.Attack.ToString(), isAttack);
        animator.SetBool(TypeStateCharacter.Rise.ToString(), isRise);
        animator.SetBool(TypeStateCharacter.Dead.ToString(), isDead);
        animator.SetBool(TypeStateCharacter.Fall.ToString(), isFall);
    }




    #region VerifyState
    public bool IsAnimationCurrentName(string animCurrent)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animCurrent);
    }

    public bool IsAnimationCurrentOver()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime
            > animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public float AnimationCurrentTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime
            / animator.GetCurrentAnimatorStateInfo(0).length;
    }
    #endregion


    #region Event
    public void EventAnimation(string nameEvent, int type, bool eventActive)
    {

    }
    #endregion
}
