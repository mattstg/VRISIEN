using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    void Awake()
    {
        GameFlowControl.Instance.Initialize();
    }

    void Start()
    {
        GameFlowControl.Instance.PostInitialize();
    }

    void Update()
    {
        GameFlowControl.Instance.Refresh();
    }

    void FixedUpdate()
    {
        GameFlowControl.Instance.PhysicsRefresh();
    }
}
