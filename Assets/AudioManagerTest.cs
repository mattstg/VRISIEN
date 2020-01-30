using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTest : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {   
        
        Debug.Log("Inside start++++++++++++++++++++++++");
        SoundManager.Instance.PlayMusic("Bg_Music", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
