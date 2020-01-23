using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform gunPoint;
    public float fireRate = 0.25f;
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

    bool isFoundCover = false;
    bool isInCover = false;
    bool canShoot = true;
    bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coverObjects = GameObject.FindGameObjectsWithTag("CoverLocation");
        nv = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        outlineScript = GetComponent<Outline>();        
    }

    // Update is called once per frame
    void Update()
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
            print("shoot");
            if (canShoot)
            {
                BulletManager.Instance.CreateBullet(gunPoint);
            }
        }  
        //print("Shooting");
    }

    void RotateTowardsPlayer()
    {
        transform.LookAt(player.transform);
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
            Hit();
        }
    }

    public void Hit()
    {
        //throw new System.NotImplementedException();
        print("DamageReceived");
        if(isStunned)
        {
            animController.SetTrigger("Die");
        }
        else
        {
            if (maxHitResistance > 0)
            {
                StartCoroutine(RunOutlineEffect(2f));
                maxHitResistance--;
                animController.SetTrigger("Hit");
            }
            else
                animController.SetTrigger("Die");
        }
    }

    IEnumerator RunOutlineEffect(float time)
    {
        outlineScript.enabled = true;
        yield return new WaitForSeconds(time);
        outlineScript.enabled = false;
    }
}
