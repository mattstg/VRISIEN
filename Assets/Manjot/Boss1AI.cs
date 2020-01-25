using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : Enemy, IHittable
{
    enum Abilities {Walk, Shoot, SpecialAttack};

    Transform target;
    NavMeshAgent agent;
    float fireRate = 0.15f;
    float fireRateCount;
    float abilityTimer = 3f;
    float abilityTimeCounter;

    Vector3 newPos;

    int randSide;
    int randAbility;

    public bool specialAtt = false;
    public Transform leftSide, rightSide;
   
    public float distanceToMaintain = 10f;
    public float sidewayTimer = 2f;
    public Transform gunPoint1, gunPoint2;
    public Transform toiletSeat;

    // Start is called before the first frame update
    public void Start()
    {
      //  base.Initialize();
      //  Debug.Log("boss ai");
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FakePlayer>().transform;
        fireRateCount = fireRate;
        abilityTimeCounter = abilityTimer;
    }

    // Update is called once per frame
    public void Update()
    {
       // base.Refresh();
     //   Debug.Log("AA");
       // WalkingAI();
       // Shoots();
        //SpecialAttack();
        RandomAbilities();
        Debug.Log(randAbility);
        Debug.Log(abilityTimeCounter);
    }

    public void RandomAbilities()
    {
        abilityTimeCounter -= Time.deltaTime;
        if(abilityTimeCounter <= 0)
        {
            randAbility = Random.Range(0, 4);
            ResetAbilityTime(randAbility);
        }

        switch (randAbility)
        {
            case 0:
                WalkingAI();
                break;
            case 1:
                Shoots();
                break;
            case 2:
                SpecialAttack();
                break;
            default:
                Shoots();
                break;
        }
    }

    public void WalkingAI()
    {
        transform.LookAt(target);
        //if (!specialAtt)
        //{
           agent.SetDestination(target.position);
       // }
       
    }

    public void Shoots()
    {
        transform.LookAt(target);
        sidewayTimer -= Time.deltaTime;
        if (sidewayTimer <= 0)
        {
            randSide = Random.Range(0, 2);
            sidewayTimer = 2f;

        }
        //if ((transform.position - target.position).sqrMagnitude < Mathf.Pow(distanceToMaintain, 2))
       // {
            switch (randSide)
            {
                case 0:
                    agent.SetDestination(leftSide.position);
                    break;
                case 1:
                    agent.SetDestination(rightSide.position);
                    break;
            }
       // }
        if (fireRateCount < 0)
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

    public void SpecialAttack()
    {
        //if (specialAtt)
       // {
            transform.LookAt(toiletSeat);
            agent.SetDestination(toiletSeat.position);
      // }
    }

    public void Stun()
    {
        throw new System.NotImplementedException();
    }

    public void SwordHit()
    {
        throw new System.NotImplementedException();
    }

    public void ResetAbilityTime(int ability)
    {
        if(ability == 0)
        {
            abilityTimeCounter = 1f;
        }
        else if(ability == 1)
        {
            abilityTimeCounter = 3f;
        }
        else if(ability == 2)
        {
            abilityTimeCounter = 4f;
        }
    }
}
