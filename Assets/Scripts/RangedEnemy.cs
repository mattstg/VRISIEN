using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class RangedEnemy : MonoBehaviour,IHittable
{
    public Transform gunPoint;

    GameObject player;
    GameObject[] coverObjects;
    Vector3 coverLocation;
    NavMeshAgent nv;

    bool isFoundCover = false;
    bool isInCover = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coverObjects = GameObject.FindGameObjectsWithTag("CoverLocation");
        nv = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();

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
        for(int i=0;i < coverObjects.Length;i++)
        {
            if (i == 0)
            {
                nearestCoverLocation = coverObjects[i].transform.position;
                nearestCoverDistance = Vector3.Distance(transform.position, coverObjects[i].transform.position);
            }
            else
            {
                if (Vector3.Distance(transform.position, coverObjects[i].transform.position) < nearestCoverDistance)
                {
                    nearestCoverLocation = coverObjects[i].transform.position;
                    nearestCoverDistance = Vector3.Distance(transform.position, coverObjects[i].transform.position);
                }
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
        print("Shooting");
    }

    void RotateTowardsPlayer()
    {
        transform.LookAt(player.transform);
    }

    public void Hit()
    {
        //throw new System.NotImplementedException();
        print("DamageReceived");
    }
}
