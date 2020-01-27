using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager
{
    #region Singleton
    private static ParticlesManager instance;
    private ParticlesManager() { }
    public static ParticlesManager Instance { get { return instance ?? (instance = new ParticlesManager()); } }
    #endregion
    // Start is called before the first frame update
    public enum ParticleType { MuzzleFlash,BloodSplash,StunGunMuzzle,BulletImpact};
    List<GameObject> ParticlePool;
    List<GameObject> ParticleList;
    public void Initialize()
    {
        ParticleList = new List<GameObject>();
        ParticlePool = new List<GameObject>();
        GameObject[] pool = Resources.LoadAll<GameObject>("Prefabs/ParticlePrefabs");
        foreach(GameObject go in pool)
        {
            ParticleList.Add(go);
        }
            

        for(int i=0;i<ParticleList.Count;i++)
        {
            GameObject go = GameObject.Instantiate(ParticleList[i], Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            ParticlePool.Add(go);
        }

    }

    public void CreateParticleEffect(ParticleType particleType,Transform pos,float lifeTime)
    {
        GameObject particlePrefab = null;
        GameObject spawnedParticle;

        if (particleType.Equals(ParticleType.BloodSplash))
            particlePrefab = ParticlePool[0];
        else if(particleType.Equals(ParticleType.BulletImpact))
            particlePrefab = ParticlePool[1];
        else if (particleType.Equals(ParticleType.MuzzleFlash))
            particlePrefab = ParticlePool[2];
        else if (particleType.Equals(ParticleType.StunGunMuzzle))
            particlePrefab = ParticlePool[3];

        spawnedParticle = GameObject.Instantiate(particlePrefab, pos.position, pos.rotation);
        spawnedParticle.transform.SetParent(GameObject.FindGameObjectWithTag("ParticlesParent").transform);
        if(lifeTime != 0)
            GameObject.Destroy(spawnedParticle, lifeTime);
        spawnedParticle.SetActive(true);
    }

}
