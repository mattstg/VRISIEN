//bulletmanager
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


    public List<Bullet> bulletList;
    GameObject bulletPrefab;
    Transform bulletParent;

    public void Initialize()
    {
        Debug.Log("BulletManager is Loaded");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = new GameObject("BulletParent").transform;
        bulletList = new List<Bullet>();

        foreach (Bullet b in bulletList)
        {
            b.Initialize();
        }

    }


    public void PostInitialize()
    {

        foreach (Bullet b in bulletList)
        {

            b.PostInitialize();
        }
    }

    public void PhysicsRefresh()
    {
        foreach (Bullet b in bulletList)
        {

            b.PhysicsRefresh();
        }
    }

    public void Refresh()
    {
        foreach (Bullet b in bulletList)
        {

            b.Refresh();
        }
    }
    public Bullet CreateBullet(Transform trans)
    {
        Bullet bullet = null;
        Debug.Log("Bullet is Created");
        if (bulletList.Count == 0)
        {
            bullet = GameObject.Instantiate(bulletPrefab, trans.position, trans.rotation, bulletParent).GetComponent<Bullet>();
            bulletList.Add(bullet);
            bullet.Initialize();
            bullet.PostInitialize();
            bulletList.Add(bullet);
        }
        else
        {
            // After a short break
        }
        return bullet;
    }
}
