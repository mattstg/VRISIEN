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
        if(other.gameObject.layer==LayerMask.NameToLayer("Enemy"))
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        else
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        velocity = rb.velocity.magnitude;
        angularVelocity = rb.angularVelocity.magnitude;
    }
}
