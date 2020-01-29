using System.Collections;
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
        GameSetup.gs = GameObject.FindObjectOfType<GameSetup>();

        //InputManager.Instance.Initialize();
        PlayerManager.Instance.Initialize();
        VRCameraManager.Instance.Initialize();
        BulletManager.Instance.Initialize();
        WeaponManager.Instance.Initialize();
        SoundManager.Instance.Initialize();
        CollectableManager.Instance.Initialize();
        ParticlesManager.Instance.Initialize();
        GameSetup.gs.Initialize();
        EnemyManager.Instance.Initialize();
    }

    public void PostInitialize()
    {
        //InputManager.Instance.PostInitialize();
        PlayerManager.Instance.PostInitialize();
        VRCameraManager.Instance.PostInitialize();
        BulletManager.Instance.PostInitialize();
        WeaponManager.Instance.PostInitialize();
        CollectableManager.Instance.PostInitialize();
        GameSetup.gs.PostInitialize();
        EnemyManager.Instance.PostInitialize();

    }

    public void PhysicsRefresh()
    {
        //InputManager.Instance.PhysicsRefresh();
        PlayerManager.Instance.PhysicsRefresh();
        VRCameraManager.Instance.PhysicsRefresh();
        BulletManager.Instance.PhysicsRefresh();
        WeaponManager.Instance.PhysicsRefresh();
        CollectableManager.Instance.PhysicsRefresh();
        GameSetup.gs.PhysicsRefresh();
        EnemyManager.Instance.PhysicsRefresh();

    }

    public void Refresh()
    {
        GameSetup.gs.Refresh(Time.deltaTime);
        //InputManager.Instance .Refresh();
        PlayerManager.Instance.Refresh();
        VRCameraManager.Instance.Refresh();
        BulletManager.Instance.Refresh();
        WeaponManager.Instance.Refresh();
        CollectableManager.Instance.Refresh();
        EnemyManager.Instance.Refresh();

    }


}
