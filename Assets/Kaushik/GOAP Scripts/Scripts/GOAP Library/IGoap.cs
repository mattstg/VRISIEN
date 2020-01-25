using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This interface must be implemented by whatever monobehaviour you use for your agents (ex: some kind of NPC class)
//Note that the GoapAgent class in this library does NOT implement this interface. Rather, GoapAgent 
//holds a reference to an IGoap, which is the link between this library and your particular Goap project.
//Since GoapAgent has a reference to something that implements this interface, it is able to use these
//5 functions to communicate between this library and your project without having to know anything about
//your project
public interface IGoap {

    Dictionary<string, object> GetWorldState();
    Dictionary<string, object> CreateGoalState();
    bool MoveAgent(GoapAction nextAction);
    void PerformingAction(GoapAction action);
    void ActionFinished(GoapAction action);
    
}
