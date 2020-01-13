using UnityEngine;
using System.Collections;

public class VRCameraManager : IManagable
{

    #region Singleton
    private static VRCameraManager instance;
    private VRCameraManager() { }
    public static VRCameraManager Instance { get { return instance ?? (instance = new VRCameraManager()); } }
    #endregion
    public OVRCameraRig vrCamRig;

    public void Initialize()
    {
        GameObject vrCamObj = GameObject.FindGameObjectWithTag("OVRCamera");
        //Instantiate(Resources.Load<GameObject>(ResourceDirHelper.PLAYER_PREFAB_PATH), GameLinks.gl.playerStartPos.position + new Vector3(0, 2.3f, 0), Quaternion.identity);
        vrCamRig = vrCamObj.GetComponent<OVRCameraRig>();
        vrCamRig.Initialize();
    }

    public void PostInitialize()
    {
        vrCamRig.PostInitialize();
    }

    public void PhysicsRefresh()
    {
        vrCamRig.PhysicsRefresh();
    }

    public void Refresh()
    {
        vrCamRig.Refresh();
    }
}


