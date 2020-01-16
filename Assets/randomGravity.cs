using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGravity : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * .95f);
        if (Input.GetKey(KeyCode.Space))
            rb.AddExplosionForce(10f,Vector3.one,60f);
    }
}
