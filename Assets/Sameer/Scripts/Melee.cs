using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee :Enemy
{
    // Start is called before the first frame update
    public Transform player;
    public Transform head;
    public float enemyHittingRadious = 6f;
    public float playerDetactingRange = 7f;
    Rigidbody rb;
    NavMeshAgent agent;
    Player p;
    float dodgeForce = 30f;
    bool isDodging = false;
    bool isWalking = false;
    bool isChasing = false;
    bool isAttacking = false;
    bool playerDetected = false;



    public override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        p = player.gameObject.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void Refresh()
    {
        dodge2();
        RaycastHit rayForward;
        Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward) * playerDetactingRange, out rayForward);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * playerDetactingRange);

        if (playerDetected)
        {
            chasePlayer();
        }
        else
        {
            agent.SetDestination(new Vector3(Random.Range(0, 15), 1, Random.Range(0, 15)));
        }
        if (rayForward.collider.CompareTag("Player"))//get bool value from player whn raycast on enemy to make enemy dodge whn in target range or whn shooting and add that bool in OR condition
        {
            dodge(transform.position);
        }
        if (Vector3.SqrMagnitude(player.position - transform.position) < enemyHittingRadious)
        {

            //call player's HitByProjectile and do damage
        }


    }
    void chasePlayer()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }
    void dodge(Vector3 oldPos)
    {
        //if (Random.value > 0.5f)
        //    rb.AddForce(-transform.right * dodgeForce);
        //else
        //    rb.AddForce(transform.right * dodgeForce);
        Vector3 newPosition;
        if (Random.value > 0.5f)
        {
            newPosition = new Vector3(oldPos.x + 5, oldPos.y, oldPos.z + 5);
        }
        else
        {
            newPosition = new Vector3(oldPos.x - 5, oldPos.y, oldPos.z - 5);
        }
        transform.position = Vector3.Lerp(oldPos, newPosition, 2f);
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
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(1, 0, 1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(-1, 0, 1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(1, 0, -1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(-1, 0, -1f)) * playerDetactingRange);
    }
   
}