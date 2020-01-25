using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1AI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    Vector3 newPos;

    public float SidewaySpeed = 10f;
    public float distanceToMaintain = 10f;
    public float sidewayFloat = 1f;
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
        if ((transform.position - target.position).sqrMagnitude < Mathf.Pow(distanceToMaintain, 2))
        {
            // Debug.Log("walking Sideways");
            //transform.position -= new Vector3(sidewayFloat , transform.position.y, sidewayFloat) * SidewaySpeed * Time.deltaTime;
            // agent.isStopped = true;
            agent.SetDestination(transform.position + new Vector3(sidewayFloat, transform.position.y, sidewayFloat));
        }
        else
        {
            //agent.isStopped = false;
            agent.SetDestination(target.position);
        }   
    }

    public void Shoots()
    {

    }
}
