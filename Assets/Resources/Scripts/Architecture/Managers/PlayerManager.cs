using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager for Player and VRPlayer scripts
public class PlayerManager : IManagable
{
    #region Singleton
    private static PlayerManager instance;
    private PlayerManager() { }
    public static PlayerManager Instance { get { return instance ?? (instance = new PlayerManager()); } }
    #endregion
    public VRPlayer player;

    public void Initialize()
    {
        GameObject newPlayer = GameObject.FindGameObjectWithTag("Player");// Resources.Load<GameObject>("Prefabs/Chair");
            //Instantiate(Resources.Load<GameObject>(ResourceDirHelper.PLAYER_PREFAB_PATH), GameLinks.gl.playerStartPos.position + new Vector3(0, 2.3f, 0), Quaternion.identity);
        player = newPlayer.GetComponent<VRPlayer>();
        player.Initialize();
    }

    public void PostInitialize()
    {
        player.PostInitialize();
    }

    public void PhysicsRefresh()
    {
        //player.PhysicsRefresh(InputManager.Instance.physicsRefreshInputPkg);
    }

    public void Refresh()
    {
        player.Refresh();
    }
    
}
