using System;
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


    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    [SerializeField] public AudioClip[] resourceAudioClips;

    private bool firstMusicSourceIsPlaying;

    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();


    public void Initialize()
    {
        //DontDestroyOnLoad(this.gameObject);

       

        resourceAudioClips = Resources.LoadAll<AudioClip>("Bhumit/Audio");
        Debug.Log("apdu random");
        foreach (AudioClip a in resourceAudioClips)
        {
            Debug.Log(a.name);
            //AudioClip clip1 = resourceAudioClips;
            //audioDict.Add(a.name, clip1);

        }

        for (int i = 0; i<resourceAudioClips.Length;i++)
        {
            audioDict.Add(resourceAudioClips[i].name, resourceAudioClips[i]);
            
        }

       
    }

    public void PlayMusic(string name, GameObject go)
    {
        //musicSource = go.GetComponent<AudioSource>();
        //musicSource2 = this.gameObject.GetComponent<AudioSource>();
        //sfxSource = this.gameObject.GetComponent<AudioSource>();

        go.AddComponent<AudioSource>();

        foreach (AudioClip b in resourceAudioClips)
        {
            Debug.Log(audioDict[b.name]);
        }
        
        //go.loop = true;
        //musicSource2.loop = true;
        Debug.Log("Play music working 5625vfbfbdfbfbfbsfbsfb");
        AudioSource activeSource = go.GetComponent<AudioSource>();
        //foreach(KeyValuePair<string,AudioClip> item in audioDict)
        //{
        //    if(item.Key.Equals(name))
        //    {
        activeSource.clip = audioDict[name];
        //    }
        //}
        
        activeSource.volume = 1;
        activeSource.Play();

        
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        //StartCoroutine(UpdateMusicWithFAde(activeSource, newClip, transitionTime));

    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFAde(activeSource, newSource, transitionTime));

    }

    private void StartCoroutine(IEnumerator enumerator)
    {
        throw new NotImplementedException();
    }

    private IEnumerator UpdateMusicWithFAde(AudioSource acticveSource, AudioClip newClip, float transitionTime)
    {
        if (!acticveSource.isPlaying)
            acticveSource.Play();

        float t = 0.0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            acticveSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        acticveSource.Stop();
        acticveSource.clip = newClip;
        acticveSource.Play();


        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            acticveSource.volume = (t / transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFAde(AudioSource original, AudioSource newSource, float transitionTime)
    {

        float t = 0.0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void SetMusicVolume(float volume, float pitch)
    {
        Debug.Log("cdcsdcsdcdsvsdvdsvvvvvvvvvvvvv");
        musicSource.volume = volume;
        musicSource2.volume = volume;
        musicSource.pitch = pitch;
        musicSource2.pitch = pitch;
    }

    public void SetSFXVolume(float volume,float pitch)
    {
        sfxSource.volume = volume;
        sfxSource.pitch = pitch;
    }

}



