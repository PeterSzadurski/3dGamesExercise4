using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    GameObject _GameObj;
    AI _Ai;
    float _navTimer = 0;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _GameObj = animator.gameObject;
        _Ai = _GameObj.GetComponent<AI>();
        _navTimer = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_navTimer > 10)
        {
            _navTimer = 0;
            _Ai.Chase();
        }
        _navTimer += Time.deltaTime;

        if (_Ai.InAttackRange())
            animator.SetBool("isInRange", true);

        if (!_Ai.IsTargetObjectVisable())
        {
            _Ai.GetNextPatrolTarget();
            animator.SetBool("isObjectTargetVisible", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Ai.ClearNav();

    }
}