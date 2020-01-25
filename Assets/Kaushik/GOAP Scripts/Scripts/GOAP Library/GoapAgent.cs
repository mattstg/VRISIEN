using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapAgent : MonoBehaviour {

    GoapStateMachine stateMachine;
    GoapPlanner planner;

    protected List<GoapAction> availableActions; //this list is built by grabbing all the components on the agent gameObject that inherit from GoapAction 
                                                 //It represents all the actions the agent's planner is allowed to consider
    public Queue<GoapAction> currentActions; //if the agent has a plan, this queue holds all the actions in that plan (that remain to be performed)
                                      //when the agent has no plan, this queue is simply empty

    public IGoap nPC;//this is the link to whatever class your project uses for agents. Since this reference
                     //is of type IGoap (rather than the actual class type you are using), this class (and the library in general)
                     //doesn't need to know about the class you use. We simply know it must implement IGoap, so we hold 
                     //a reference to it AS an IGoap so that we can call the interface methods. This reference is thus the link
                     //that connects the library side of things with the implementation side of things.

    public void Start()
    {
        SetupStateDelegates(); //this instantiates our agent's state machine, and registers our methods to the appropriate delegates
        availableActions = new List<GoapAction>();
        currentActions = new Queue<GoapAction>();
        planner = new GoapPlanner(); //instantiating the planner
        LoadActions(); //filling our availableActions list by using GetComponents<GoapAction>()
        FindIGoap(); //using some tricky code to figure out which component on our gameObject is the
                     //one that implements IGoap, so that we can store our reference to it
    }

    public void Update()
    {
        stateMachine.Refresh(Time.deltaTime); //each agent passes control to its state machine by calling its update here.
                                              //The state machine in turn will invoke its proper refresh delegates depending on the current state,
                                              //which will end up calling the methods this class registered to those delegates (IdleRefresh, MoveRefresh, ActingRefresh)
    }

    void SetupStateDelegates()
    {
        //instatiating the state machine and registering our methods to its delegates
        stateMachine = new GoapStateMachine();
        stateMachine.idleRefresh += IdleRefresh;
        stateMachine.moveRefresh += MoveRefresh;
        stateMachine.actingRefresh += ActingRefresh;
    }

    void IdleRefresh(float dt)
    {
        Dictionary<string, object> worldState = nPC.GetWorldState(); //agent's current world state; not to be confused with the state machine states
        Dictionary<string, object> goalState = nPC.CreateGoalState();//agen't goal state. we are trying to get from the state above to this state

        //asking the planner for a plan to get this agent from worldState to goalState given their available actions
        Queue<GoapAction> plan = planner.CalculatePlan(this, availableActions, worldState, goalState);

        //if we find a plan, we fill our currentActions queue with that plan, then switch to
        //the Acting state of the state machine (though this will quickly be switched to the moving
        //state if we find we aren't currently in range)
        if(plan != null)
        {
            currentActions = plan;
            stateMachine.currentState = GoapStateMachine.AgentState.Acting;
        }
    }

    void MoveRefresh(float dt)
    {
        //grabbing our current action in the plan
        GoapAction action = currentActions.Peek();

        if (action.requiresInRange && action.target == null) //just a safety check
        {
            //Debug.LogError("Error: target required but not set");
            stateMachine.currentState = GoapStateMachine.AgentState.Idle;
            return;
        }

        //this gets called each update, which calls your IGoap's MoveAgent, and it returns false until the agent has made it to the action's target
        if (nPC.MoveAgent(action)) 
            stateMachine.currentState = GoapStateMachine.AgentState.Acting; //once we've made it, change the state machine's state to acting

    }

    void ActingRefresh(float dt)
    {
        if(!HasActionPlan()) //if we're in acting state but have no plan, move to idle state
        {
            stateMachine.currentState = GoapStateMachine.AgentState.Idle;
            return;
        }

        GoapAction action = currentActions.Peek(); //grabbing our current action
        if (action.IsDone())
        {
            currentActions.Dequeue(); //if the action is completed, remove it from our plan
            nPC.ActionFinished(action); //this is a callback to let the IGoap know the action is done,
                                        //in case it wants to apply custom logic. This is an example of our library
                                        //communicating with our implementation
        }
            

        if(HasActionPlan())
        {
            action = currentActions.Peek();
            bool inRange = action.requiresInRange ? action.InRange : true;
            if (inRange)
            {
                //if we're in here, then we are within range of the next action in our plan, and should perform it
                bool success = action.Perform(this); //calling the actions custom Perform() code
                nPC.PerformingAction(action); //letting our IGoap know that we are currently performing an action
                if(!success) //if the action somehow fails, just fall back to idle state
                    stateMachine.currentState = GoapStateMachine.AgentState.Idle;
            }
            else //if we have a plan, but our next action is out of range, go to move state instead
                stateMachine.currentState = GoapStateMachine.AgentState.Move;
        }
        else
            stateMachine.currentState = GoapStateMachine.AgentState.Idle;


    }

    void LoadActions()
    {
        foreach (GoapAction a in gameObject.GetComponents<GoapAction>())
          availableActions.Add(a);
    }

    private bool HasActionPlan()
    {
        return currentActions.Count > 0;
    }

    private void FindIGoap()
    {
        foreach (Component comp in gameObject.GetComponents(typeof(Component)))
        {
            if (typeof(IGoap).IsAssignableFrom(comp.GetType())) //tricky line of code; see if you can figure it out by looking up
                                                                // typeof(), Type.IsAssignableFrom(), and Object.GetType() 
            {
                nPC = (IGoap)comp;
                return;
            }
        }
    }

}
