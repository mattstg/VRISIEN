using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFood : GoapAction
{

    public float actionTimer;
    //an initial GoapAction class to begin with; later, addition such GoapActions can easily be created
    //and added to the simulation


    public override void Initialize()
    {
        //Preconditions.Add("HasFood", false);
        Effects.Add("HasFood", true);
        Effects.Add("CanWork", true);
        actionTimer = 0;
        type = ActionType.Food;
        requiresInRange = true;
    }

    public override bool CheckSpecialPreconditions(GoapAgent agent)
    {
        /* Inventory agentInv = agent.gameObject.GetComponent<Inventory>();
         if (Preconditions.Count == 0)
             return true;
         foreach (var p in Preconditions)
             if (agentInv.InventoryDict.ContainsKey(p.Key))
                 if ((bool)agentInv.InventoryDict[p.Key])
                     return true;

         return false; */

        return true;
    }

    public override bool IsDone()
    {
        if (actionTimer > 6f)
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
        var trees = GameObject.FindGameObjectsWithTag("Food");
        target = trees[0].transform;
        var dist = Vector3.Distance(transform.position, trees[0].transform.position);
        for (int i = 0; i < trees.Length; i++)
            if (Vector3.Distance(this.transform.position, trees[i].transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, trees[i].transform.position);
                target = trees[i].transform;
            }
    }
}
