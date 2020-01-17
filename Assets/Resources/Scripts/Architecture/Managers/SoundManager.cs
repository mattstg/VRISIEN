using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    private static SoundManager instance;
    private SoundManager() { }
    public static SoundManager Instance { get { return instance ?? (instance = new SoundManager()); } }
    #endregion


}
