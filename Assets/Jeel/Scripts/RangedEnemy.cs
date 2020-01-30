using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RangedEnemy : Enemy, IHittable
{
    public Transform gunPoint;
    public int maxHealth = 100;
    public float fireRate = 0.25f;
    public int maxAmmo = 10;
    public int maxHitResistance = 3;
    public bool isHit = false;

    GameObject player;
    GameObject[] coverObjects;
    Vector3 coverLocation;

    NavMeshAgent nv;
    Rigidbody rb;
    Animator animController;
    Outline outlineScript;
    float fireRateCounter = 0f;
    int currentAmmoCount;

    bool isFoundCover = false;
    bool isInCover = false;
    bool canShoot = false;
    bool isStunned = false;
    bool isReloading = false;
    bool canReactToDamage = true;
    bool hasAttackSlot = false;
    bool canSeePlayer;
    bool canMove = true;
    bool meleeBool = true;


    // Start is called before the first frame update
    public override void Initialize(float _hp = 100)
    {
        base.Initialize(_hp);
        player = GameObject.FindGameObjectWithTag("Player");
        coverObjects = GameObject.FindGameObjectsWithTag("CoverLocation");
        nv = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        outlineScript = GetComponent<Outline>();
        outlineScript.enabled = false;
        currentAmmoCount = maxAmmo;
    }

    // Update is called once per frame
    public override void Refresh()
    {
        base.Refresh();
        RayCast();
        UpdateAnimations();

        if (!isFoundCover)
        {
            FindCover();
        }
        else
        {
            if (canMove)
            {
                if (!isInCover)
                {
                    MoveToCover();
                }
                else
                {
                    if (animController.enabled)
                    {
                        if (PlayerTooClose())
                        {
                            Melee();
                        }
                        else
                        {
                            ShootPlayer();
                        }
                    }
                }
            }
            else
            {
                nv.ResetPath();
            }
        }
    }

    void RayCast()
    {
        //NOT using raycast for targetting because anymore because it was decides that bullets will be a driven by colliders for "Deflet" functionality
        RaycastHit hit;
        if(Physics.Raycast(gunPoint.transform.position,player.transform.position - gunPoint.transform.position,out hit,Mathf.Infinity))
        {
            if(hit.transform.CompareTag("Player"))
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }

    void FindCover()
    {
        float nearestCoverDistance = 0;
        Vector3 nearestCoverLocation = Vector3.zero;
        bool isCoverAssigned = false;
        if (coverObjects.Length != 0)
        {
            while (!isCoverAssigned)
            {
                int i = Random.Range(0, coverObjects.Length);
                if (!coverObjects[i].GetComponent<CoverSpot>().isCoverSpotOccupied)
                {
                    nearestCoverLocation = coverObjects[i].transform.position;
                    nearestCoverDistance = Vector3.Distance(transform.position, coverObjects[i].transform.position);
                    coverObjects[i].GetComponent<CoverSpot>().isCoverSpotOccupied = true;
                    isCoverAssigned = true;
                }
            }
            coverLocation = nearestCoverLocation;
            isFoundCover = true;
        }
        else
        {
            isFoundCover = false;
        }
     }

    void MoveToCover()
    {
        nv.SetDestination(coverLocation);
    }

    void ShootPlayer()
    {

        RotateTowardsPlayer();

        fireRateCounter += Time.deltaTime;
        if (fireRateCounter > fireRate)
        {
            fireRateCounter = 0f;
            //print("shoot");
            if (canShoot && !isReloading && hasAttackSlot && canSeePlayer)
            {
                if (currentAmmoCount <= 0)
                {
                    Reload();
                }
                else
                {
                    SoundManager.Instance.PlaySfx("Ranged_Gun1", gameObject);
                    BulletManager.Instance.CreateBullet(gunPoint);
                    ParticlesManager.Instance.CreateParticleEffect(ParticlesManager.ParticleType.MuzzleFlash, gunPoint, 0.25f);
                    currentAmmoCount--;
                }

            }
            else
            {
                WaitForAttackSlot();
            }
        }  
        //print("Shooting");
    }

    void Melee()
    {
        if(meleeBool)
        {
            meleeBool = false;
            StartCoroutine(MeleeSequence(0.5f));
        }
    }
    public bool PlayerTooClose()
    {
        bool temp = false;
        if(Vector3.SqrMagnitude(player.transform.position - transform.position) < 25f)
        {
            temp = true;
        }
        return temp;
    }

    void RotateTowardsPlayer()
    {
        transform.LookAt(new Vector3(player.transform.position.x,0,player.transform.position.z));
        gunPoint.transform.LookAt(player.transform.position+new Vector3(Random.Range(-2,2), Random.Range(-2, 2), Random.Range(-2, 2)));
    }

    void UpdateAnimations()
    {
        if (nv.velocity != Vector3.zero)
        {
            animController.SetBool("isRunning", true);
            SoundManager.Instance.PlayMusic("Enemy_Footsteps",gameObject);
        }
        else
            animController.SetBool("isRunning", false);

        animController.SetBool("isInCover", isInCover);

        if (isHit)
        {
            isHit = false;
            StartCoroutine(HitReactionSequence(2f));
        }
    }

    void WaitForAttackSlot()
    {
        hasAttackSlot = EnemyManager.Instance.AssignAttackSlot();
    }

    void ReleaseAttackSlot()
    {
        EnemyManager.Instance.ReleaseAttackSlot();
        hasAttackSlot = false;
    }

    public void ActivatePlayerShoot()
    {
        canShoot = true;
    }

    void Reload()
    {

        SoundManager.Instance.PlaySfx("Gun_Reload",gameObject);
        ReleaseAttackSlot();
        StartCoroutine(ReloadSequence(3f));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CoverLocation"))
        {
            isInCover = true;
        }
    }

    IEnumerator HitReactionSequence(float time)
    {
        outlineScript.enabled = true;
        canMove = false;
        canShoot = false;
        animController.SetTrigger("Hit");
        yield return new WaitForSeconds(time);
        canShoot = true;
        outlineScript.enabled = false;
        canMove = true;
    }

    IEnumerator MeleeSequence(float time)
    {
        animController.SetTrigger("Melee");
        yield return new WaitForSeconds(time);
        PlayerManager.Instance.player.TakeDamage(200f);
    }

    IEnumerator ReloadSequence(float time)
    {
        isReloading = true;
        animController.SetBool("isReloading", true);
        print("isreloading");
        yield return new WaitForSeconds(time);
        currentAmmoCount = maxAmmo;
        animController.SetBool("isReloading", false);
        isReloading = false;
    }

    IEnumerator DeathSequence(float time)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        canReactToDamage = false;
        //Activate RagDoll
        animController.SetTrigger("Die");
        yield return new WaitForSeconds(time);
        //Remove from EnemyManager
    }

    void IHittable.Stun()
    {
        //throw new System.NotImplementedException();
        isStunned = true;
        StartCoroutine(HitReactionSequence(2f));
    }

    void IHittable.SwordHit()
    {
        // throw new System.NotImplementedException();
        if (canReactToDamage)
        {
            if (isStunned)
            {
                StartCoroutine(DeathSequence(5f));
            }
            else
            {
                if(maxHitResistance > 0)
                {
                    maxHitResistance--;
                    StartCoroutine(HitReactionSequence(2f));
                }
                else
                {
                    StartCoroutine(DeathSequence(5f));
                }              
            }
        }
    }

    void IHittable.ApplyDamage(int damageAmt)
    {
        maxHealth -= damageAmt;
        StartCoroutine(HitReactionSequence(2f));
        PlayerManager.Instance.player.TakeDamage(200f);
    }
}
