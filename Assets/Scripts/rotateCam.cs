using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCam : MonoBehaviour
{
    public float VerticalFOV = 60, horizontalFOV = 120;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newRotation = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        
        newRotation.x = Mathf.Clamp(newRotation.x, -VerticalFOV, VerticalFOV);
        if (newRotation.x > 0)
            newRotation.x = 360 - newRotation.x;

        newRotation.y = Mathf.Clamp(newRotation.y, -horizontalFOV, horizontalFOV);
        if (newRotation.y > 0) 
            newRotation.y = 360 - newRotation.y;

        transform.rotation = Quaternion.Euler(newRotation);
    }
}
