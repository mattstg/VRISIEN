using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public Transform player;
    Rigidbody rb;
    public bool shoot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))//(OVRInput.Get(OVRInput.RawButton.X))
            shoot = !shoot;
    }

    private void FixedUpdate()
    {
        if (shoot)
           // transform.LookAt(player.transform);
            rb.AddForce(- (transform.position - player.position) * 9f);
    }
}
