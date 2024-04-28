using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [HideInInspector] public AudioSource Audio;
    [HideInInspector] private TMP_Text sfxSliderText;
    [HideInInspector] private Slider sfxSlider;
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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Audio = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LinkSFXToSliderListener()
    {
        sfxSlider = GameObject.Find("Canvas/Panel/Settings/Sound Effects").GetComponent<Slider>();
        sfxSliderText = GameObject.Find("Canvas/Panel/Settings/Sound Effects/Handle Slide Area/Handle/Number").GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float volume = PlayerPrefs.GetFloat("SFXVolume");
            Instance.Audio.volume = volume;
            sfxSlider.value = volume * sfxSlider.maxValue;

            string number = PlayerPrefs.GetString("SFXSliderText");
            sfxSliderText.text = number;
        }
        else
        {
            sfxSlider.value = sfxSlider.maxValue;
            Instance.Audio.volume = sfxSlider.maxValue / sfxSlider.maxValue;

            sfxSliderText.text = sfxSlider.value.ToString();
        }

        sfxSlider.onValueChanged.AddListener(SetTextSlider);
        sfxSlider.onValueChanged.AddListener(SFXVolume);
    }

    public void SFXVolume(float val)
    {
        float normalizedValue = sfxSlider.value / sfxSlider.maxValue;

        if (normalizedValue == 0)
        {
            Instance.Audio.mute = true;
        }
        else
        {
            Instance.Audio.mute = false;
            Instance.Audio.volume = normalizedValue;
        }

        PlayerPrefs.SetFloat("SFXVolume", normalizedValue);
    }

    public void SetTextSlider(float val)
    {
        sfxSliderText.text = sfxSlider.value.ToString();

        PlayerPrefs.SetString("SFXSliderText", sfxSliderText.text);
    }
}
