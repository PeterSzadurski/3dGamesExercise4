using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNextTarget : StateMachineBehaviour
{
    AI _Ai;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Ai = animator.GetComponent<AI>();
        _Ai.GetNextPatrolTarget();
    }
}
