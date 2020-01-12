using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform gunPoint;

    GameObject player;

    bool isInCover = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        SetRotation();
    }

    void RayCast()
    {
        Debug.DrawLine(gunPoint.transform.position, player.transform.position,Color.blue);
    }

    void SetRotation()
    {
        if(isInCover)
        {
            transform.LookAt(player.transform.position);
        }
    }
}
