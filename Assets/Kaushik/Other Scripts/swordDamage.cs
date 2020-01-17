using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordDamage : MonoBehaviour
{
    public Rigidbody rb;
    public float velocity, angularVelocity;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        velocity = rb.velocity.magnitude;
        angularVelocity = rb.angularVelocity.magnitude;
    }
}
