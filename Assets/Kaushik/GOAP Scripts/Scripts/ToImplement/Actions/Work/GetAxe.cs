using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAxe : GoapAction
{

    public float actionTimer;
    //an initial GoapAction class to begin with; later, addition such GoapActions can easily be created
    //and added to the simulation


    public override void Initialize()
    {
        // Preconditions.Add("HasAxe", true);
        Effects.Add("HasAxe", true);
        actionTimer = 0;
        type = ActionType.Work;
        requiresInRange = true;
        target = GameObject.Find("Axe").transform;
    }

    public override bool CheckSpecialPreconditions(GoapAgent agent)
    {
        /*var agentScript = agent.GetComponent<NPC>();
             if (agentScript.isFatigued || agentScript.isHungry || agentScript.isThirsty)
                 return false; */
        return true;
        /* Inventory agentInv = agent.gameObject.GetComponent<Inventory>();
         if (Preconditions.Count == 0)
             return true;
         foreach (var p in Preconditions)
             if (agentInv.InventoryDict.ContainsKey(p.Key))
                 if ((bool)agentInv.InventoryDict[p.Key])
                     return true;

         return false; */

    }

    public override bool IsDone()
    {
        if (actionTimer > 2f)
            return true;
        else return false;
    }

    public override bool Perform(GoapAgent agent)
    {
        actionTimer += Time.deltaTime;
        return true;
    }

    public override void Reset()
    {
        actionTimer = 0;
        InRange = false;
    }
}
