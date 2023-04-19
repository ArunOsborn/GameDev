using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private GameObject mainVolumeSlider;
    [SerializeField] private GameObject musicVolumeSlider;
    [SerializeField] private GameObject sfxVolumeSlider;

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        // Positions sliders according to current saved settings
        mainVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Main Volume");
        Debug.Log("Set main volume slider to: " + PlayerPrefs.GetFloat("Main Volume"));
        musicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music Volume");
        sfxVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX Volume");

    }

    public void SetMainVolume(float volume)
    {
        PlayerPrefs.SetFloat("Main Volume", volume);
        float currentVol;
        mixer.GetFloat("MainVolume", out currentVol);
        Debug.Log("Current main volume is: " + currentVol);
        mixer.SetFloat("MainVolume", volume);
        Debug.Log("Set main volume to: " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music Volume", volume);
        mixer.SetFloat("MusicVolume", volume);
        Debug.Log("Set music volume to: " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFX Volume", volume);
        mixer.SetFloat("SFXVolume", volume);
        Debug.Log("Set sfx volume to: " + volume);
    }
}
