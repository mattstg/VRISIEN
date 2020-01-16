using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform head;
    public float enemyHittingRadious = 6f;
    public float playerDetactingRange = 7f;
    Rigidbody rb;
    NavMeshAgent agent;
    Player p;
    float dodgeForce=30f;
    
    void Start()
    {
        p = player.gameObject.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit rayForward;
        Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward) * playerDetactingRange, out rayForward);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * playerDetactingRange);
        
        if (p.isShooting() && rayForward.collider.CompareTag("Player") )//get bool value from player whn raycast on enemy to make enemy dodge whn in target range and add that bool in OR condition
        {
            dodge();
        }
        if (Vector3.SqrMagnitude(player.position - transform.position) < enemyHittingRadious)
        {
            doDamage();
        }
        
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }
    void dodge()
    {
        if (Random.value > 0.5f)
            rb.AddForce(-transform.right * dodgeForce);
        else
            rb.AddForce(transform.right * dodgeForce);
    }
    void dodge2()
    {
        RaycastHit rayBack;
        RaycastHit rayLeft;
        RaycastHit rayRight;
        RaycastHit rayForwardLeft;
        RaycastHit rayForwardRight;
        RaycastHit rayBackLeft;
        RaycastHit rayBackRight;

        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayBack);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayRight);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayForwardLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayForwardRight);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayBackLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayBackRight);

        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.right) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.right) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, 0.45f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, -0.45f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, 0.45f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, -0.45f)) * playerDetactingRange);
    }
    void doDamage()
    {
        Debug.Log("Hitting Enemy");
    }
}
