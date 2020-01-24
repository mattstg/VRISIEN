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

    public List<GameObject> ParticlePool;

    List<GameObject> ParticleList;
    public void Initialize()
    {
        ParticleList = new List<GameObject>();
        foreach(GameObject go in System.Enum.GetValues(typeof(GameObject)))
        {
            ParticleList.Add(Resources.Load<GameObject>("Prefabs/ParticlePrefabs" + go.ToString()));
        }

        for(int i=0;i<ParticleList.Count;i++)
        {
            GameObject go = GameObject.Instantiate(ParticleList[i], Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            ParticlePool.Add(go);
        }

    }

    public void SpawnParticle(string particleName,Transform pos,bool isPermanant,float lifeTime)
    {
        GameObject go = null;
        for (int i = 0; i < ParticlePool.Count; i++)
        {
            if(ParticlePool[i].ToString() == particleName)
            {
                go = ParticlePool[i];
                break;
            }
        }
        GameObject.Instantiate(go, pos.position, pos.rotation);
        GameObject.Destroy(go, lifeTime);
    }

}
