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

    Transform enemyParent;

    public HashSet<Enemy> enemies;
    public Stack<Enemy> toRemove;
    public Stack<Enemy> toAdd;

    Dictionary<EnemyType, GameObject> enemyPrefabDict = new Dictionary<EnemyType, GameObject>(); //all enemy prefabs

    public void Initialize()
    {
        toRemove = new Stack<Enemy>();
        toAdd = new Stack<Enemy>();
        enemies = new HashSet<Enemy>();
        enemyParent = new GameObject("EnemyParent").transform;

        foreach (EnemyType etype in System.Enum.GetValues(typeof(EnemyType))) //fill the resource dictionary with all the prefabs
        {
            enemyPrefabDict.Add(etype, Resources.Load<GameObject>("Prefabs/sameer prefabs/Enemy/" + etype.ToString())); //Each enum matches the name of the enemy perfectly
        }

    }

    public Enemy SpawnEnemies(EnemyType etype,int qty,Vector3 location)
    {
        GameObject newEnemy = GameObject.Instantiate(enemyPrefabDict[eType], enemyParent);       //create from prefab
        newEnemy.transform.position = spawnLoc;     //set the position
        Enemy e = newEnemy.GetComponent<Enemy>();   //get the enemy component on the newly created obj
        e.Initialize(startingEnergy);               //initialize the enemy
        toAdd.Push(e);                              //add to update list
        return e;
    }
}
