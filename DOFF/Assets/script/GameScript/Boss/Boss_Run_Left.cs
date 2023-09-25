using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run_Left : StateMachineBehaviour
{
    public float speed = 2.5f;
    private float range;
	public float attackRange = 200f;

	Transform player;
	Rigidbody rb;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody>();
        
		
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        range = Vector2.Distance(rb.position, player.position);
		if (range <= attackRange)
		{
			animator.SetTrigger("attackLeft");
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("attackLeft");
	}
}
