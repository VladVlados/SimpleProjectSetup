using System;
using System.Collections.Generic;
using Architecture.CodeBase.Services.GlobalData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Architecture.Audio {
  [CreateAssetMenu(fileName = "AudioAssets", menuName = "Audio/AudioAssets")]
  public class AudioAsset : ItemGlobalData {
    [SerializeField]
    private AudioValue[] _audioValues;
    private Dictionary<SoundType, AudioClip[]> _audioValuesDictionary;

    protected override void LoadData() {
      _audioValuesDictionary = new Dictionary<SoundType, AudioClip[]>();
      for (var i = 0; i < _audioValues.Length; i++) {
        _audioValuesDictionary.Add(_audioValues[i].GetSoundType, _audioValues[i].GetAudioClips);
      }
    }

    public AudioClip GetAudioClip(SoundType soundType) {
      _audioValuesDictionary.TryGetValue(soundType, out AudioClip[] audioClipArray);
      AudioClip audioClip = audioClipArray?[Random.Range(0, audioClipArray.Length - 1)];
      return audioClip;
    }
  }

  [Serializable]
  public class AudioValue {
    [SerializeField]
    private AudioClip[] _audioClipBundle;

    [SerializeField]
    private SoundType _soundType;

    public SoundType GetSoundType {
      get {
        return _soundType;
      }
    }

    public AudioClip[] GetAudioClips {
      get {
        return _audioClipBundle;
      }
    }
  }

  public enum SoundType {
    TestSound1,
    TestSound2,
    TestSound3,
    TestSound4
  }
}