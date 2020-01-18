using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    Rigidbody[] ragdollBodies;
    public bool ragdollToggle;
    Animator anim;
    OVRGrabbable[] grabDoll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        grabDoll = gameObject.GetComponentsInChildren<OVRGrabbable>();
        ragdollBodies = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ragdollToggle = !ragdollToggle;

        foreach (Rigidbody r in ragdollBodies)
            r.isKinematic = !(ragdollToggle);           // Put this part into a function called Ragdoll Toggle, call it from stungun on timer
        anim.enabled = !ragdollToggle;
        foreach(var g in grabDoll)                      // Include this if grabbable. Can then use body as shield, maybe throw for damage
            g.enabled = ragdollToggle;
        
    }
}
