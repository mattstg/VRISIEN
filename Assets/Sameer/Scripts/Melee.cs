using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee :Enemy
{
    // Start is called before the first frame update
    Transform player;
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
    bool playerDetected = true;
        RaycastHit rayBack;
        RaycastHit rayLeft ;
        RaycastHit rayRight;
        RaycastHit rayForwardLeft;
        RaycastHit rayForwardRight;
        RaycastHit rayBackLeft;
        RaycastHit rayBackRight;

    

    public override void Initialize()
    {
        base.Initialize();
        player = GameObject.FindGameObjectWithTag("Player").transform;
       // p = player.gameObject.GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void Refresh()
    {
        base.Refresh();
                    dodge2(transform.position);
        
        RaycastHit hit;
        Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward) , out hit,playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * playerDetactingRange);

        if (playerDetected)
        {
            
            if (Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward), out hit, playerDetactingRange))//get bool value from player whn raycast on enemy to make enemy dodge whn in target range or whn shooting and add that bool in OR condition
            {
              //  if(hit.collider.CompareTag("Player"))
            }

            if (Vector3.SqrMagnitude(player.position - transform.position) < enemyHittingRadious)
            {

                //call player's HitByProjectile and do damage
            }
            //chasePlayer();
        }
        else
        {
            Debug.Log("RAND WANDER");
            agent.SetDestination(new Vector3(Random.Range(0, 15), 1, Random.Range(0, 15)));
        }
       


    }
    void chasePlayer()
    {
        Debug.Log("Chasing Player");
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
    void dodge2(Vector3 oldPos)
    {
        Debug.Log("dodge2");

        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.right) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.right) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(1, 0, 1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(-1, 0, 1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(1, 0, -1f)) * playerDetactingRange);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(-1, 0, -1f)) * playerDetactingRange);

        
        if (Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * playerDetactingRange, out rayBack))
        {
            if (rayBack.collider.CompareTag("Obstacle"))
            {
                Debug.Log("dodge back:  " + rayBack.collider.gameObject.transform.position);
                agent.SetDestination(rayBack.collider.ClosestPoint(rayBack.collider.gameObject.transform.position));
                //rb.AddForce(rayBack.collider.gameObject.transform.position-oldPos * dodgeForce);
                //transform.position = Vector3.Lerp(oldPos, rayBack.collider.gameObject.transform.position, 2f);
            }
        }
        else if(Physics.Raycast(head.position, transform.TransformDirection(-Vector3.right) * playerDetactingRange, out rayLeft) && rayLeft.collider.CompareTag("Obstacle"))
        {
            //agent.SetDestination(rayLeft.collider.gameObject.transform.position);
            // rb.AddForce(rayLeft.collider.gameObject.transform.position - oldPos * dodgeForce);
            //transform.position = Vector3.Lerp(oldPos, rayBack.collider.gameObject.transform.position, 2f);
            agent.SetDestination(rayLeft.collider.ClosestPoint(rayLeft.collider.gameObject.transform.position));
        }
        else if (Physics.Raycast(head.position, transform.TransformDirection(Vector3.right) * playerDetactingRange, out rayRight) && rayRight.collider.CompareTag("Obstacle"))
        {
            //agent.SetDestination(rayRight.collider.gameObject.transform.position);
            // rb.AddForce(rayRight.collider.gameObject.transform.position - oldPos * dodgeForce);
            //transform.position = Vector3.Lerp(oldPos, rayBack.collider.gameObject.transform.position, 2f);
            agent.SetDestination(rayRight.collider.ClosestPoint(rayRight.collider.gameObject.transform.position));

        }
        else if (Physics.Raycast(head.position, transform.TransformDirection(new Vector3(1, 0, 1f)) * playerDetactingRange, out rayForwardLeft) && rayForwardLeft.collider.CompareTag("Obstacle"))
        {
            //agent.SetDestination(rayForwardLeft.collider.gameObject.transform.position);
            agent.SetDestination(rayForwardLeft.collider.ClosestPoint(rayForwardLeft.collider.gameObject.transform.position));
        }
        else if (Physics.Raycast(head.position, transform.TransformDirection(new Vector3(-1, 0, 1f)) * playerDetactingRange, out rayForwardRight) && rayForwardRight.collider.CompareTag("Obstacle"))
        {
            // agent.SetDestination(rayForwardRight.collider.gameObject.transform.position);
            agent.SetDestination(rayForwardRight.collider.ClosestPoint(rayForwardRight.collider.gameObject.transform.position));
        }
        else if (Physics.Raycast(head.position, transform.TransformDirection(new Vector3(1, 0, -1f)) * playerDetactingRange, out rayBackLeft) && rayBackLeft.collider.CompareTag("Obstacle"))
        {
            // agent.SetDestination(rayBackLeft.collider.gameObject.transform.position);
            agent.SetDestination(rayBackLeft.collider.ClosestPoint(rayBackLeft.collider.gameObject.transform.position));
        }
        else if (Physics.Raycast(head.position, transform.TransformDirection(new Vector3(-1, 0, -1f)) * playerDetactingRange, out rayBackRight) && rayBackRight.collider.CompareTag("Obstacle"))
        {
            // agent.SetDestination(rayBackRight.collider.gameObject.transform.position);
            agent.SetDestination(rayBackRight.collider.ClosestPoint(rayBackRight.collider.gameObject.transform.position));

        }
        else
        {
            transform.RotateAround(transform.position, Vector3.up, 45f);
        }

    }
   
}