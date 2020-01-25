using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    void Awake()
    {
        GameFlowControl.Instance.Initialize();
        gameObject.GetComponent<GameSetup>().Initialize();
    }

    void Start()
    {
        GameFlowControl.Instance.PostInitialize();
    }

    void Update()
    {
        GameFlowControl.Instance.Refresh(Time.deltaTime);
        gameObject.GetComponent<GameSetup>().Refresh(Time.deltaTime);
    }

    void FixedUpdate()
    {
        GameFlowControl.Instance.PhysicsRefresh();
    }
}
