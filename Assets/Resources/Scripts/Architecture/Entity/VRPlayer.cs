using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A subclass of OVRPlayerController, can be used to add custom code of VRplayer, if any
public class VRPlayer : OVRPlayerController, IManagable
{
    [HideInInspector]
    public float slowMoActiveTime = 3;
    float boostActiveTime = 2;

    float boostCooldown = 0;
    float boostTimer = 0;
    float slowmoCooldown = 5;
    float slowmoTimer = 0;

    //[HideInInspector]
    //public RootMotion.FinalIK.VRIK playerModelIK;

    public void PhysicsRefresh()
    {
        
    }

    override public void PostInitialize()
    {
        base.PostInitialize();

        //playerModelIK = gameObject.GetComponentInChildren<RootMotion.FinalIK.VRIK>();
        //playerModelIK.solver.spine.minHeadHeight = playerModelIK.solver.
    }

    override public void Initialize()
    {
        base.Initialize();
    }

    override public void Refresh()
    {
        base.Refresh();

        PlayerSpecialPowers();
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

    void PlayerSpecialPowers()
    {
        // Slow Mo
        slowmoTimer += Time.deltaTime;
        if (slowmoTimer >= slowmoCooldown)
        {
            if (OVRInput.GetDown(OVRInput.Button.Four))
            { 
                Time.timeScale = .25f;
                slowmoTimer = 0;
            }
        }
        if(slowmoTimer >= slowMoActiveTime)
            Time.timeScale = 1;

        // Boost
        /*boostTimer += Time.deltaTime;
        if (boostTimer >= boostCooldown)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                Acceleration += .5f;
                boostTimer = 0;
            }
        }
        if (boostTimer >= boostActiveTime)
            Acceleration -= .5f;
            */
    }
}
