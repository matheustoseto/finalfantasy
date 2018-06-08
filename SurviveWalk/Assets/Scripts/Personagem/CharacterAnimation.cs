using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour {

    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;    
    CharacterController controller;

	private float timer = 0;

	public Weapon weapon;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
		if(timer >= 0)
			timer-= Time.deltaTime;

        float speedPercent = controller.velocity.magnitude / 2;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && timer <= 0)
        {
            animator.SetTrigger("isAttack");
			timer = 0.85f;
        }
    }

	public void AttackOn(){
		weapon.Attack();
	}

	public void AttackOff(){
		
	}
}
