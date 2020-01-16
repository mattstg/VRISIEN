using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isAlive;
    protected float hp=100f;
    float nextWaveCounter = 10f;
    float TempNextWaveCounter = 0f;

    int meleeCountPerLocation = 0;
    int rangedCountPerLocation = 0;
    int droneCountPerLocation = 0;

    public virtual void Initialize()
    {
        TempNextWaveCounter = nextWaveCounter;
        isAlive = true;
        meleeCountPerLocation = GetComponent<EnemyController>().CountOfMeleeEnemyAtOneSpawnLocation;
        rangedCountPerLocation = GetComponent<EnemyController>().CountOfRangedEnemyAtOneSpawnLocation;
        droneCountPerLocation = GetComponent<EnemyController>().CountOfDroneEnemyAtOneSpawnLocation;
    }
  
    public virtual void HitByProjectile(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }
    public virtual void Refresh()
    {
        if (EnemyManager.Instance.enemies.Count <= 0)
        {
            TempNextWaveCounter -= Time.deltaTime;
            if (TempNextWaveCounter <= 0)
            {
                EnemyManager.Instance.toAdd.Clear();
                EnemyManager.Instance.toRemove.Clear();
                EnemyManager.Instance.NumberOfEnemyToSpawn(meleeCountPerLocation, rangedCountPerLocation, droneCountPerLocation);
                TempNextWaveCounter = nextWaveCounter;
            }
        }
    }
    public virtual void Die()
    {
        EnemyManager.Instance.EnemyDied(this);
        isAlive = false;
    }
}
