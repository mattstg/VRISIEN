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
    private Transform target;

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").transform;
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
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit,2f);
        //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    isHit = true;
        //    Debug.Log("Player Hit");
        //    //HitPlayer(); call a function on player
        //}
       if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isHit = true;
            Debug.Log("Wall Hit");
            HitWall(); 
        }
        else
        {
            isHit = false;
        }
    }
    public void FollowPlayer()
    {
        transform.position += Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public void HitWall()
    {
        rb.Deflect();
    }
}
