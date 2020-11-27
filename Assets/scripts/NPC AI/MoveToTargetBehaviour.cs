using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetBehaviour : StateMachineBehaviour
{
    GameObject _GameObj;
    AI _Ai;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _GameObj = animator.gameObject;
        _Ai = animator.gameObject.GetComponent<AI>();

        _Ai.OnTarget = false;
        _Ai.MoveToTarget();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //check Patrol Target
        if (Vector3.Distance(_GameObj.transform.position, _Ai._PatrolTarget.position) <= 0.1f) animator.SetBool("onTarget", true);

        //check target object visibility
        if (_Ai.IsTargetObjectVisable()) animator.SetBool("isObjectTargetVisible", true);
    }
}
