using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : StateMachineBehaviour
{
    Transform EnemySpawnTrigger;
    bool musicPlay = false;
    Transform player;
    GameObject go;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemySpawnTrigger = GameSetup.gs.EnemySpawnTrigger;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Update collectibla count,time state is on,check for trigger zones
           if(Input.GetKeyDown(KeyCode.P))
           {
                GameSetup.gs.PlayerTriggeredEnemy = true;
           }
        if (EnemyManager.Instance.enemies.Count > 0)
        {
    
            if (!musicPlay)
            {
            
                go = new GameObject();
                SoundManager.Instance.PlayMusic("Bg_Music", go);
                musicPlay = true;

            }
        }
        else if(EnemyManager.Instance.enemies.Count<=0)
        {
           
            if (musicPlay)
            {
          
                SoundManager.Instance.StopMusic( go);
                musicPlay = false;
            }
        }
           

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
