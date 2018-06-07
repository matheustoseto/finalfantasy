using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationControl : MonoBehaviour {

    //const float locomotionAnimationSmoothTime = .1f;

    private Animator animator = null;
    private CharacterController charController = null;

    [Header("Smooth:")]
    [SerializeField] [Range(-1, 1)] private float locomotionTime = 0.1f;


    private bool isAttack = false;

    public bool IsAttack { get { return isAttack; } }


    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    public Animator Animator { get { return animator; } }


    protected bool IsAnimationCurrentName(string animCurrent)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animCurrent);
    }

    public bool IsPlayingCurrentAnimatorState()
    {
        Debug.Log("length(" + animator.GetCurrentAnimatorStateInfo(0).length + ") > normalizedTime(" + animator.GetCurrentAnimatorStateInfo(0).normalizedTime + ") = "
                           + (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime));

        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public bool IsAnimationCurrentName(TypeStateCharacter state)
    {
        bool animationActive = false;
        switch (state)
        {
            case TypeStateCharacter.Move:
                animationActive = IsAnimationCurrentName("Locomotion");
                break;
            case TypeStateCharacter.Attack:
                animationActive = IsAnimationCurrentName("Attack");
                break;
            case TypeStateCharacter.Dash:
                animationActive = IsAnimationCurrentName("Dash");
                break;
            case TypeStateCharacter.Action:
                animationActive = IsAnimationCurrentName("Action");
                break;
            default:
                break;
        }
        return animationActive;
    }



    #region Animations Activated
    public void Locomotion()
    {

        animator.SetBool("isAttack", false);

        float speedPercent = charController.velocity.magnitude / 2;
        //animator.SetFloat("speedPercent", speedPercent, locomotionTime, Time.deltaTime);
        animator.SetFloat("Speed", speedPercent, locomotionTime, Time.deltaTime);
        
        //Debug.Log("[ANIMATION][LOCOMOTION]: Activate");
    }


    public void Attack()
    {
        float speedPercent = charController.velocity.magnitude / 2;
        //animator.SetFloat("speedPercent", 0, locomotionTime, Time.deltaTime);
        animator.SetFloat("Speed", 0);

        //animator.SetBool("isAttack", true);
        animator.SetBool("Attack", true);

        //isAttack = true;
        //Debug.Log("[ANIMATION][ATTACK]: Activate");
    }

    public bool Dash()
    {
        bool isDash = false;
        //isDash = IsAnimationCurrentName("isDash");

        //if (!isDash)
        //{
        //    animator.SetTrigger("isDash");
        //    isDash = true;
            Debug.Log("[ANIMATION][DASH]: Activate");
        //}
        return isDash;
    }

    public bool Action()
    {
        bool isAction = false;
        //isAction = IsAnimationCurrentName("isAction");

        //if (!isAction)
        //{
        //    animator.SetTrigger("isAction");
        //    isAction = true;
            Debug.Log("[ANIMATION][ACTION]: Activate");
        //}
        return isAction;
    }


    #endregion

    //public void AttackFinish()
    //{
    //    //animator.SetBool("isAttack", false);
    //    animator.SetBool("Attack", false);
    //    isAttack = false;
    //}

}
