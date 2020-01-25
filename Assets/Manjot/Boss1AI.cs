using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    Vector3 newPos;

    int randSide;

    public Transform leftSide, rightSide;
    
    public float distanceToMaintain = 10f;
    public float sidewayTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<FakePlayer>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        WalkingAI();
        transform.LookAt(target);
    }

    public void WalkingAI()
    {
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

    }
}
