using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movechair : MonoBehaviour
{
    Rigidbody rb;
    public GameObject chair;

    [SerializeField]
    [Range(1, 60)]
    public float movementForce, maxVelocity;

    [SerializeField]
    [Range(1, 10)]
    public float boostForce, jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var currentVelocity = rb.velocity.magnitude;
        currentVelocity = Mathf.Clamp(currentVelocity,0, maxVelocity);
        rb.velocity = rb.velocity.normalized * currentVelocity;
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(chair.transform.forward * rb.mass * movementForce *
                (Input.GetKey(KeyCode.LeftShift) ? boostForce : 1));             // Check LeftShift for Boost
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-chair.transform.forward * rb.mass * movementForce *
                (Input.GetKey(KeyCode.LeftShift) ? boostForce : 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-chair.transform.right * rb.mass * movementForce *
                (Input.GetKey(KeyCode.LeftShift) ? boostForce : 1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(chair.transform.right  * rb.mass * movementForce *
                (Input.GetKey(KeyCode.LeftShift) ? boostForce :1));
        }

        if (Input.GetKey(KeyCode.LeftControl));
            // Jump with layers. Depends on layer nomenclature in the future 

    }
}
