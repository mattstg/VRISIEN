﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStone : GoapAction
{

    public float actionTimer;
    //an initial GoapAction class to begin with; later, addition such GoapActions can easily be created
    //and added to the simulation


    public override void Initialize()
    {
        Preconditions.Add("HasStone", true);
        Preconditions.Add("CanWork", true);
        Effects.Add("MakeMoney", true);
        Effects.Add("HasStone", false);
        type = ActionType.Work;
        actionTimer = 0;

        requiresInRange = true;
        target = GameObject.Find("House").transform;
    }

    public override bool CheckSpecialPreconditions(GoapAgent agent)
    {
        /* Inventory agentInv = agent.gameObject.GetComponent<Inventory>();
         if (Preconditions.Count == 0)
             return true;
         foreach (var p in Preconditions)
             if (agentInv.InventoryDict.ContainsKey(p.Key))
                 if ((bool)agentInv.InventoryDict[p.Key])
                 {
                     return true;
                 }

         return false; */
       /* var agentScript = agent.GetComponent<NPC>();
        if (agentScript.isFatigued || agentScript.isHungry || agentScript.isThirsty)
            return false; */
        return true;
    }

    public override bool IsDone()
    {
        if (actionTimer > 3f)
        {
            return true;
        }
        else return false;
    }

    public override bool Perform(GoapAgent agent)
    {
        actionTimer += Time.deltaTime;
        return true;
    }

    public override void Reset()
    {
        InRange = false;
        actionTimer = 0;
    }
}