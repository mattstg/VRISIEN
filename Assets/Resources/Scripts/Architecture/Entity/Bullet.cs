using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletLife = 5f;
    Vector3 startPos;
    public bool isHit = false;
    Rigidbody rb;
    float speed = 20f;
    float counter;
    private Transform target;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").transform;
        startPos = transform.forward;
        counter = bulletLife;
        //Debug.Log("Bullet");
    }

    public void PostInitialize()
    {
        
    }

    public void Refresh()
    {
        BulletShootDir(startPos);
        CheckHit();
        //FollowPlayer();
        
        counter -= Time.deltaTime;
        if (counter <= 0) //(counter <= 0 || isHit)
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
    public void CheckHit()
    {
       
    }
    public void FollowPlayer()
    {
        transform.position += Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void HitWall()
    {
        rb.Deflect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            print("Hit Wall");
            isHit = true;
        }
    }
}
