using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private GameObject mainVolumeSlider;
    [SerializeField] private GameObject musicVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        mainVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Main Volume");
        Debug.Log("Set main volume slider to: " + PlayerPrefs.GetFloat("Main Volume"));
        musicVolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music Volume");

    }

    public void SetMainVolume(float volume)
    {
        PlayerPrefs.SetFloat("Main Volume", volume);
        Debug.Log("Set main volume to: " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("Music Volume", volume);
        Debug.Log("Set music volume to: " + volume);
    }
}
