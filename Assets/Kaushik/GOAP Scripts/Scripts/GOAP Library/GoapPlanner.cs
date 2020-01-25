using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//This class contains the main guts of the algorithm. 
//Each GoapAgent creates and stores a link to one of these.
//This class is basically glorified A*, slightly modified to work in the Goap paradigm
public class GoapPlanner:MonoBehaviour {


    List<GoapAction> usableActions;
    //The one public function that is available to our GoapAgents. This function returns to them a plan 
    //given an agent, a current state, a goal state, and a list of available actions.
    //All other methods in this class are private, and are just used by this method.
    public Queue<GoapAction> CalculatePlan(GoapAgent agent, List<GoapAction> availableActions, Dictionary<string, object> worldState, Dictionary<string, object> goalState)
    {
        foreach (GoapAction gA in availableActions)
            gA.Reset();

        usableActions = new List<GoapAction>();
        foreach (GoapAction gA in availableActions)
            if (gA.CheckSpecialPreconditions(agent))
                usableActions.Add(gA);
        Debug.Log(usableActions.Count);
        List<Node> leaves = new List<Node>();

        Node start = new Node(null, 0, worldState, null);
        bool success = BuildGraph(start, leaves, usableActions, goalState);

        if (!success)
            return null;

        Node cheapest = null;
        foreach (Node leaf in leaves)
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.runningCost < cheapest.runningCost)
                    cheapest = leaf;
            }

        List<GoapAction> result = new List<GoapAction>();
        Node n = cheapest;

        while(n != null)
        {
            if (n.action != null)
                result.Add(n.action);
            n = n.parent;
        }
        result.Reverse();

        Queue<GoapAction> toReturn = new Queue<GoapAction>();

        foreach (GoapAction gA in result)
            toReturn.Enqueue(gA);

        return toReturn;

    }

	
    bool BuildGraph(Node parent, List<Node> leaves, List<GoapAction> usableActions, Dictionary<string, object> goalState )
    {
        bool foundAPath = false;

        foreach(GoapAction action in usableActions)
        {
            if(InState(action.Preconditions, parent.state))
            {
                Dictionary<string, object> currentState = ChangeState(parent.state, action.Effects);
                Node node = new Node(parent, parent.runningCost + action.cost, currentState, action);

                if(InState(goalState, currentState))
                {
                    leaves.Add(node);
                    foundAPath = true;
                }
                else
                {
                    List<GoapAction> subset = ActionSubset(usableActions, action);
                    if (BuildGraph(node, leaves, subset, goalState))
                        foundAPath = true;
                }
            }
        }

        return foundAPath;
    }

    List<GoapAction> ActionSubset(List<GoapAction> actions, GoapAction toRemove)
    {
        List<GoapAction> toReturn = new List<GoapAction>();
        foreach (GoapAction gA in actions)
            if (gA != toRemove)
            {
                
                toReturn.Add(gA);
            }

        foreach (var e in toRemove.Effects)
            if (e.Value.GetType() == typeof(int))
            {
                toReturn.Add(toRemove);
                break;
            }
        return toReturn;
    }

    bool InState(Dictionary<string, object> toTest, Dictionary<string, object> toTestAgainst)
    {
        bool allMatch = true;
        foreach (KeyValuePair<string, object> t in toTest.ToList())
        {
            bool match = false;
            foreach (KeyValuePair<string, object> s in toTestAgainst)
                if (s.Value.GetType() == typeof(bool))
                {
                    if (s.Equals(t))
                    {
                        match = true;
                        break;
                    }
                }
                else
                    if(s.Key == t.Key)
                        if(s.Equals(t))
                    {
                        Debug.Log("Sdhaw");
                        match = true;
                        break;
                    }
                   /* else
                    {
                        if ((int)s.Value > 0)
                        {
                            if (!toTest.ContainsKey(s.Key + ((int)t.Value - (int)s.Value).ToString()))
                            {
                                toTest.Add(s.Key + ((int)t.Value - (int)s.Value).ToString(), (int)t.Value - (int)s.Value);
                                Debug.Log((int)t.Value - (int)s.Value);
                            }
                        }
                        if (toTest.ContainsKey(s.Key + ((int)t.Value - (int)s.Value).ToString()))
                            Debug.Log(toTest[s.Key + ((int)t.Value - (int)s.Value).ToString()]);
                    } */ 
            if (!match)
                allMatch = false;
        }
        return allMatch;
    }

    Dictionary<string, object> ChangeState(Dictionary<string, object> currentState, Dictionary<string, object> stateChange)
    {
        Dictionary<string, object> toReturn = new Dictionary<string, object>();
        foreach (KeyValuePair<string, object> kVP in currentState)
        {
            toReturn.Add(kVP.Key, kVP.Value);
        }
        foreach (var c in stateChange)
        {
          //  Debug.Log(c.Key+ c.Value);
        }
        foreach (KeyValuePair<string, object> kVP in stateChange)
        {
            if (toReturn.ContainsKey(kVP.Key))
                if (kVP.Value.GetType() == typeof(bool))
                {
                    toReturn.Remove(kVP.Key);
                    toReturn.Add(kVP.Key, kVP.Value);
                }
                else
                {

                    /* int val = (int)toReturn[kVP.Key]; 
                       val  += (int)kVP.Value;
                     currentState[kVP.Key] = val;
                     toReturn[kVP.Key] = val;
                     Debug.Log(toReturn[kVP.Key]); */
                    toReturn.Remove(kVP.Key);
                    toReturn.Add(kVP.Key /*+ kVP.Value.ToString()*/,kVP.Value);
                }
        }
        return toReturn;
    }

    private class Node
    {
        public Node parent;
        public float runningCost;
        public Dictionary<string, object> state;
        public GoapAction action;

        public Node(Node _parent, float _runningCost, Dictionary<string, object> _state, GoapAction _action)
        {
            parent = _parent;
            runningCost = _runningCost;
            state = _state;
            action = _action;
        }
    }
}
