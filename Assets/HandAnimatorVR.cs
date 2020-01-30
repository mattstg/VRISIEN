using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimatorVR : MonoBehaviour
{
    OVRGrabbable grabRef;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabRef.grabbedByRight)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                animator.SetBool("isGrab", true);
            }

            if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                animator.SetBool("isGrab", false);
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                animator.SetBool("isGrab", true);
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                animator.SetBool("isGrab", false);
            }
        }
    }
}
