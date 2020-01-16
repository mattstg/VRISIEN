using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
// In progress
public enum EnemyType { ZombieGirl } //, ZombieCop
public class EnemyManager
{
    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    [HideInInspector]public Transform enemyParent;
    public HashSet<Enemy> enemies;

    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs


    public void Initialize()
    {
        enemies = new HashSet<Enemy>();
        enemyParent = new GameObject("ZombieParent").transform;

        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }

        SpawnRandomEnemies();
    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {
        foreach (Enemy e in enemies)
                e.PhysicsRefresh();
    }

    public void Refresh()
    {
        foreach (Enemy e in enemies)
                e.Refresh();
    }


    public Enemy SpawnEnemy(EnemyType eType, Vector3 spawnLoc)
    {
        GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[eType],enemyParent);
        newEnemy.transform.position = spawnLoc;
        // add state machine controller for enemy behavior
        //Animator enemyStateController = newEnemy.AddComponent<Animator>();
        //enemyStateController.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Controller/EnemyStateController");
        return newEnemy.GetComponent<Enemy>();   
    }


    public void SpawnRandomEnemies()
    {
        
        foreach(Transform t in GameLinks.gl.enemySpawnLocations)
        {
            Enemy e = SpawnEnemy(EnemyType.ZombieGirl, t.position + new Vector3(0, t.localScale.y, 0));
            e.Initialize();
            enemies.Add(e);
        }
            
    }
}
*/