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
        AudioManager.Instance.OnOffMusic();
    }
    public void OnOfSFX()
    {
        AudioManager.Instance.OnOffSFX();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }
    public void ChangeSFXVolume()
    {
        AudioManager.Instance.MusicVolume(sfxSlider.value);
    }
}
