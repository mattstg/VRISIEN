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


    [SerializeField] private AudioClip[] resourceAudioClips;
    Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();


    public void Initialize()
    {
        resourceAudioClips = Resources.LoadAll<AudioClip>("Bhumit/Audio");
        for (int i = 0; i < resourceAudioClips.Length; i++)
        {
            audioDict.Add(resourceAudioClips[i].name, resourceAudioClips[i]);
        }
    }

    public void PlayMusic(string name, GameObject go)
    {
        go.AddComponent<AudioSource>();
        AudioSource activeSource = go.GetComponent<AudioSource>();
        activeSource.clip = audioDict[name];
        activeSource.volume = 0.8f;
        activeSource.Play();
    }
    public void StopMusic( GameObject go)
    {
        
        AudioSource activeSource = go.GetComponent<AudioSource>();       
        activeSource.Stop();
    }

    public void PlaySfx(string name, GameObject go)
    {
        go.AddComponent<AudioSource>();
        AudioSource sfxSource = go.GetComponent<AudioSource>();
        sfxSource.PlayOneShot(audioDict[name]);
        sfxSource.loop = true;
        sfxSource.volume = 0.5f;
    }


    //public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    //{
    //    AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
    //    StartCoroutine(UpdateMusicWithFAde(activeSource, newClip, transitionTime));

    //}

    //public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    //{
    //    AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
    //    AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

    //    firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

    //    newSource.clip = musicClip;
    //    newSource.Play();
    //    StartCoroutine(UpdateMusicWithCrossFAde(activeSource, newSource, transitionTime));

    //}

    //private void StartCoroutine(IEnumerator enumerator)
    //{
    //    throw new NotImplementedException();
    //}

    //private IEnumerator UpdateMusicWithFAde(AudioSource acticveSource, AudioClip newClip, float transitionTime)
    //{
    //    if (!acticveSource.isPlaying)
    //        acticveSource.Play();

    //    float t = 0.0f;

    //    for (t = 0; t < transitionTime; t += Time.deltaTime)
    //    {
    //        acticveSource.volume = (1 - (t / transitionTime));
    //        yield return null;
    //    }

    //    acticveSource.Stop();
    //    acticveSource.clip = newClip;
    //    acticveSource.Play();


    //    for (t = 0; t < transitionTime; t += Time.deltaTime)
    //    {
    //        acticveSource.volume = (t / transitionTime);
    //        yield return null;
    //    }
    //}
    //private IEnumerator UpdateMusicWithCrossFAde(AudioSource original, AudioSource newSource, float transitionTime)
    //{

    //    float t = 0.0f;

    //    for (t = 0; t < transitionTime; t += Time.deltaTime)
    //    {
    //        original.volume = (1 - (t / transitionTime));
    //        newSource.volume = (t / transitionTime);
    //        yield return null;
    //    }

    //    original.Stop();
    //}




}



