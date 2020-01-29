using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee :Enemy
{
    // Start is called before the first frame update
    //Transform player;
    //public Transform head;
    //public float enemyHittingRadious = 6f;
    //public float playerDetactingRange = 7f;
    //Rigidbody rb;
    //NavMeshAgent agent;
    //Player p;
    //bool isDodging = false;
    //bool isWalking = false;
    //bool isChasing = false;
    //bool isAttacking = false;
    //bool playerDetected = true;
    //    RaycastHit rayBack;
    //    RaycastHit rayLeft ;
    //    RaycastHit rayRight;
    //    RaycastHit rayForwardLeft;
    //    RaycastHit rayForwardRight;
    //    RaycastHit rayBackLeft;
    //    RaycastHit rayBackRight;
    
    
    
    

    GameObject player;
    Transform WalkablePoints;
    GameObject[] coverObjects;
    Vector3 coverLocation;
    //Animator anim;
    NavMeshAgent nv;
    Rigidbody rb;
    Animator animController;
    Outline outlineScript;
    float fireRateCounter = 0f;
    int currentAmmoCount;
    public float dodgeForce = 30f;
    public float PlayerDetectionRange = 100f;
    public float playerInAttackRange = 16f;
    public float doDamage = 10f;
    Transform[] allWalkablePoints;
    Transform randomObject ; 
    Vector3 newDodgePos = new Vector3();

    bool hasNewDodgePose = false;
    bool hasDodged = false;
    float afterDodgeTime = 0f;
    float attackTimer = 0f;
    bool hasWanderPoint = false;
    bool playerInRange = false;
    bool isInAttackingRange = false;
    bool isBeingAimedAt = false;
    bool isBeingShootAt = false;
    public bool isTassered = false;
    bool isEnemyAlive = true;


    public override void Initialize(float _hp = 100)
    {
        base.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");
        nv = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        outlineScript = GetComponent<Outline>();
        WalkablePoints = GameObject.FindGameObjectWithTag("WalkPoints").transform;       
        allWalkablePoints= WalkablePoints.GetComponentsInChildren<Transform>();

    }
    public override void Refresh()
    {
        base.Refresh();
        isEnemyAlive=this.isAlive;
        if(Vector3.SqrMagnitude(transform.position-player.transform.position)<PlayerDetectionRange)
        {
            playerInRange = true;
            animController.SetBool("playerInRange", true);
            if (Vector3.SqrMagnitude(transform.position - player.transform.position) < playerInAttackRange)
            {
                isInAttackingRange = true;
                animController.SetBool("isInAttackingRange", true);

            }
            else
            {
                isInAttackingRange = false;
                animController.SetBool("isInAttackingRange", false);

            }
            isBeingAimedAt = true; //get Latest Values from Player Script
            isBeingShootAt = false;//get Latest Values from Player Script
            isTassered = false;    //get Latest Values from Player Script
        }
        else
        {
            playerInRange = false;
            animController.SetBool("playerInRange", false);

        }
        Brain();
    }    
    public void Brain()
    {
        if (playerInRange)
        {
            ChasePlayer();
            Debug.Log("ChasePlayer");
            if (isInAttackingRange)
            {
                Attack();
                Debug.Log("AttackPlayer");
            }
            if (isBeingAimedAt || isBeingShootAt)
            {
                if (!hasDodged)
                {
                    Dodge();
                    hasDodged = true;
                }
                else
                {
                    afterDodgeTime += Time.deltaTime;
                    if(afterDodgeTime>=3f)
                    {
                        hasDodged = false;
                        afterDodgeTime = 0f;
                    }
                }
            Debug.Log("DodgePlayer");
            }
            if (isTassered)
            {
                tassered();
                Debug.Log("TasserPlayer");
            }
        }
        else if (!isEnemyAlive)
        {
            Debug.Log("DeathPlayer");
            Death();
        }
        else
        {
            Wander();
            Debug.Log("WanderPlayer");
        }
    }       
    void ChasePlayer()
    {
       // animController.SetBool("playerInRange", true);
        Debug.Log("Chasing Player");
        Vector3 toLookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(toLookAt);
        nv.SetDestination(player.transform.position);
    }
    void Attack()
    {
        attackTimer+=Time.deltaTime;
        Vector3 toLookAt = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(toLookAt);
        Debug.Log("Attacking Player");
        if (attackTimer > 3f)
        {
           // animController.SetBool("playerInRange", true);
            PlayerManager.Instance.player.TakeDamage(20f);
            SoundManager.Instance.PlayMusic("SwordHit", gameObject);
            attackTimer = 0;
        }
        else
        {
           // animController.SetBool("playerInRange", false);
        }
        //call the attack function of player Script
    }
    void Dodge()
    {
        if (!hasNewDodgePose)
        {
            if (Random.value > 0.5f)
                newDodgePos = -transform.right * dodgeForce;
            else
                newDodgePos = transform.right * dodgeForce;
            hasNewDodgePose = true;
        }

        rb.AddForce(newDodgePos);
        transform.LookAt(newDodgePos);
       

        if (Vector3.SqrMagnitude(transform.position-newDodgePos)<2f)
        {
            hasNewDodgePose = false;
        }
    }
    public void tassered()
    {

    }
    public void Death()
    {

    }
    public void Wander()
    {
        SoundManager.Instance.PlayMusic("Enemy_Footsteps", gameObject);

        ///Debug.Log("1)hasWanderPoint: "+hasWanderPoint);
        if (!hasWanderPoint)
        {
          //  Debug.Log("2) No WanderPoint");
            randomObject = allWalkablePoints[Random.Range(0, allWalkablePoints.Length)];
          //  Debug.Log("3)Got Wander Point: "+ randomObject.position);
            hasWanderPoint = true;
          //  Debug.Log("4)hasWanderPoint: " + hasWanderPoint);
        }
        //Debug.Log("5)hasWanderPoint: " + hasWanderPoint);
        nv.SetDestination(randomObject.position);
      //  Debug.Log("6)DestinationSet: " + randomObject.position);
        if (Vector3.SqrMagnitude(transform.position - randomObject.position)<20f)
        {
            hasWanderPoint = false;
           // Debug.Log("7)hasreachedWanderPoint: ");            
        }

    }    

    //public void Stun()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void SwordHit()
    //{
    //    throw new System.NotImplementedException();
    //}
}