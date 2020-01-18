using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public float grabRadius;
    Vector3 initialPos, rootPos;
    OVRGrabbable grab;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        rootPos = transform.root.position;
        grabRadius = Vector3.Distance(initialPos,rootPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.SqrMagnitude(transform.position - rootPos) < Mathf.Pow(grabRadius * 0.95f, 2) ||
            Vector3.SqrMagnitude(transform.position - rootPos) > Mathf.Pow(grabRadius * 1.15f, 2))
        {
            grab.ReleaseObject();
            transform.position = initialPos;
        }
    }
}
