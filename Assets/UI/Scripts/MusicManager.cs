using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public TMP_Text musicSliderText;
    [HideInInspector] public Slider musicSlider;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LinkMusicToSliderListener()
    {
        musicSlider = GameObject.Find("Canvas/Panel/Settings/Music").GetComponent<Slider>();
        musicSliderText = GameObject.Find("Canvas/Panel/Settings/Music/Handle Slide Area/Handle/Number").GetComponent<TMP_Text>();

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            Instance.audioSource.volume = volume;
            musicSlider.value = volume * musicSlider.maxValue;

            string number = PlayerPrefs.GetString("MusicSliderText");
            musicSliderText.text = number;
        }
        else
        {
            musicSlider.value = musicSlider.maxValue / 2;
            Instance.audioSource.volume = musicSlider.maxValue / musicSlider.maxValue / 2;

            musicSliderText.text = musicSlider.value.ToString();
        }

        musicSlider.onValueChanged.AddListener(SetTextSlider);
        musicSlider.onValueChanged.AddListener(MusicVolume);
    }

    public void MusicVolume(float val)
    {
        float normalizedValue = musicSlider.value / musicSlider.maxValue;

        if (normalizedValue == 0)
        {
            Instance.audioSource.mute = true;
        }
        else
        {
            Instance.audioSource.mute = false;
            Instance.audioSource.volume = normalizedValue;
        }

        PlayerPrefs.SetFloat("MusicVolume", normalizedValue);
    }

    public void SetTextSlider(float val)
    {
        musicSliderText.text = musicSlider.value.ToString();

        PlayerPrefs.SetString("MusicSliderText", musicSliderText.text);
    }
}
