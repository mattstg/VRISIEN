using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponManager
// This class is for managing 3 different weapons ( SWORD and STUNGUN (that player will use) and a default GUN that AI would USE (only if in a case AI uses a gun other than stunGun) )
// but for now this class will only manage STUN GUN that a player will use.

public class WeaponManager
{
    GameObject go;
    GameObject gunParent;
    public List<StunGun> guns;

    StunGun stunGun;

    #region Singleton
    private static WeaponManager instance;
    private WeaponManager() { }
    public static WeaponManager Instance { get { return instance ?? (instance = new WeaponManager()); } }
    #endregion
    Vector3 spawnLoc = new Vector3(0, 1, -1);
    public void Initialize()
    {
        go = Resources.Load<GameObject>("Prefabs/StunGun");
        guns = new List<StunGun>();
        gunParent = new GameObject("GunParent");
        spawnGun(spawnLoc); // spawnGun(spawnLoc) must be called at the time of playerSpawning since gun needs to get attached to the player.
        Debug.Log("WeaponManager");
        stunGun.Initialize();
    }

    public void PostInitialize() { }
    public void PhysicsRefresh()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i]?.PhysicsRefresh();
        }
    }
    public void Refresh()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i]?.Refresh();
        }
    }
    // spawning a gun
    public StunGun spawnGun(Vector3 spawnLoc)
    {
        // can be spawned and attached to the player's location (in this case i m using a random location to spawn a gun). 
        stunGun = GameObject.Instantiate(go, spawnLoc, Quaternion.identity, gunParent.transform).GetComponent<StunGun>();
        guns.Add(stunGun);
        return stunGun;
    }
}
