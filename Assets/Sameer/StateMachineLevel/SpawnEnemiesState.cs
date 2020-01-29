using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesState : StateMachineBehaviour
{
    // Start is called before the first frame update
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyManager.Instance.NumberOfEnemyToSpawn(GameSetup.gs.CountOfMeleeEnemyAtOneSpawnLocation, GameSetup.gs.CountOfRangedEnemyAtOneSpawnLocation, 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //update setup enemy count,collectivle count
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
