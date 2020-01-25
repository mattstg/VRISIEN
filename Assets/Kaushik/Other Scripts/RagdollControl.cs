using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    Rigidbody[] ragdollBodies;
    public bool isRagdolled;
    Animator anim;
    OVRGrabbable[] grabDoll;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SetupRagdoll();
    }

    // Update is called once per frame
    void Update()
    {      
        
    }
    
    void SetupRagdoll()
    {
        grabDoll = gameObject.GetComponentsInChildren<OVRGrabbable>();
        ragdollBodies = gameObject.GetComponentsInChildren<Rigidbody>();
        DoRagdoll(false);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        isRagdolled = isRagdoll;
        foreach (Rigidbody r in ragdollBodies)
        {
            r.isKinematic = !(isRagdoll);
            r.gameObject.layer = isRagdoll ? LayerMask.NameToLayer("Ragdoll") : LayerMask.NameToLayer("Enemy");
        }
        anim.enabled = !isRagdoll;
        foreach (var g in grabDoll)                      // Include this if grabbable. Can then use body as shield, maybe throw for damage
            g.enabled = isRagdoll;

        
       
    }
}
