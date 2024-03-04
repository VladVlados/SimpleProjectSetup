using System;
using System.Collections.Generic;
using Architecture.Settings.Global;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Audio {
  [CreateAssetMenu(fileName = "AudioAssets", menuName = "Audio/AudioAssets")]
  public class AudioAsset : SettingsComponent {
    [SerializeField]
    private AudioValue[] _audioValues;
    private Dictionary<SoundType, AudioClip[]> _audioValuesDictionary;

    public override void InitializeSettings() {
      foreach (var audioAsset in _audioValues) {
        _audioValuesDictionary.Add(audioAsset.SoundType, audioAsset.AudioClipBundle);
      }
    }

    public AudioClip GetAudioClip(SoundType soundType) {
      _audioValuesDictionary.TryGetValue(soundType, out AudioClip[] audioClipArray);
      AudioClip audioClip = audioClipArray?[Random.Range(0, audioClipArray.Length)];
      return audioClip;
    }
  }

  [Serializable]
  public class AudioValue {
    [SerializeField]
    private AudioClip[] _audioClipBundle;

    [SerializeField]
    private SoundType _soundType;
    
    public SoundType SoundType { get; private set; }
    public AudioClip[] AudioClipBundle { get; private set; }
  }

  public enum SoundType {
    Nothing = 100,
    TestSound = 101
  }
}