using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Leaf._helpers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private int _maximumAudioCount = 10;
        [SerializeField]
        private float _masterVolume = 1f;
        [SerializeField]
        private bool _isAudioSourceMuted = false;

        [SerializeField]
        private List<AudioSource> _audioSources = new List<AudioSource>();
        [SerializeField]
        private List<Audio> _audioList = new List<Audio>();

        [Header("Audio Mixer")]
        [Tooltip("Audio mixer group for sound effects.")]
        [SerializeField] private AudioMixerGroup _soundMixerGroup;

        private void Awake()
        {
            // AudioSource componentlerini oluþtur
            for (int i = 0; i < _maximumAudioCount; i++)
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.outputAudioMixerGroup = _soundMixerGroup;
                _audioSources.Add(newSource);
            }
        }

        /// <summary>
        /// Plays an audio clip with the given name, volume, and loop setting, using an available AudioSource.
        /// If no AudioSource is available, logs a warning message and returns without playing the
        /// </summary>
        /// <param name="clipName"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public void PlaySound(string clipName, float volume = 1f, bool loop = false)
        {
            // Find audio source which is not playing
            AudioSource activeSource = null;
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (!_audioSources[i].isPlaying)
                {
                    activeSource = _audioSources[i];
                    break;
                }
            }

            if (activeSource == null)
            {
                Debug.LogWarning("There is no any other audioSource");
                return;
            }

            for (int i = 0; i < _audioList.Count; i++)
            {
                if (clipName == _audioList[i].Name)
                {
                    activeSource.mute = _isAudioSourceMuted;

                    activeSource.clip = _audioList[i].Clip;
                    //activeSource.volume = masterVolume * soundList[i].Volume;
                    activeSource.volume = _masterVolume * volume;
                    activeSource.loop = _audioList[i].Loop;
                    activeSource.Play();
                }
            }
        }

        /// <summary>
        /// Plays an audio clip with the given name, volume, and loop setting, using an available AudioSource.
        /// If no AudioSource is available, logs a warning message and returns without playing the
        /// </summary>
        /// <param name="clipName"></param>
        public void PlaySound(string clipName)
        {
            // Find audio source which is not playing
            AudioSource activeSource = null;
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (!_audioSources[i].isPlaying)
                {
                    activeSource = _audioSources[i];
                    break;
                }
            }

            if (activeSource == null)
            {
                Debug.LogWarning("There is no any other audioSource");
                return;
            }

            for (int i = 0; i < _audioList.Count; i++)
            {
                if (clipName == _audioList[i].Name)
                {
                    activeSource.mute = _isAudioSourceMuted;

                    activeSource.clip = _audioList[i].Clip;
                    activeSource.Play();
                }
            }
        }

        /// <summary>
        /// Plays an audio clip with the given clip reference, volume, and loop setting, using an available AudioSource.
        /// If no AudioSource is available, logs a warning message and returns without playing the   
        /// /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false)
        {
            // Find audio source which is not playing
            AudioSource activeSource = null;
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (!_audioSources[i].isPlaying)
                {
                    activeSource = _audioSources[i];
                    break;
                }
            }

            if (activeSource == null)
            {
                Debug.LogWarning("There is no any other audioSource");
                return;
            }

            activeSource.mute = _isAudioSourceMuted;

            activeSource.clip = clip;
            activeSource.volume = _masterVolume * volume;
            activeSource.loop = loop;
            activeSource.Play();
        }
    }
}