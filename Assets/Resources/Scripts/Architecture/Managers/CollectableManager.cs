using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager
{

    #region Singleton
    private static CollectableManager instance;
    private CollectableManager() { }
    public static CollectableManager Instance { get { return instance ?? (instance = new CollectableManager()); } }
    #endregion

    public void Initialize()
    {

    }

    public void PostInitialize()
    {

    }

    public void PhysicsRefresh()
    {

    }

    public void Refresh()
    {
    }



}

