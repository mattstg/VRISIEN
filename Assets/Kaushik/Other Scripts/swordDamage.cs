using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordDamage : MonoBehaviour
{
    public Rigidbody rb;
    public OVRGrabbable hiltGrabber;
    public GameObject gushingBlood;
    public float velocity, angularVelocity;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
         //   transform.parent.SetParent(other.transform);                  // Stick sword in enemy. Reverse setting of parent to 'dismember' enemy
         //   transform.localPosition = Vector3.zero;
         //   hiltGrabber.ReleaseObject();

            var blood = GameObject.Instantiate(gushingBlood, other.transform);
            blood.transform.localPosition = Vector3.zero;

            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            other.GetComponent<Rigidbody>().Deflect();
            
        velocity = rb.velocity.magnitude;
        angularVelocity = rb.angularVelocity.magnitude;

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
