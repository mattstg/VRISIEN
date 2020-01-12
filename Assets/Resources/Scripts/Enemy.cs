using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform head;
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
        Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward) * 7, out rayForward);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * 7);
        
        if (p.isShooting() && rayForward.collider.CompareTag("Player"))
        {
            dodge();
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

        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayBack);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayRight);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayForwardLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayForwardRight);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayBackLeft);
        Physics.Raycast(head.position, transform.TransformDirection(-Vector3.forward) * 7, out rayBackRight);

        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.forward) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(-Vector3.right) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.right) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, 0.45f)) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, -0.45f)) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, 0.45f)) * 7);
        Debug.DrawRay(head.position, transform.TransformDirection(new Vector3(0, 0, -0.45f)) * 7);
    }
}
