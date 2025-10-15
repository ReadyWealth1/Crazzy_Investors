using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source ---------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("---------Audio Clip ---------")]
    public AudioClip background;
    public AudioClip coin_Pickup;
    public AudioClip Buy_sound;

    public static AudioManager instance { get; private set; }

    private List<Slider> musicSliders = new List<Slider>();
    private List<Slider> sfxSliders = new List<Slider>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSource.clip == null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }

        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        SetMusicVolume(savedMusicVolume);
        SetSFXVolume(savedSFXVolume);
    }

    public void RegisterMusicSlider(Slider slider)
    {
        musicSliders.Add(slider);
        slider.value = musicSource.volume;
    }

    public void RegisterSFXSlider(Slider slider)
    {
        sfxSliders.Add(slider);
        slider.value = SFXSource.volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();

        foreach (var slider in musicSliders)
        {
            if (slider != null && slider.value != volume)
            {
                slider.value = volume;
            }
        }
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();

        foreach (var slider in sfxSliders)
        {
            if (slider != null && slider.value != volume)
            {
                slider.value = volume;
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
