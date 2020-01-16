using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform SpawnLocation;
    public int CountOfMeleeEnemyAtOneSpawnLocation = 3;
    public int CountOfRangedEnemyAtOneSpawnLocation = 5;
    public int CountOfDroneEnemyAtOneSpawnLocation =2;

    void Awake()
    {
        Debug.Log("controller Awake");
        EnemyManager.Instance.SetSpawnLocations(SpawnLocation);
    }

}
