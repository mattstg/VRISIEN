using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponManager
// This class is for managing 3 different weapons ( SWORD and STUNGUN (that player will use) and a default GUN that AI would USE (only if in a case AI uses a gun other than stunGun) )
// but for now this class will only manage STUN GUN that a player will use.

public class WeaponManager
{
    GameObject go, go2;
    GameObject gunParent;
    public List<StunGun> guns;
    public List<Blade> blades;

    StunGun stunGun;
    Blade blade;

    #region Singleton
    private static WeaponManager instance;
    private WeaponManager() { }
    public static WeaponManager Instance { get { return instance ?? (instance = new WeaponManager()); } }
    #endregion
    Vector3 spawnLoc, spawnLoc2;
    public void Initialize()
    {
        go = Resources.Load<GameObject>("Prefabs/StunGun");
        go2 = Resources.Load<GameObject>("Prefabs/Blade");
        guns = new List<StunGun>();
        blades = new List<Blade>();
        gunParent = new GameObject("GunParent");
        spawnLoc = GameObject.Find("Player").transform.position + Vector3.right/1.5f + Vector3.up * 2f;
        spawnLoc2 = GameObject.Find("Player").transform.position - Vector3.right / 1.5f + Vector3.up * 2f;

        spawnGun(spawnLoc); // spawnGun(spawnLoc) must be called at the time of playerSpawning since gun needs to get attached to the player.
        spawnBlade(spawnLoc2);
        Debug.Log("WeaponManager");
        stunGun.Initialize();
        blade.Initialize();
    }

    public void PostInitialize() { }
    public void PhysicsRefresh()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i]?.PhysicsRefresh();
        }
        blade.PhysicsRefresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i]?.Refresh();
        }
        blade.Refresh();
    }


    // spawning a gun
    public StunGun spawnGun(Vector3 spawnLoc)
    {
        // can be spawned and attached to the player's location (in this case i m using a random location to spawn a gun). 
        stunGun = GameObject.Instantiate(go, spawnLoc, Quaternion.identity, gunParent.transform).GetComponent<StunGun>();
        guns.Add(stunGun);
        return stunGun;
    }

    public Blade spawnBlade(Vector3 spawnLoc)
    {
        blade = GameObject.Instantiate(go2, spawnLoc, Quaternion.identity).GetComponent<Blade>();
        blades.Add(blade);
        return blade;
    }
}
