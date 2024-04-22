using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [HideInInspector] public AudioSource Audio;
    public AudioClip Clip;

    [SerializeField] private GameObject sfxSlider;
    private Slider slider;

    void Awake()
    {
        Audio = GetComponent<AudioSource>();
        slider = sfxSlider.GetComponent<Slider>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.sfxSlider = sfxSlider;
            slider = sfxSlider.GetComponent<Slider>();
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float volume = PlayerPrefs.GetFloat("SFXVolume");
            Audio.volume = volume;
            slider.value = volume * slider.maxValue;
        }
        else
        {
            slider.value = slider.maxValue;
            Audio.volume = slider.maxValue;
        }
    }

    public void SFXVolume()
    {
        float normalizedValue = slider.value / slider.maxValue;

        if (normalizedValue == 0)
        {
            Audio.mute = true;
        }
        else
        {
            Audio.mute = false;
            Audio.volume = normalizedValue;
        }

        PlayerPrefs.SetFloat("SFXVolume", normalizedValue);
    }
}
