using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isAlive;
    protected float hp;
    //float nextWaveCounter = 10f;
    //float TempNextWaveCounter = 0f;

    //int meleeCountPerLocation = 0;
    //int rangedCountPerLocation = 0;
    //int droneCountPerLocation = 0;

    //public Animator brainAnimator;
    //public Animator modelAnimator;
    public virtual void Initialize(float _hp=100)
    {
        
        //Debug.Log("Enemy Initialize()");
        //TempNextWaveCounter = nextWaveCounter;
        isAlive = true;
        hp = _hp;
        //meleeCountPerLocation = 3;
        //rangedCountPerLocation = 5;
        //droneCountPerLocation = 2;
        //meleeCountPerLocation = GetComponent<EnemyController>().CountOfMeleeEnemyAtOneSpawnLocation;
        //rangedCountPerLocation = GetComponent<EnemyController>().CountOfRangedEnemyAtOneSpawnLocation;
        //droneCountPerLocation = GetComponent<EnemyController>().CountOfDroneEnemyAtOneSpawnLocation;
    }
  
    public virtual void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0) { 
            Debug.Log("Hp: "+hp);
             Die();            
            isAlive = false;
        }
    }
    public virtual void Refresh()
    {
        
        //Debug.Log("Enemy Refresh()");
        //if (EnemyManager.Instance.enemies.Count <= 0)
        //{
        //    TempNextWaveCounter -= Time.deltaTime;
        //    if (TempNextWaveCounter <= 0)
        //    {
        //        EnemyManager.Instance.toAdd.Clear();
        //        EnemyManager.Instance.toRemove.Clear();
        //        EnemyManager.Instance.NumberOfEnemyToSpawn(meleeCountPerLocation, rangedCountPerLocation, droneCountPerLocation);
        //        TempNextWaveCounter = nextWaveCounter;
        //    }
        //}
    }

    
    public virtual void Die()
    {
        EnemyManager.Instance.EnemyDied(this);
        SoundManager.Instance.PlayMusic("Enemy_Die", gameObject);
        isAlive = false;
    }
}
