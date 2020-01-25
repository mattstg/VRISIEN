using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is an abstract class, so we need to create a child of this class for every type of action we make
public abstract class GoapAction : MonoBehaviour {

    [HideInInspector]
    public Dictionary<string, object> Preconditions { get; private set; } //required world state for this action to be performed
    [HideInInspector]
    public Dictionary<string, object> Effects { get; private set; } //changes to world state upon this action being performed
    [HideInInspector]
    public bool InRange { get; set; }
    [HideInInspector]
    public bool requiresInRange; //if this is false, InRange and target are meaningless and can be left their default values
    public float cost; 

    [HideInInspector]
    public Transform target; //if our action needs a target, then whenever it is performed, we will set this target
                             // with custom logic in our particular GoapAction class

        public enum ActionType
        {
        Work, Rest, Food, Water
        };

    public ActionType type;

    public void Start()
    {
        Preconditions = new Dictionary<string, object>();
        Effects = new Dictionary<string, object>();
        Initialize();
    }


    //these are all the abstract methods every GoapAction must implement.
    public abstract void Initialize(); //what does our action need to do when it is first initialized? like a start function
    public abstract bool Perform(GoapAgent agent);//what code does our action run while it's being performed by an agent?
    public abstract bool IsDone();//how do we determine if the action is done?
    public abstract void Reset();//how do we return our action to its default state to be used by the planner again? ie: setting inRange back to false
    public abstract bool CheckSpecialPreconditions(GoapAgent agent);//this is here in case we want some action to have special custom prerequisites 
                                                                    //that are separate from Preconditions (for example, maybe our agent cant
                                                                    //use the anvil while another agent is using it. That logic would go in this method)

}
