using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IGoap
{

    public enum Jobs
    {
        Wood, Stone
    };

    public NPCStats stats = new NPCStats();
    public bool isFatigued, isHungry, isThirsty;


    //this class is our agent's monobehaviour from the implementation side (rather than the GOAP side, which has GoapAgent)
    //it implements IGoap, so that the corresponding GoapAgent can call the methods it needs without needing to know
    //about this class.
    //this class will need to create an inventory
    //this class is abstract, so you will need to create children classes.
    //GetWorldState should be abstract on this level, and left to be defined by children classes
    //This class will be on the same prefab as a GoapAgent component and a component for each action this agent can perform

    public abstract Dictionary<string, object> CreateGoalState();

    public abstract Dictionary<string, object> GetWorldState();


    public abstract bool MoveAgent(GoapAction nextAction);


    public abstract void PerformingAction(GoapAction action);



    public abstract void ActionFinished(GoapAction action);


    public class NPCStats
    {
        int age;

        public int fatigueThresh;
        public int foodThresh;
        public int waterThresh;

        public int workProficiencyMult;
        public int fatigueMult;
        public int foodMult;
        public int waterMult;

        public Jobs jobType;

        public void Initialize(NPCStats other = null)
        {
            if (other != null)
            {
                age = other.age;
                fatigueThresh = other.fatigueThresh;
                foodThresh = other.foodThresh;
                waterThresh = other.waterThresh;
                workProficiencyMult = other.workProficiencyMult;
                fatigueMult = other.fatigueMult;
                foodMult = other.foodMult;
                waterMult = other.waterMult;
                jobType = other.jobType;
            }

            else
            {
                age = Random.Range(0, 60) * Random.Range(1, 5) * Random.Range(1, 5);

                fatigueThresh = Random.Range(10, 41);
                foodThresh = Random.Range(10, 41);
                waterThresh = Random.Range(10, 41);

                workProficiencyMult = Random.Range(1, 7);
                fatigueMult = Random.Range(1, 7);
                foodMult = Random.Range(10, 24);
                waterMult = Random.Range(5, 12);
            }
    
        }
    }
}
