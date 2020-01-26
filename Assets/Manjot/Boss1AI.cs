using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : Enemy, IHittable
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    float fireRate = 0.15f;
    float fireRateCount;
    float abilityTimer = 3f;
    float abilityTimeCounter;

    Vector3 newPos;

    int randSide;
    int randAbility = 0;

    public bool specialAtt = false;
    public Transform leftSide, rightSide;
   
    public float distanceToMaintain = 10f;
    public float sidewayTimer = 2f;
    public Transform gunPoint1;
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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        // base.Refresh();
        // WalkAndPunch();
        //  Shoots();
        // SpecialAttack();
         RandomAbilities();
        Debug.Log(randAbility);
    }

    public void RandomAbilities()
    {
        //abilityTimeCounter -= Time.deltaTime;
        //if(abilityTimeCounter <= 0)
        //{
        //    randAbility = Random.Range(0, 4);
        //    abilityTimeCounter = 5f;
        //}

        switch (randAbility)
        {
            case 0:
                WalkAndPunch();
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

    public void WalkAndPunch()
    {
        transform.LookAt(target);
        agent.SetDestination(target.position);
        if (agent.remainingDistance <= agent.stoppingDistance + 4)
        {
            anim.ResetTrigger("Run");
            anim.SetTrigger("Kick");
        }
        else
        {
            anim.SetTrigger("Run");
        }
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
                    anim.SetTrigger("StrafeLeft");
                    break;
                case 1:
                    agent.SetDestination(rightSide.position);
                    anim.SetTrigger("StrafeRight");
                    break;
            }
       // }
        if (fireRateCount < 0)
        {
              ParticlesManager.Instance.CreateParticleEffect(ParticlesManager.ParticleType.MuzzleFlash, gunPoint1, 0.15f);
              BulletManager.Instance.CreateBullet(gunPoint1);
          //  BulletManager.Instance.CreateBullet(gunPoint2);
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
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                transform.LookAt(target);
                anim.SetTrigger("Throw");
            }
            else
            {
                anim.SetTrigger("Run");
                anim.ResetTrigger("Throw");
            }
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

    public void ResetAbil(int ability)
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
    public void ResetAbilityTime()
    {
        randAbility = Random.Range(0, 3);
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Kick");
        anim.ResetTrigger("StrafeLeft");
        anim.ResetTrigger("StrafeRight");
        anim.ResetTrigger("Throw");
        anim.ResetTrigger("FlyingKick");

        //abilityTimeCounter = 1f;
        ////anim.ResetTrigger("Run");
        ////anim.ResetTrigger("Kick");
        ////anim.ResetTrigger("StrafeLeft");
        ////anim.ResetTrigger("StrafeRight");
        ////anim.ResetTrigger("Throw");
        ////anim.ResetTrigger("FlyingKick");
        //anim.SetBool("Runs", false);
        //anim.SetBool("Kicks", false);
        //anim.SetBool("StrafeLefts", false);
        //anim.SetBool("StrafeRights", false);
        //anim.SetBool("Throws", false);
        //anim.SetBool("FlyingKicks", false);
    }
}
