using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peasant : NPC {

    //an initial NPC class to start with; later, other NPC classes like this can easily be made and dropped in
    //the simulation
    public Inventory inv;
    public GoapAgent thisAgent;
    public SkillGenome skillGenome;

    public float fatigue, foodLevel, waterLevel;

    private void Start()
    {
        thisAgent = gameObject.AddComponent<GoapAgent>();
        inv = gameObject.AddComponent<Inventory>();
        skillGenome = gameObject.AddComponent<SkillGenome>();
        stats.Initialize();
        stats.jobType = Random.Range(0, 2) == 0 ? Jobs.Wood : Jobs.Stone;

        fatigue = 100;
        foodLevel = 100;
        waterLevel = 100;

        inv.InventoryDict.Add("MakeMoney", false);
        inv.InventoryDict.Add("HasAxe", false);
        inv.InventoryDict.Add("HasWood", false);
        inv.InventoryDict.Add("HasPickaxe", false);
        inv.InventoryDict.Add("HasStone", false);
        inv.InventoryDict.Add("HasFire", false);
        inv.InventoryDict.Add("HasFood", true);
        inv.InventoryDict.Add("HasWater", true);
        inv.InventoryDict.Add("HasRest", true);
        inv.InventoryDict.Add("CanWork" ,true);
    }

    public override Dictionary<string, object> CreateGoalState()
    {
        Dictionary<string, object> goal = new Dictionary<string, object>()
        {
            { "MakeMoney", true }
        };
        
        return goal;
    }

    public override Dictionary<string, object> GetWorldState()
    {
        return inv.InventoryDict;
    }



    public override bool MoveAgent(GoapAction nextAction)
    {
        if (fatigue > stats.fatigueThresh)
        {
            fatigue -= Time.deltaTime * stats.fatigueMult;
            isFatigued = false;
        }
        else
        {
            inv.InventoryDict["HasRest"] = false;
            isFatigued = true;
        }

        if(isFatigued&&isHungry&&isThirsty)
            inv.InventoryDict["CanWork"] = false;


        transform.LookAt(nextAction.target);
        transform.Translate(transform.forward * Time.deltaTime * 5f, Space.World);
        if (Vector3.Distance(transform.position, nextAction.target.transform.position) <= 5f)
        {
            nextAction.InRange = true;
            return true;
        }
        return false;

    }


    public override void PerformingAction(GoapAction action)
    {
        if (action.type == GoapAction.ActionType.Work)
        {
            if (fatigue > stats.fatigueThresh)
            {
                fatigue -= Time.deltaTime * stats.fatigueMult;
                isFatigued = false;
            }
            else
            {
                inv.InventoryDict["HasRest"] = false;
                isFatigued = true;
            }

            if (waterLevel > stats.waterThresh)
            {
                waterLevel -= Time.deltaTime * stats.waterMult;
                isThirsty = false;
            }
            else
            {
                inv.InventoryDict["HasWater"] = false;
                isThirsty = true;
            }

            if (foodLevel > stats.foodThresh)
            {
                foodLevel -= Time.deltaTime * stats.foodMult;
                isHungry = false;
            }
            else
            {
                inv.InventoryDict["HasFood"] = false;
                isHungry = true;
            }

        }


        else if (action.type == GoapAction.ActionType.Food)
            foodLevel = 100;
        else if (action.type == GoapAction.ActionType.Water)
            waterLevel = 100;
        else if (action.type == GoapAction.ActionType.Rest)
            fatigue = 100;

        if (isFatigued && isHungry && isThirsty)
            inv.InventoryDict["CanWork"] = false;

        if (action.Perform(thisAgent)) ;
        //    curActionTimer += Time.deltaTime;

    }



    public override void ActionFinished(GoapAction action)
    {
        if (action.IsDone())
        {
            foreach (var e in action.Effects)
            {
                if (e.Value.GetType() == typeof(bool))
                {
                    if (!inv.InventoryDict.ContainsKey(e.Key))
                    {
                        inv.InventoryDict.Add(e.Key, e.Value);
                    }
                }
                else
                {
                    if (!inv.InventoryDict.ContainsKey(e.Key))

                        inv.InventoryDict.Add(e.Key, e.Value);

                    else
                    {
                        int eVal = (int)inv.InventoryDict[e.Key];
                        eVal += (int)e.Value;
                        inv.InventoryDict[e.Key] = eVal;
                    }
                }
            }
            
        }


    }


}
