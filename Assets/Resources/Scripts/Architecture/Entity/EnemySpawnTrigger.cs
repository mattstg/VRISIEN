using System;
using System.Collections.Generic;
using UnityEngine;


class EnemySpawnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        GameObject triggeredObj = c.gameObject;

        if(triggeredObj.tag == "Player")
        {
            GameSetup.gs.PlayerTriggeredEnemy = true;
        }
    }
}

