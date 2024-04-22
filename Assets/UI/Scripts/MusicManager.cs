using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private GameObject musicSlider;
    private AudioSource audioSource;
    private Slider slider;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        slider = musicSlider.GetComponent<Slider>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.musicSlider = musicSlider;
            slider = musicSlider.GetComponent<Slider>();
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = volume;
            slider.value = volume * slider.maxValue;
        }
        else
        {
            slider.value = slider.maxValue;
            audioSource.volume = slider.maxValue;
        }
    }

    public void MusicVolume()
    {
        float normalizedValue = slider.value / slider.maxValue;

        if (normalizedValue == 0)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
            audioSource.volume = normalizedValue;
        }

        PlayerPrefs.SetFloat("MusicVolume", normalizedValue);
    }
}
