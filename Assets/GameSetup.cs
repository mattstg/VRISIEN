using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{

    public Transform SpawnTriggerLocation;
    //public Transform SpawnLocation;
    
    public int CountOfMeleeEnemyAtOneSpawnLocation = 3;
    public int MeleeHealth = 100;
    public int CountOfRangedEnemyAtOneSpawnLocation = 5;
    public int RangedHealth = 100;
    public int CountOfDroneEnemyAtOneSpawnLocation = 0;
    public int DroneHealth = 0;
    public int attackSlots = 2;

    int EnemyCountInScene { get 
        {
            if (EnemyManager.Instance.enemies.Count > 0)
                return EnemyManager.Instance.enemies.Count;
            else
                return 0;
        } 
    }
    float TimePassed = 0;
    int CollectablesCount = 0;

    bool BossDead = false;

    bool CollectedSword = false;
    bool Collectedcolllectable = false;
    [HideInInspector]
    public Transform EnemySpawnTrigger;
    [HideInInspector]
    public bool PlayerTriggeredEnemy = false;
    //bool EnemySpawnedAfterTriggered = false;
    Animator StateMachineFlowAnimator;
    [HideInInspector]
    public Transform player;

    
    public void Initialize()
    {
        //GameSetupClass.Instance.SpawnLocation = SpawnLocation;
        
        GameSetupClass.Instance.CountOfMeleeEnemyAtOneSpawnLocation = CountOfMeleeEnemyAtOneSpawnLocation;
        GameSetupClass.Instance.CountOfRangedEnemyAtOneSpawnLocation = CountOfRangedEnemyAtOneSpawnLocation;
        GameSetupClass.Instance.CountOfDroneEnemyAtOneSpawnLocation = CountOfDroneEnemyAtOneSpawnLocation;
        GameSetupClass.Instance.MeleeHealth = MeleeHealth;
        GameSetupClass.Instance.RangedHealth = RangedHealth;
        GameSetupClass.Instance.DroneHealth = DroneHealth;

        GameSetupClass.Instance.EnemyCountInScene = EnemyCountInScene;
        


        EnemySpawnTrigger = GameObject.FindGameObjectWithTag("EnemySpawnTrigger").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StateMachineFlowAnimator = gameObject.GetComponent<Animator>();
        
    }

    public void Refresh(float DTime)
    {
        if(CollectableManager.Instance.gotChip)
        {
            Collectedcolllectable = true;
        }
        if (CollectableManager.Instance.gotSword)
        {
            CollectedSword = true;
        }

        // Debug.Log("GameSetupRefresh");       
            if (!PlayerTriggeredEnemy && TimePassed <= 120 && EnemyCountInScene == 0 || Input.GetKeyDown(KeyCode.Alpha1))
        {
           // Debug.Log("main");
            TimePassed += DTime;
            StateMachineFlowAnimator.SetBool("SpawnEnemy", false);
            StateMachineFlowAnimator.SetBool("AllEnemyDead", true);

        }


        if (PlayerTriggeredEnemy || TimePassed >= 120f || Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Debug.Log("SpawnEnemies");
            PlayerTriggeredEnemy = false;
            TimePassed = 0;
            StateMachineFlowAnimator.SetBool("AllEnemyDead", false);
            StateMachineFlowAnimator.SetBool("SpawnEnemy", true);

        }
        if (CollectedSword ||  Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Boss");
            StateMachineFlowAnimator.SetBool("SpawnEnemy", false);

            StateMachineFlowAnimator.SetBool("AllEnemyDead", true);
            StateMachineFlowAnimator.SetBool("CollectedSword", true);
        }
        if (BossDead || Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Exit");
            StateMachineFlowAnimator.SetBool("BossKilled", true);
        }


    }
    public void PostInitialize()
    { 
    
    }
    public void PhysicsRefresh()
    {
    }
    public void SwordCollected()
    {
        CollectedSword = true;
    }

    public void EnemySpawnTriggerZone() { 
    
    }
    
 }