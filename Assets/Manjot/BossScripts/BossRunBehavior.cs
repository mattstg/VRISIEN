using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRunBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform target;
    Boss1AI bossAI;
    public string abilityName;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FakePlayer>().transform;
        bossAI = animator.GetComponent<Boss1AI>();
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (bossAI.randAbility)
        {
            case 0://Kick
                //animator.SetTrigger("Kick");
                bossAI.RunAndKick();
                break;
            case 1://ShootLeft
                animator.SetTrigger("StrafeLeft");
                break;
            case 2://ShootRight
                animator.SetTrigger("StrafeRight");
                break;
            case 3://SpecialAttack
                //animator.SetTrigger("Throw");
                bossAI.SpecialAttack();
                break;
            default:
                Debug.Log("Default");
                break;
        }

    }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
