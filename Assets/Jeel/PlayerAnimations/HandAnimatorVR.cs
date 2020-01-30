using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimatorVR : MonoBehaviour
{
    public bool isRightHand = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRightHand)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                animator.SetBool("isGrab", true);
            }
            if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger) || Input.GetKeyUp(KeyCode.Mouse1))
            {
                animator.SetBool("isGrab", false);
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetBool("isGrab", true);
            }
            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || Input.GetKeyUp(KeyCode.Mouse0))
            {
                animator.SetBool("isGrab", false);
            }
        }
    }
}
