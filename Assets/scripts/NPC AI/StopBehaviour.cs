using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBehaviour : StateMachineBehaviour
{
	GameObject gameObject;

	AI _Ai;
	private float timer;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		gameObject = animator.gameObject;
		_Ai = animator.gameObject.GetComponent<AI>();
		timer = _Ai.GetReloadTime();
		_Ai.ClearNav();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			_Ai.Reload();
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("escaped", false);
	}
}
