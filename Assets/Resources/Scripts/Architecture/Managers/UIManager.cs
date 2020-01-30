using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    #region Singleton
    private static UIManager instance;
    private UIManager() { }
    public static UIManager Instance { get { return instance ?? (instance = new UIManager()); } }
    #endregion

    
    public void Initialize()
    {

    }

    public void Refresh()
    {

    }

    public void PhysicsRefresh()
    {

    }
    
}
