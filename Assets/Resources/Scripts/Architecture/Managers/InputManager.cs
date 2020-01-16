using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Still in progress, not currently used.
public class InputManager : IManagable
{
    #region Singleton
    private static InputManager instance;
    private InputManager() { }
    public static InputManager Instance { get { return instance ?? (instance = new InputManager()); } }
    #endregion

    public InputPkg refreshInputPkg = new InputPkg();
    public InputPkg physicsRefreshInputPkg = new InputPkg();

    public void Initialize()
    {

    }

    public void PhysicsRefresh()
    {
        SetInputPkg(physicsRefreshInputPkg);
    }

    private void SetInputPkg(InputPkg ip)
    {
        ip.turnLeft = Input.GetKey(KeyCode.A);
        ip.turnRight = Input.GetKey(KeyCode.D);
        ip.moveForward = Input.GetKey(KeyCode.W);
        ip.moveBackward = Input.GetKey(KeyCode.S);
    }

    public void Refresh()
    {
        SetInputPkg(refreshInputPkg);
    }

    public void PostInitialize()
    {

    }


    //Data class to handle player input.
    public class InputPkg
    {
        public bool turnLeft;
        public bool turnRight;
        public bool moveForward;
        public bool moveBackward;
    }

    
}
 