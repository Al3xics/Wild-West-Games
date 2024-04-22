using NovaSamples.UIControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [HideInInspector] public AudioSource Audio;
    public AudioClip Clip;

    [SerializeField] private Slider sfxSlider;

    void Awake()
    {
        Audio = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Instance.sfxSlider = sfxSlider;
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float volume = PlayerPrefs.GetFloat("SFXVolume");
            Audio.volume = volume;
            sfxSlider.Value = volume * Slider.MaxValue;
        }
        else
        {
            Audio.volume = Slider.MaxValue;
        }
    }

    public void SFXVolume()
    {
        float normalizedValue = sfxSlider.Value / Slider.MaxValue;

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
