using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StunGun
public class StunGun : MonoBehaviour
{
    private Transform endPos; // can be the  player's location where the gun will lerp back and forth 
    private Transform startPos;

    OVRGrabbable grabRef;


    public float smoothing = 1f;

    private float cooldownTime = 0; // time difference between shooting of bullets
    private float timer = 0.5f;

    public Vector3 angle;

    bool lerp = false;

    public void Initialize()
    {

        endPos = GameObject.Find("Right").transform;
        startPos = this.transform;
        grabRef = gameObject.GetComponent<OVRGrabbable>();

    }
    public void Refresh()
    {
        cooldownTime += Time.deltaTime;
        if (grabRef.isGrabbed)
        {
            if (grabRef.grabbedByRight)
            {
                if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && cooldownTime > timer)
                {
                    Shoot();
                    cooldownTime = 0;
                }
            }
            else
                if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) && cooldownTime > timer)
                {
                    Shoot();
                    cooldownTime = 0;
                }
        }

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            StartCoroutine(Lerp());
        }
    }
    public void PhysicsRefresh()
    {

    }
    void Shoot()
    {
        //RaycastHit hit;
        //Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity,1<<LayerMask.NameToLayer("Enemy"));
        var go = transform.CheckRaycast();
        // Debug.DrawRay(transform.position, Vector3.forward, Color.white, .1f);
        if (go)
            StartCoroutine(Stun(go.GetComponent<RagdollControl>()));
        
           // hit.transform.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        
    }

    IEnumerator Stun(RagdollControl ragdoll)
    {
        ragdoll.DoRagdoll(true);
        yield return new WaitForSeconds(2f);
        ragdoll.DoRagdoll(false);
    }


    IEnumerator Lerp()
    {
        // needs to be fixed.
        if (this.transform.position == endPos.position)
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
            while (Vector3.Distance(transform.position, endPos.position) >=.2f)//!= endPos.position.sqrMagnitude)
            {
                transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
                transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

                yield return null;
            }
        }
        yield return new WaitForSeconds(1f);

    }
}
