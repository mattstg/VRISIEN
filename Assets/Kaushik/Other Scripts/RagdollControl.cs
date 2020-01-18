using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    Rigidbody[] ragdollBodies;
    public bool ragdollToggle;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ragdollBodies = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ragdollToggle = !ragdollToggle;

        foreach (Rigidbody r in ragdollBodies)
            r.isKinematic = !(ragdollToggle);
        anim.enabled = !ragdollToggle;

        
    }
}
