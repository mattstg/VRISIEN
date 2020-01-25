using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {Melee,Ranged,Drone}
public class EnemyManager 
{

    #region Singleton
    private static EnemyManager instance;
    private EnemyManager() { }
    public static EnemyManager Instance { get { return instance ?? (instance = new EnemyManager()); } }
    #endregion

    float meleeHealth,rangedHealth;
    Transform enemyParent;
    Transform SpawnLocations;
    public int enemyCountofCurrentWave = 0;
    public HashSet<Enemy> enemies;//stacks to keep track of enemies
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;
    //public int EnemyCount { get { return enemies.Count; } }

    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs

    public void Initialize()
    {
        //Debug.Log("EnemyManager Initialize()");
        meleeHealth = GameSetupClass.Instance.MeleeHealth;
        rangedHealth = GameSetupClass.Instance.RangedHealth;
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        enemyParent = new GameObject("EnemyParent").transform;
        SpawnLocations = GameObject.FindGameObjectWithTag("EnemySpawnLocationParent").transform;
        //SpawnLocations = GameSetupClass.Instance.SpawnLocation;
        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/sameer prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }
        //Initially spawning enemies
        //NumberOfEnemyToSpawn(2,3,0);
       // NumberOfEnemyToSpawn(1, 0, 0);

    }
    public void Refresh()
    {
        // Debug.Log("EnemyManager Refresh()");
        foreach (Enemy e in enemies)
        {
            if (e.isAlive)
            {
                e.Refresh();
            }
        }


        while (toRemove.Count > 0) //remove all dead ones
        {
            Enemy e = toRemove.Pop();
            enemies.Remove(e);
            GameObject.Destroy(e.gameObject);
        }

        while (toAdd.Count > 0) //Add new ones
            enemies.Add(toAdd.Pop());
    }
    public void PhysicsRefresh() { }
    public void PostInitialize() { }
    public void SetSpawnLocations(Transform spawnLocation)
    {
        Debug.Log("Setspawnlocation in enemy mnger");

        SpawnLocations = spawnLocation;
    }
    public Transform GetSpawnLocations()
    {
        return SpawnLocations ;
    }

    
    public void NumberOfEnemyToSpawn(int melee,int ranged,int drone)
    {
        SpawnEnemies(EnemyType.Melee, melee);
        SpawnEnemies(EnemyType.Ranged, ranged);
        SpawnEnemies(EnemyType.Drone, drone);

    }

    public Enemy SpawnEnemies(EnemyType etype,int qty)
    {
        Enemy e=null;
        if(SpawnLocations != null)
            foreach (Transform spawnArea in SpawnLocations)  
            {
                for (int i = 0; i < qty; i++)
                {
                    Vector3 spawnLocation = new Vector3(Random.Range(-spawnArea.localScale.x * 10 / 2, spawnArea.localScale.x * 10 / 2), 1, Random.Range(-spawnArea.localScale.z * 10 / 2, spawnArea.localScale.z * 10 / 2));
                    GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[etype], enemyParent);
                
                    newEnemy.transform.position = spawnArea.position;
                    newEnemy.transform.position += spawnLocation;
                    Debug.Log("New Enemy Created");
                    e = newEnemy.GetComponent<Enemy>();
                
                    if (etype.Equals(EnemyType.Melee))
                        e.Initialize(meleeHealth);
                    else if(etype.Equals(EnemyType.Ranged))
                        e.Initialize(rangedHealth);

                    toAdd.Push(e);  
                }
           
            }
        enemyCountofCurrentWave = toAdd.Count;
        return e;
    }
    public void EnemyDied(Enemy enemyDied)
    {
        toRemove.Push(enemyDied);
    }
}
