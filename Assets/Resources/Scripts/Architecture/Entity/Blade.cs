using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StunGun
public class Blade : MonoBehaviour
{
    private Transform endPos; // can be the  player's location where the gun will lerp back and forth 
    private Transform startPos;
    private Transform holsterPos;

    OVRGrabbable grabRef;


    public float smoothing = 1f;

    private float cooldownTime = 0; // cool down for GETOVERHERE!!! *experimental*
    private float timer = 1.5f;
    public float damageAmount;

    public bool isSpecial;

    public Vector3 angle;

    bool lerp = false;

    public void Initialize()
    {

      //  endPos = GameObject.Find("Left").transform;
      //  startPos.position = 
        grabRef = gameObject.GetComponent<OVRGrabbable>();
        if (gameObject.layer == LayerMask.NameToLayer("Collectables"))
        {
            isSpecial = true;
            damageAmount = 100f;
            holsterPos = PlayerManager.Instance.player.specialSpot;
        }
        else
        {
            isSpecial = false;
            damageAmount = 34f;
            holsterPos = PlayerManager.Instance.player.swordSpot;
        }
        

    }
    public void Refresh()
    {
        cooldownTime += Time.deltaTime;
        if (grabRef.isGrabbed)
        {
            transform.SetParent(null);
            if (grabRef.grabbedByRight)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && cooldownTime > timer)
                {
               //     Spear();
                    cooldownTime = 0;
                }
            }
            else
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && cooldownTime > timer)
            {
                //Spear();
                cooldownTime = 0;
            }
        }

        else
            LerpBackToHolster();

        //if (OVRInput.Get(OVRInput.Button.Four))
        //{
        //   // StartCoroutine(Lerp());
        //}
       
    }
    public void PhysicsRefresh()
    {

    }
    void Spear()
    {

        var go = transform.CheckRaycast();
        if (go)
        {
            endPos = go.transform; 
            startPos = GameObject.Find("Left").transform;
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
            while (Vector3.Distance(transform.position, startPos.position) >= .05f) //!= startPos.position.sqrMagnitude)
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

    void LerpBackToHolster()
    {
        if (Vector3.SqrMagnitude(holsterPos.position - transform.position) > 0.15f)
        {
            transform.position = Vector3.MoveTowards(transform.position, holsterPos.position, 2.5f * Time.deltaTime);
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, holsterPos.eulerAngles, 2, 1);
        }
        else if (transform.parent != holsterPos)
        {
            transform.SetParent(holsterPos);
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }


        //while (Vector3.Distance(transform.position, endPos.position) >=.25f)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, endPos.transform.position, smoothing * Time.deltaTime);
        //        transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, angle, Time.deltaTime * smoothing);

        //        yield return null;
        //    }      

        //yield return new WaitForSeconds(1f);

    }
}
