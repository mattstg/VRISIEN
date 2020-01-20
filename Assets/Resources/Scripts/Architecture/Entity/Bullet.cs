using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletLife = 5f;
    Vector3 startPos;
    public bool isHit = false;
    Rigidbody rb;
    float speed = 5f;
    float counter;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.forward;
        counter = bulletLife;
        Debug.Log("Bullet");
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh()
    {
        BulletShootDir(startPos);
        checkHit();
        // check for WALL OR ENEMY
        counter -= Time.deltaTime;
        if (counter <= 0 || isHit)
        {
            BulletManager.Instance.BulletDied(this.gameObject.GetComponent<Bullet>());
            counter = bulletLife;
        }
    }

    public void PhysicsRefresh()
    {

    }
    public void BulletShootDir(Vector3 shootDir)
    {
        transform.position += shootDir.normalized * speed * Time.deltaTime;
    }
    public void checkHit()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
    }
}
