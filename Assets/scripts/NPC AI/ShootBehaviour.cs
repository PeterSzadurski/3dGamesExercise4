using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : StateMachineBehaviour
{
    GameObject gameObject;

    AI _Ai;

    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameObject = animator.gameObject;
        _Ai = animator.gameObject.GetComponent<AI>();
        timer = _Ai.GetFireTimer();
        animator.SetBool("reloaded", false);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            _Ai.Fire();
            animator.SetInteger("ammo", animator.GetInteger("ammo") - 1);
            timer = _Ai.GetFireTimer();
        }
        if (_Ai.InAttackRange())
        {
            gameObject.transform.LookAt(_Ai.GetTargetObject().transform);
        }
        else
        {
            animator.SetBool("inAttackRange", false);
        }
    }
}
