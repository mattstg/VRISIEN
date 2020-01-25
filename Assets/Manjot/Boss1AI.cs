using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : Enemy, IHittable
{
    Transform target;
    NavMeshAgent agent;
    float fireRate = 0.15f;
    float fireRateCount;

    Vector3 newPos;

    int randSide;

    public Transform leftSide, rightSide;
    
    public float distanceToMaintain = 10f;
    public float sidewayTimer = 2f;
    public Transform gunPoint1, gunPoint2;

    // Start is called before the first frame update
    public void Start()
    {
      //  base.Initialize();
      //  Debug.Log("boss ai");
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FakePlayer>().transform;
        fireRateCount = fireRate;
    }

    // Update is called once per frame
    public void Update()
    {
       // base.Refresh();
     //   Debug.Log("AA");
        WalkingAI();
        Shoots();
    }

    public void WalkingAI()
    {
        transform.LookAt(target);
        sidewayTimer -= Time.deltaTime;
        if(sidewayTimer <= 0)
        {
            randSide = Random.Range(0, 2);
            sidewayTimer = 2f;
           
        }
        if ((transform.position - target.position).sqrMagnitude < Mathf.Pow(distanceToMaintain, 2))
        {
            switch (randSide)
            {
                case 0:
                    agent.SetDestination(leftSide.position);
                    break;
                case 1:
                    agent.SetDestination(rightSide.position);
                    break;
                
            }
        }
        else
        {
            agent.SetDestination(target.position);
        }   
    }

    public void Shoots()
    {
        if(fireRateCount < 0)
        {
            BulletManager.Instance.CreateBullet(gunPoint1);
            BulletManager.Instance.CreateBullet(gunPoint2);
            fireRateCount = fireRate;
        }
        else
        {
            fireRateCount -= Time.deltaTime;
        }
        
    }

    public void Stun()
    {
        throw new System.NotImplementedException();
    }

    public void SwordHit()
    {
        throw new System.NotImplementedException();
    }
}
