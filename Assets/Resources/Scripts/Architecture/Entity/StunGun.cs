using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//StunGun
public class StunGun : MonoBehaviour
{
    const float gunDistanceOffset = 0.05f;
    const float smoothLerp = 1f;
    const float maxRadians = Mathf.PI / 4;


    private Transform endPos; // can be the  player's location where the gun will lerp back and forth 
    private Transform startPos;

    OVRGrabbable grabRef;


    public float smoothing = 1f;
    public Transform MuzzlePoint;

    private float cooldownTime = 0; // time difference between shooting of bullets
    private float timer = 0.5f;

    public Vector3 angle;

    bool lerp = false;
    Transform HoldsterSocket;

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
            if (transform.parent == PlayerManager.Instance.player.gunSpot)
            {
                transform.parent = null;
            }

            if (grabRef.grabbedByRight)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && cooldownTime > timer)
                {
                    Shoot();
                    cooldownTime = 0;
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && cooldownTime > timer)
                {
                    print("grabbed1");
                    Shoot();
                    cooldownTime = 0;
                }
            }
        }
        else
        {
            LerpBackToHolster();
        }

     
        /*if (Input.GetKeyDown(KeyCode.K))
        {
       
            BulletManager.Instance.CreateBullet(this.transform);
        }*/
    }
    public void PhysicsRefresh()
    {

    }
    void Shoot()
    {
        ParticlesManager.Instance.CreateParticleEffect(ParticlesManager.ParticleType.StunGunMuzzle, MuzzlePoint, 0.2f);
        print("shoot");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))     // Hey sir, why did you replace my extension function that does EXACTLY this ? :'(
        {
            Enemy e;
            IHittable ih;
            if (e = hit.transform.gameObject.GetComponentInParent<Enemy>())
            {
                StartCoroutine(Stun(e.transform.GetComponentInParent<RagdollControl>()));
            }

            if(hit.transform.GetComponent<IHittable>() != null)
            {
                hit.transform.gameObject.GetComponent<IHittable>().Stun();
            }
        } 
    }

    IEnumerator Stun(RagdollControl ragdoll)
    {
        ragdoll.DoRagdoll(true);
        ragdoll.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(10f);
        ragdoll.GetComponent<NavMeshAgent>().enabled = true;
        ragdoll.DoRagdoll(false);
        
    }


    void LerpBackToHolster()
    {
        if (Vector3.SqrMagnitude(PlayerManager.Instance.player.gunSpot.position - transform.position) > gunDistanceOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerManager.Instance.player.gunSpot.position, smoothLerp * Time.deltaTime);
            transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, PlayerManager.Instance.player.gunSpot.eulerAngles, 2, 1);
        }
        else if(transform.parent != PlayerManager.Instance.player.gunSpot)
        {
            transform.SetParent(PlayerManager.Instance.player.gunSpot);
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
