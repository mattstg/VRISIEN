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
            if (!grabRef.grabbedByRight)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && cooldownTime > timer)
                {
                    Shoot();
                    cooldownTime = 0;
                }
            }
            else
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && cooldownTime > timer)
                {
                    Shoot();
                    cooldownTime = 0;
                }
        }
        else
        {
            StartCoroutine(Lerp());
        }

     
        if (Input.GetKeyDown(KeyCode.K))
        {
       
            BulletManager.Instance.CreateBullet(this.transform);
        }
    }
    public void PhysicsRefresh()
    {

    }
    void Shoot()
    {
    
        var go = transform.CheckRaycast();
        if (go)
        {
            StartCoroutine(Stun(go.transform.root.GetComponent<RagdollControl>()));
        }
        
          
    }

    IEnumerator Stun(RagdollControl ragdoll)
    {
        ragdoll.DoRagdoll(true);
        yield return new WaitForSeconds(2f);
        ragdoll.DoRagdoll(false);
    }


    IEnumerator Lerp()
    {
     
        //while (Vector3.Distance(transform.position, endPos.position) >=.25f)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
        //        transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

        //        yield return null;
        //    }      
        
        yield return new WaitForSeconds(1f);

    }
}
