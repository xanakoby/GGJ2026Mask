using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using DesignPatterns.Generics;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager instance;

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
            musicSource.clip = s.clip;
            musicSource.Play();
        }
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
