using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Manages the game options menu, including settings for audio and screen resolution.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    // SerializeField attributes to expose private fields in the inspector
    [Header("Audio Settings")]
    [Tooltip("Slider to control the sound volume.")]
    [SerializeField] private Slider _soundSlider;
    [Tooltip("Audio mixer for controlling sound volume.")]
    [SerializeField] private AudioMixer _soundMixer;
    [Space]
    [Tooltip("Slider to control the music volume.")]
    [SerializeField] private Slider _musicSlider;
    [Tooltip("Audio mixer for controlling music volume.")]
    [SerializeField] private AudioMixer _musicMixer;

    ////[Header("Resolution Settings")]
    ////[Tooltip("Dropdown to select the screen resolution.")]
    //[SerializeField] private Dropdown _resolutionDropdown;

    // HideInInspector attribute to hide the field in the inspector but keep it serialized
    [HideInInspector] public float _currentSoundVolume;
    [HideInInspector] public float _currentMusicVolume;
    //[HideInInspector] public int _currentResolutionIndex;

    //// Available resolutions
    //private Resolution[] _resolutions;

    private void Start()
    {
        InitializeVolumeSettings();
        //InitializeResolutionSettings();
    }

    /// <summary>
    /// Initializes the volume settings.
    /// </summary>
    private void InitializeVolumeSettings()
    {
        _soundSlider.onValueChanged.AddListener(SetSoundVolume);
        _currentSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.75f);
        _soundSlider.value = _currentSoundVolume;

        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        _musicSlider.value = _currentMusicVolume;
    }

    /// <summary>
    /// Sets the sound volume.
    /// </summary>
    /// <param name="volume">The new volume level.</param>
    public void SetSoundVolume(float volume)
    {
        _currentSoundVolume = volume;
        float dB = volume <= 0.01f ? -80f : Mathf.Log10(volume) * 20f;
        _soundMixer.SetFloat("SoundVolume", dB);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    /// <summary>
    /// Sets the music volume.
    /// </summary>
    /// <param name="volume">The new volume level.</param>
    public void SetMusicVolume(float volume)
    {
        _currentMusicVolume = volume;
        float dB = volume <= 0.01f ? -80f : Mathf.Log10(volume) * 20f;
        _musicMixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    ///// <summary>
    ///// Initializes the resolution settings.
    ///// </summary>
    //private void InitializeResolutionSettings()
    //{
    //    _resolutions = Screen.resolutions;
    //    _resolutionDropdown.ClearOptions();

    //    List<string> options = new List<string>();
    //    int currentResolutionIndex = 0;

    //    for (int i = 0; i < _resolutions.Length; i++)
    //    {
    //        string option = _resolutions[i].width + " x " + _resolutions[i].height;
    //        options.Add(option);

    //        if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
    //        {
    //            currentResolutionIndex = i;
    //        }
    //    }

    //    _resolutionDropdown.AddOptions(options);
    //    _resolutionDropdown.value = currentResolutionIndex;
    //    _resolutionDropdown.RefreshShownValue();
    //    _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    //}

    ///// <summary>
    ///// Sets the screen resolution.
    ///// </summary>
    ///// <param name="resolutionIndex">The index of the selected resolution.</param>
    //public void SetResolution(int resolutionIndex)
    //{
    //    Resolution resolution = _resolutions[resolutionIndex];
    //    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    //    PlayerPrefs.SetInt("resolution", resolutionIndex);
    //}
}