using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : Enemy ,IHittable
{
    public Transform target;
    NavMeshAgent agent;
    Animator anim;
    float fireRate = 0.15f;
    float fireRateCount;
    float abilityTimer = 3f;
    float abilityTimeCounter;
    Vector3 newPos;

    int randSide;
    public bool abilityDone = false;
    public int randAbility = 0;

    public bool specialAtt = false;
    public Transform leftSide, rightSide;
   
    public float distanceToMaintain = 10f;
    public float sidewayTimer = 2f;
    public Transform gunPoint1;
    public Transform toiletSeat;
   // public float hp = 300;
    // Start is called before the first frame update
    public override void Initialize(float _hp = 300)
   //public void Start()
    {
        base.Initialize(_hp);
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.Instance.player.transform;
        fireRateCount = fireRate;
        abilityTimeCounter = abilityTimer;
        anim = GetComponent<Animator>();
        Debug.Log("HP is : " + hp);
    }
   
         


    public override void Refresh()
    {
        Debug.Log("HP is : " + hp);
        
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
                RunAndKick();
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

    public void RunAndKick()
    {
        agent.SetDestination(target.position);
        //if (agent.remainingDistance <= agent.stoppingDistance + 4)
        if((transform.position - target.position).sqrMagnitude <= 70)
        {
            anim.SetTrigger("Kick");
        }
    }

    public void Shoots()
    {
        transform.LookAt(new Vector3(target.position.x, 0, target.position.z));
        gunPoint1.LookAt(target);
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
        agent.SetDestination(toiletSeat.position);
        //if(agent.remainingDistance <= agent.stoppingDistance)
        if ((transform.position - toiletSeat.position).sqrMagnitude <= 20)
        {
            transform.LookAt(target);
            anim.SetTrigger("Throw");
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
    
    public void AbilityComplete()
    {
        abilityDone = true;
    }
    public void ApplyDamage(int damage)
    {
        hp -= damage;
    }
}
