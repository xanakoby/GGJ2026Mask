using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DesignPatterns.Generics;

public class AudioManager : Singleton<AudioManager>
{
    //public static AudioManager instance;

    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public override void Awake()
    {
        base.Awake();
    }
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            //fa fade in fade out tra musica

            //musicSource.clip = s.clip;
            //musicSource.Play();

            StartCoroutine(FadeInOutMusic(s));
        }
    }
    IEnumerator FadeInOutMusic(Sound s)
    {
        float currentTime = 0;
        float startVolume = musicSource.volume;
        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, currentTime / 0.5f);
            yield return null;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
        currentTime = 0;
        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, startVolume, currentTime / 0.5f);
            yield return null;
        }
        yield return null;
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void OnOffMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void OnOffSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float _volume)
    {
        musicSource.volume = _volume;
    }

    public void SFXVolume(float _volume)
    {
        sfxSource.volume = _volume;
    }
}
