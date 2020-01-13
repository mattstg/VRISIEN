using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A subclass of OVRPlayerController, can be used to add custom code of VRplayer, if any
public class VRPlayer : OVRPlayerController, IManagable
{
    public void PhysicsRefresh()
    {
        
    }

    override public void PostInitialize()
    {
        base.PostInitialize();
    }

    override public void Initialize()
    {
        base.Initialize();
    }

    override public void Refresh()
    {
        base.Refresh();
    }

    /*
    void Start() {
        PostInitialize();
    }

    void Update()
    {
        Refresh();
    }

    void Awake()
    {
        Initialize();
    }
    */
}
