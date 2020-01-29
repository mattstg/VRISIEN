using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInstructions : StateMachineBehaviour
{
    public string abilityName;
    float timeToShoot;
    Boss1AI boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss1AI>();
        timeToShoot = Random.Range(3, 6);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(abilityName == "Kick")
        {
            animator.transform.LookAt(boss.target);
            if(boss.abilityDone)
                animator.SetTrigger("ChoosingNewAbility");

        }
        else if(abilityName == "Throw")
        {
           animator.transform.LookAt(boss.target);
            if (boss.abilityDone)
                animator.SetTrigger("ChoosingNewAbility");
        }
        else
        {
            animator.transform.LookAt(boss.target);
            timeToShoot -= Time.deltaTime;
            if(timeToShoot <= 0)
            {
                animator.SetTrigger("ChoosingNewAbility");
            }
            boss.Shoots();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.abilityDone = false;
        animator.ResetTrigger(abilityName);        
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
