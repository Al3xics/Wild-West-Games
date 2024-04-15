using NovaSamples.UIControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private Slider musicSlider;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            //musicSlider = GameObject.Find("Music").GetComponent<Slider>();
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = volume;
            musicSlider.Value = volume * Slider.MaxValue;
        }
        else
        {
            audioSource.volume = Slider.MaxValue;
        }
    }

    public void MusicVolume()
    {
        //if (musicSlider == null)
        //{
        //    musicSlider = GameObject.FindWithTag("MUSIC").GetComponent<Slider>();
        //}

        float normalizedValue = musicSlider.Value / Slider.MaxValue;

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
