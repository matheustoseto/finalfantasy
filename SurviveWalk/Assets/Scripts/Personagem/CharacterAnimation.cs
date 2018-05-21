using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;    
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float speedPercent = controller.velocity.magnitude / 2;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttack");
        }
    }
}
