using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool shooting = false;
    public Transform enemy;
    public Transform head;
    void Start()
    {
        
    }
    public bool isShooting()
    {
        return shooting;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(enemy);
        RaycastHit ray;
        Physics.Raycast(head.position, transform.TransformDirection(Vector3.forward) * 7, out ray);
        Debug.DrawRay(head.position, transform.TransformDirection(Vector3.forward) * 7);

    }
}
