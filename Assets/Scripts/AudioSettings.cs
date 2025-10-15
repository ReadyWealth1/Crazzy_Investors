using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (AudioManager.instance != null)
        {
            // Register sliders with AudioManager
            AudioManager.instance.RegisterMusicSlider(musicSlider);
            AudioManager.instance.RegisterSFXSlider(sfxSlider);

            // Set initial values based on AudioManager's current values
            musicSlider.value = AudioManager.instance.musicSource.volume;
            sfxSlider.value = AudioManager.instance.SFXSource.volume;

            // Add listeners
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        else
        {
            Debug.LogError("AudioManager instance not found. Make sure the AudioManager is in the scene.");
        }
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.instance?.SetMusicVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.instance?.SetSFXVolume(volume);
    }
}