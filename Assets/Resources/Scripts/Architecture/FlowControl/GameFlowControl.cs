﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowControl
{
    #region Singleton
    private static GameFlowControl instance = null;

    private GameFlowControl() {}

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
        //GameLinks.gl = GameObject.FindObjectOfType<GameLinks>();
        //InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        VRCameraManager.Instance.Initialize();
        //EnemyManager.Instance.Initialize();
    }

    public void PostInitialize()
    {
        //InputManager.Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        VRCameraManager.Instance.PostInitialize();
        //EnemyManager.Instance.PostInitialize();

    }

    public void PhysicsRefresh()
    {
        //InputManager.Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh();
        VRCameraManager.Instance.PhysicsRefresh();
        //EnemyManager.Instance.PhysicsRefresh();

    }

    public void Refresh()
    {
        //InputManager.Instance .Refresh();
        PlayerManager.Instance.Refresh();
        VRCameraManager.Instance.Refresh();
        //EnemyManager.Instance.Refresh();

    }

    
}