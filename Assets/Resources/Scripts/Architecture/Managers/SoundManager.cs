using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Singleton
    private static SoundManager instance;
    private SoundManager() { }
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance = null)
                {
                    instance = new GameObject("Spawned Audio Manager", typeof(SoundManager)).GetComponent<SoundManager>();
                }
            }

            return instance;
        }
        private set
        {

        }
    }

    #endregion


    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;


    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.GetComponent<AudioSource>();
        musicSource2 = this.gameObject.GetComponent<AudioSource>();
        sfxSource = this.gameObject.GetComponent<AudioSource>();


        musicSource.loop = true;
        musicSource2.loop = true;
        //foreach (AudioClip a in audioclips)
        //{
        //    AudioClip clip1 = (AudioClip)Resources.Load("Assets/Bhumit/Audio");
        //    soundDict.Add(a.name,clip1);

        //}
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        StartCoroutine(UpdateMusicWithFAde(activeSource, newClip, transitionTime));

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

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}



