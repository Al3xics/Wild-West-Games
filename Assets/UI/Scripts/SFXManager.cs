using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [HideInInspector] public AudioSource Audio;
    public AudioClip Clip;
    public AudioClip Cut;
    public AudioClip Bomb;
    public AudioClip SplashInsect;
    public AudioClip CloseAdd;
    public AudioClip LaunchBall;
    public AudioClip WaterFill;
    public AudioClip WaterRemove;
    public AudioClip Validation;
    public AudioClip Fail;
    public AudioClip WinSequence;
    public AudioClip LoseSequence;
    public AudioClip GameOverSequence;

    [SerializeField] private GameObject sfxSlider;
    [SerializeField] private TMP_Text sfxSliderText;
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
            Instance.sfxSliderText = sfxSliderText;
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

    public void SetTextSlider()
    {
        sfxSliderText.text = slider.value.ToString();
    }
}
