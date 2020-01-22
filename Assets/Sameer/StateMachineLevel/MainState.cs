using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : StateMachineBehaviour
{
    Transform EnemySpawnTrigger;
    Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemySpawnTrigger = GameSetupClass.Instance.EnemySpawnTrigger;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Update collectibla count,time state is on,check for trigger zones
           if(Input.GetKeyDown(KeyCode.P))
            {
                GameSetupClass.Instance.PlayerTriggeredEnemy = true;
            }
        
           

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
