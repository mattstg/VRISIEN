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
        grab = GetComponent<OVRGrabbable>();
        grabRadius = Vector3.Distance(initialPos,rootPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.SqrMagnitude(transform.position - rootPos) < Mathf.Pow(grabRadius * 0.75f, 2) ||
            Vector3.SqrMagnitude(transform.position - rootPos) > Mathf.Pow(grabRadius * 1.25f, 2))
        {
            grab.ReleaseObject();
          //  rootPos = transform.root.position;                                                 Test Repositioning after Testing at least Once
          //  transform.position = (rootPos - transform.position).normalized * grabRadius;
        }
    }
}
