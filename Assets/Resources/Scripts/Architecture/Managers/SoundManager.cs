using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    
    #region Singleton
    private static SoundManager instance;
    private SoundManager() { }
    public static SoundManager Instance { get { return instance ?? (instance = new SoundManager()); } }
    #endregion

    public AudioClip[] audioclips;

    Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();
    public void Initialize()
    {
        
        foreach (AudioClip a in audioclips)
        {
            AudioClip clip1 = (AudioClip)Resources.Load("Assets/Bhumit/Audio");
            soundDict.Add(a.name,clip1);
        }
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

    



    public void Play(AudioSource audioSource,string name)
    {
        audioSource.clip = soundDict["abc"];
        audioSource.Play();
    }

    public static void PlayMusic(string name)
    {

        
    }
}




