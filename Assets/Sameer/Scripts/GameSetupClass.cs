using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupClass
{
    #region Singleton
    private static GameSetupClass instance = null;

    private GameSetupClass() { }

    public static GameSetupClass Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameSetupClass();
            }
            return instance;
        }
    }
    #endregion Singleton

    public Transform SpawnLocation;
    public int CountOfMeleeEnemyAtOneSpawnLocation = 3;
    public int MeleeHealth = 100;

    public int CountOfRangedEnemyAtOneSpawnLocation = 5;
    public int RangedHealth = 100;
    public int CountOfDroneEnemyAtOneSpawnLocation = 0;
    public int DroneHealth = 0;
    public int EnemyCountInScene = 0;
    //public float TimePassed = 0;
    public int CollectablesCount = 0;

    public bool BossDead = false;

    public bool CollectedSword = false;

    public Transform EnemySpawnTrigger;

    public bool PlayerTriggeredEnemy = false;



    public Transform player;
    public void Initialize()
    {
       // EnemyManager.Instance.SetSpawnLocations(SpawnLocation);
    }

    public void PostInitialize()
    {

    }

    public void Refresh(float DTime)
    { 
    }
    public void PhysicsRefresh()
    {
    }
    public void incrementCollectableCount()
    {
        CollectablesCount++;
        PlayerTriggeredEnemy = true;
    }

}
