using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilityChoser : StateMachineBehaviour
{
    Boss1AI boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss1AI>();
        boss.randAbility = Random.Range(0, 4);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

        switch (boss.randAbility)
        {
            case 0:
                animator.SetTrigger("Run");
                break;
            case 1:
                animator.SetTrigger("StrafeLeft");
                break;
            case 2:
                animator.SetTrigger("StrafeRight");
                break;
            case 3:
                animator.SetTrigger("Run");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("ChoosingNewAbility");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
