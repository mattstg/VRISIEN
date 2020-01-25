
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    }

    public void PostInitialize()
    {
        if (bulletList != null)
        {
            foreach (Bullet b in bulletList)
            {
                b.PostInitialize();
            }
        }
    }

    public void PhysicsRefresh()
    {
        if (bulletList != null)
        {
            foreach (Bullet b in bulletList)
            {
                b.PhysicsRefresh();
            }
        }
    }

    public void Refresh()
    {
        if (bulletList != null)
        {
            foreach (Bullet b in bulletList)
            {
                b.Refresh();
            }
        }
    }
    public Bullet CreateBullet(Transform trans)
    {
        Bullet bullet = null;
        if (bulletList.Count == 0)
        {
            bullet = GameObject.Instantiate(bulletPrefab, trans.position, trans.rotation, bulletParent).GetComponent<Bullet>();
            bullet.Initialize();
            bullet.PostInitialize();
            bulletList.Add(bullet);
            return bullet;
        }
        else
        {
            foreach (Bullet b in bulletList)
            {
                if (b.gameObject.activeSelf)
                {
                    bullet = GameObject.Instantiate(bulletPrefab, trans.position, trans.rotation, bulletParent).GetComponent<Bullet>();

                    bullet.Initialize();
                    bullet.PostInitialize();
                    bulletList.Add(bullet);
                    return bullet;
                }
                else
                {

                    int inactiveBullet = 0;
                    for (int i = 0; i < bulletList.Count; i++)
                    {
                        if (!bulletList[i].gameObject.activeSelf)
                            inactiveBullet = i;
                    }
                    bulletList[inactiveBullet].gameObject.SetActive(true);
                    bullet = bulletList[inactiveBullet].gameObject.GetComponent<Bullet>();
                    bullet.transform.position = trans.position;
                    bullet.transform.rotation = trans.rotation;
                    bullet.Initialize();
                    bullet.PostInitialize();
                    return bullet;
                }
            }

            return bullet;
        }

    }
    public void BulletDied(Bullet b)
    {
        b.gameObject.SetActive(false);
    }


}
