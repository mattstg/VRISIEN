using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Each GoapAgent has a reference to one GoapStateMachine. It is used to manage whether the agent is
//planning, moving, or performing an action. It is the highest level of control for each agent. It gives 
//control to the GoapPlanner during planning (idle) state, it gives control to the movement system 
//(navmesh or whatever) during move state, and it gives control to the logic of the action itself while
//an action is being perform (acting state).

//In reality, this class simply tracks which of the three states our agent is in, and then invokes the
//appropriate delegate every update using the switch case below. 
public class GoapStateMachine {
    public delegate void StateRefresh(float dt); //defining the delegate type we will use for each state
    public enum AgentState { Idle, Move, Acting} //the 3 possible states

    public StateRefresh idleRefresh; //idle delegate. the GoapAgent will register its idle refresh function to this delegate
    public StateRefresh moveRefresh; //same as above
    public StateRefresh actingRefresh; //same as above

    public AgentState currentState = AgentState.Idle; //this is updated by the GoapAgent (ie: transitions
    // are managed by the agent, not here)
    

    public void Refresh(float dt)
    {
        switch (currentState) //just choosing which delegate to invoke each update
        {
            case AgentState.Idle:
                if (idleRefresh != null)
                    idleRefresh.Invoke(dt);
                break;
            case AgentState.Move:
                if (moveRefresh != null)
                    moveRefresh.Invoke(dt);
                break;
            case AgentState.Acting:
                if (actingRefresh != null)
                    actingRefresh.Invoke(dt);
                break;
            default:
                Debug.LogError("Should be unreachable code");
                break;
        }
           
    }

    
}
