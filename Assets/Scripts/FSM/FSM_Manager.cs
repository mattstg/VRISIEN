using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FSM_Manager : MonoBehaviour
{
    public GameObject[] coverLocations;
    public Vector3 targetCoverLocation;
    public Vector3 moveToLocation;
    public float minDistance;
    public NavMeshAgent navmesh;
    private void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
    }
}
