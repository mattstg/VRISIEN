﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowControl
{
    #region Singleton
    private static GameFlowControl instance = null;

    private GameFlowControl() { }

    public static GameFlowControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameFlowControl();
            }
            return instance;
        }
    }

    #endregion


    public void Initialize()
    {
        
        //InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        VRCameraManager.Instance.Initialize();
        BulletManager.Instance.Initialize();
        WeaponManager.Instance.Initialize();
        //SoundManager.Instance.Initialize();
        CollectableManager.Instance.Initialize();
        ParticlesManager.Instance.Initialize();
        GameSetupClass.Instance.Initialize();
        EnemyManager.Instance.Initialize();
    }

    public void PostInitialize()
    {
        //InputManager.Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        VRCameraManager.Instance.PostInitialize();
        BulletManager.Instance.PostInitialize();
        WeaponManager.Instance.PostInitialize();
        // SoundManager.Instance.PostInitialize();
        CollectableManager.Instance.PostInitialize();
        GameSetupClass.Instance.PostInitialize();
        EnemyManager.Instance.PostInitialize();

    }

    public void PhysicsRefresh()
    {
        //InputManager.Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh();
        VRCameraManager.Instance.PhysicsRefresh();
        BulletManager.Instance.PhysicsRefresh();
        WeaponManager.Instance.PhysicsRefresh();
        // SoundManager.Instance.PhysicsRefresh();
        CollectableManager.Instance.PhysicsRefresh();
        GameSetupClass.Instance.PhysicsRefresh();
        EnemyManager.Instance.PhysicsRefresh();

    }

    public void Refresh(float DeltaTime)
    {
        GameSetupClass.Instance.Refresh(DeltaTime);
        //InputManager.Instance .Refresh();
        PlayerManager.Instance.Refresh();
        VRCameraManager.Instance.Refresh();
        BulletManager.Instance.Refresh();
        WeaponManager.Instance.Refresh();
        CollectableManager.Instance.Refresh();
        EnemyManager.Instance.Refresh();

    }


}
