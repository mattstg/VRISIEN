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

    public UIRotate ui;
    GameObject uiPrefab;
    public float uiTimer;
    public Transform uiParent;

    public void Initialize()
    {
        uiPrefab = Resources.Load<GameObject>("Prefabs/Canvas");
    }

    public void SpawnUI(Transform parent)
    {
        uiParent = parent;
        ui =  GameObject.Instantiate(uiPrefab, parent.position + new Vector3(0, 2, 0), Quaternion.identity, parent).GetComponentInChildren<UIRotate>();
        ui.Initialize();
    }

    public void DestroyUI()
    {
        uiTimer += Time.deltaTime;
        if (uiTimer > 5)
        {
            GameObject.Destroy(ui);
            uiTimer = 0;
        }
    }


    public void Refresh()
    {
        ui.Refresh();
        DestroyUI();
    }

    public void PhysicsRefresh()
    {

    }
    
}
