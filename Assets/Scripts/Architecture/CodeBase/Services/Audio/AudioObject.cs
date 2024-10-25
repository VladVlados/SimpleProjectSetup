using System;
using Architecture.CodeBase.Services.Factory;
using UnityEngine;

namespace Architecture.CodeBase.Services.Audio {
  public class AudioObject : FactoryPoolablePrefab {
    private AudioSource _audioSource;
    private AudioValue _currentAudioValue;
    private SoundType _soundType;

    private void Awake() {
      _audioSource = GetComponent<AudioSource>();
      OnReturnEvent += FinishPlayback;
    }

    public event Action<AudioObject> PlaybackFinished;
    public event Action<AudioObject, SoundType> SinglePlaybackFinished;

    public void SetAudioSoundType(SoundType soundType) {
      _soundType = soundType;
    }

    public void PlaySoundOneShot(AudioClip audioClip) {
      _audioSource.clip = audioClip;
      _audioSource.PlayOneShot(audioClip);
    }

    public bool IsPlaying() {
      return _audioSource.isPlaying;
    }

    public void PlaySoundOneShot(AudioClip audioClip, Vector3 position) {
      _audioSource.transform.position = position;
      _audioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(AudioClip audioClip) {
      _audioSource.clip = audioClip;
      _audioSource.Play();
    }

    public void PlaySound(AudioClip audioClip, Vector3 position) {
      _audioSource.clip = audioClip;
      _audioSource.transform.position = position;
      _audioSource.Play();
    }

    private void FinishPlayback(object sender, EventArgs e) {
      OnReturnEvent -= FinishPlayback;
      SinglePlaybackFinished?.Invoke(this, _soundType);
      PlaybackFinished?.Invoke(this);
    }
  }
}