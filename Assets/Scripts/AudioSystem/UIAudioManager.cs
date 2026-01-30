using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public void OnOfMusic()
    {
        AudioManager.instance.OnOffMusic();
    }
    public void OnOfSFX()
    {
        AudioManager.instance.OnOffSFX();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }
    public void ChangeSFXVolume()
    {
        AudioManager.instance.MusicVolume(sfxSlider.value);
    }
}
