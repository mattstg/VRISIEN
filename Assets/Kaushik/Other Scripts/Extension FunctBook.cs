using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionFunctBook 
{
    public static Transform CheckRaycast(this Transform trans)                  // Call for detections
    {
        if (Physics.Raycast(trans.position, trans.forward, out RaycastHit hit, Mathf.Infinity))
            return hit.transform;
        return null;
    }

    public static void Deflect(this Rigidbody rb)               // Call for blade on bullet collision. Reappropriate later for Reflect
    {
        rb.angularVelocity = Vector3.one;
        rb.velocity = Vector3.zero;
        rb.AddForce(-rb.transform.forward * 1000f);
    }

    public static void Reflect(this Rigidbody rb)               // Call for blade on bullet collision. Reappropriate later for Reflect
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.one;
        rb.AddForce(-rb.transform.forward * 10000f);
    }




}
