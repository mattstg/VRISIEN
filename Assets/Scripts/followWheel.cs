using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followWheel : MonoBehaviour
{
    public GameObject wheel;
    Quaternion stockRot;
    

    [SerializeField] [Range(1,20)]
    public float turnSensitivity;

    float minRotationX = -22.5f;
    float maxRotationX = 22.5f;

    float rotationX = 0;
    float rotationY = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = wheel.transform.position + Vector3.up * 1.1f;

        Vector3 mouseMovements = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseMovements * turnSensitivity/10);

        if (Input.GetMouseButton(0))
        {
            stockRot = transform.rotation;
            //transform.Rotate(Mathf.Clamp( Vector3.forward * 0.5f,minRotationX,maxRotationX));
            transform.Rotate(Vector3.forward * 0.5f);
               
        }
        if (Input.GetMouseButtonUp(0))
        {
            stockRot.z = 0;
            stockRot.x = 0;
            transform.rotation = stockRot;
            stockRot = transform.rotation;
        }

        if (Input.GetMouseButton(1))
        {
            stockRot = transform.rotation;
            transform.Rotate(-Vector3.forward * 0.5f);
        }
        if (Input.GetMouseButtonUp(1))
        {
            stockRot.z = 0;
            stockRot.x = 0;
            transform.rotation = stockRot;
            stockRot = transform.rotation;
        }
    }
}
