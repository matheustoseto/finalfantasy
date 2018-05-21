using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour {

    Animator animator;
    NavMeshAgent agent;
    private bool playerEnter = false;
    private bool animrised = false;
    public bool realrised = false;
    const float locomotionAnimationSmoothTime = .1f;
    public float timeRising = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerEnter && !animrised)
        {
            animrised = true;
            animator.SetTrigger("isRise");
        }

        if (animrised)
        {
            timeRising -= Time.deltaTime;

            if (timeRising <= 0)
            {
                realrised = true;
            }
        }

        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ("Player".Equals(other.tag))
        {
            playerEnter = true;
        }
    }
}
