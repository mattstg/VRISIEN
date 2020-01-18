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
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.transform.SetParent(transform);                  // Lmao cheap dismembering let's see. If works, put for threshold velocities
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            other.GetComponent<Rigidbody>().Deflect();
            
        velocity = rb.velocity.magnitude;
        angularVelocity = rb.angularVelocity.magnitude;

        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
    }

    private void OnTriggerExit(Collider other)
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }


    private void OnCollisionEnter(Collision collision)
    {
        var contacts = collision.contacts;
        foreach (var c in contacts)
            rb.AddForce(-c.normal * 2f);
    }
}
