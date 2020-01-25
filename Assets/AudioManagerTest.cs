using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTest : MonoBehaviour
{
    public GameObject musicSource;
    public string musicName;

    // Start is called before the first frame update
    void Start()
    {   
        
        Debug.Log("Inside start++++++++++++++++++++++++");
        SoundManager.Instance.PlayMusic(musicName,musicSource);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
