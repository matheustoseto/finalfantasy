using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationControl : MonoBehaviour {

    protected Animator animator = null;

    TypeStateCharacter stateTest = TypeStateCharacter.Move;

    #region Properties
    public TypeStateCharacter StateTest { set { stateTest = value; } }
    #endregion


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();

    }


    public abstract void Release();

    public abstract void ExecuteAnimations();



    #region VerifyState
    public bool IsAnimationCurrentName(string animCurrent)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animCurrent);
    }

    public bool IsAnimationCurrentOver()
    {
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float length = animator.GetCurrentAnimatorStateInfo(0).length;

        return normalizedTime >= 1 || normalizedTime >= length;
    }

    public bool IsAnimationFinish(string animCurrent)
    {
        bool isName = animator.GetCurrentAnimatorStateInfo(0).IsName(animCurrent);
        float normalizedTime  = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float length = animator.GetCurrentAnimatorStateInfo(0).length;

        return isName && (normalizedTime >= 1|| normalizedTime >= length);
    }

    public float AnimationCurrentTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    #endregion
}
