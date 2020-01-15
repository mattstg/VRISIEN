using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//BulletManager
//In progress
public enum WeaponType { StunGun, Sword };
public class BulletManager
{
    #region Singleton
    private static BulletManager instance;
    private BulletManager() { }
    public static BulletManager Instance { get { return instance ?? (instance = new BulletManager()); } }
    #endregion


    public Dictionary<WeaponType, GameObject> prefabDict;
    Transform weaponParent;

    public void Initialize()
    {
        Debug.Log("BulletManager");
        weaponParent = new GameObject("Weapon").transform;
        prefabDict = new Dictionary<WeaponType, GameObject>();
        prefabDict.Add(WeaponType.StunGun, Resources.Load<GameObject>("Prefabs/StunGun"));
    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {

    }

    public void Refresh()
    {
    }
}
