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

    float hp = 200;
    float maxHP = 200;
    float hpRegenTime = 10;
    float hpRegenCooldown = 0;

    [HideInInspector]
    public bool isAlive = true;

    public Transform gunSpot;

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
        RegenerateHP();
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
        boostTimer += Time.deltaTime;
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
    }

    public void TakeDamage(float damage)
    {
        hp = Mathf.Clamp(hp - damage, 0, maxHP);
    }

    void RegenerateHP(float regenerateBy = 10)
    {
        if (hp < maxHP)
        {
            hpRegenCooldown += Time.deltaTime;
            if (hpRegenCooldown >= hpRegenTime)
            {
                hp = Mathf.Clamp(hp + regenerateBy, 0, maxHP);
                hpRegenCooldown = 0;
            }
        }
    }

    public void isDead()
    {
        isAlive = false;
    }
}
