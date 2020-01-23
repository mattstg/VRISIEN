using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, IHittable
{
    public Transform gunPoint;
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
    bool canShoot = true;
    bool isStunned = false;
    bool isReloading = false;
    bool canReactToDamage = true;

    // Start is called before the first frame update
    public void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coverObjects = GameObject.FindGameObjectsWithTag("CoverLocation");
        nv = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        outlineScript = GetComponent<Outline>();
        currentAmmoCount = maxAmmo;
    }

    // Update is called once per frame
    public void Refresh()
    {
        RayCast();
        UpdateAnimations();

        if (!isFoundCover)
            FindCover();
        if (isFoundCover)
            if (!isInCover)
                MoveToCover();
            else
                ShootPlayer();
    }

    void RayCast()
    {
        Debug.DrawLine(gunPoint.transform.position, player.transform.position,Color.blue);
    }

    void FindCover()
    {
        float nearestCoverDistance = 0;
        Vector3 nearestCoverLocation = Vector3.zero;
        bool isCoverAssigned = false;
        while (!isCoverAssigned)
        {
            int i = Random.Range(0, coverObjects.Length);
            if(!coverObjects[i].GetComponent<CoverSpot>().isCoverSpotOccupied)
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

    void MoveToCover()
    {
        if (Vector3.Distance(transform.position, coverLocation) > 1f)
            nv.SetDestination(coverLocation);
        else
            isInCover = true;
    }

    void ShootPlayer()
    {
        RotateTowardsPlayer();

        fireRateCounter += Time.deltaTime;
        if (fireRateCounter > fireRate)
        {
            fireRateCounter = 0f;
            //print("shoot");
            if (canShoot && !isReloading)
            {
                if (currentAmmoCount <= 0)
                {
                    Reload();
                }
                else
                {
                    BulletManager.Instance.CreateBullet(gunPoint);
                    currentAmmoCount--;
                }

            }
        }  
        //print("Shooting");
    }

    void RotateTowardsPlayer()
    {
        transform.LookAt(player.transform);
        gunPoint.transform.LookAt(player.transform);
    }

    void UpdateAnimations()
    {
        if (nv.velocity != Vector3.zero)
            animController.SetBool("isRunning", true);
        else
            animController.SetBool("isRunning", false);

        animController.SetBool("isInCover", isInCover);

        if (isHit)
        {
            isHit = false;
            StartCoroutine(HitReactionSequence(2f));
        }
    }

    void Reload()
    {
        StartCoroutine(ReloadSequence(3f));
    }

    IEnumerator HitReactionSequence(float time)
    {
        outlineScript.enabled = true;
        canShoot = false;
        animController.SetTrigger("Hit");
        yield return new WaitForSeconds(time);
        canShoot = true;
        outlineScript.enabled = false;
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
}
