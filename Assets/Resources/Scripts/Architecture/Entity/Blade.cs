using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StunGun
public class Blade : MonoBehaviour
{
    private Transform endPos; // can be the  player's location where the gun will lerp back and forth 
    private Transform startPos;

    OVRGrabbable grabRef;


    public float smoothing = 1f;

    private float cooldownTime = 0; // cool down for GETOVERHERE!!! *experimental*
    private float timer = 1.5f;

    public Vector3 angle;

    bool lerp = false;

    public void Initialize()
    {

        endPos = GameObject.Find("Left").transform;
      //  startPos.position = 
        grabRef = gameObject.GetComponent<OVRGrabbable>();

    }
    public void Refresh()
    {
        cooldownTime += Time.deltaTime;
        if (grabRef.isGrabbed)
        {
            if (grabRef.grabbedByRight)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && cooldownTime > timer)
                {
                    Spear();
                    cooldownTime = 0;
                }
            }
            else
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && cooldownTime > timer)
                {
                    Spear();
                    cooldownTime = 0;
                }
        }

        if (OVRInput.Get(OVRInput.Button.Four))
        {
            StartCoroutine(Lerp());
        }
       
    }
    public void PhysicsRefresh()
    {

    }
    void Spear()
    {

        var go = transform.CheckRaycast();
        if (go)
        {
            StartCoroutine(Lerp());
            StartCoroutine(Lerp());

        }

        // hit.transform.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

    IEnumerator SpearKill(RagdollControl ragdoll)
    {
        ragdoll.DoRagdoll(true);
        yield return new WaitForSeconds(2f);
        //ragdoll.DoRagdoll(false);
    }


    IEnumerator Lerp()
    {
        // needs to be fixed.
        if (Vector3.Distance(transform.position,endPos.position) <=.2f)
        {
            while (Vector3.Distance(transform.position, startPos.position) != startPos.position.sqrMagnitude)
            {
                transform.position = Vector3.Lerp(transform.position, startPos.transform.position, smoothing * Time.deltaTime);
                transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(transform.position, endPos.position) >= .2f)//!= endPos.position.sqrMagnitude)
            {
                transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
                transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);

    }
}
