using System;
using Architecture.GameCore;
using Architecture.Utils.TimerUtils;
using ObjectPool.Scripts.PoolLogic;
using UnityEngine;

namespace Architecture.Audio {
  public class AudioObject : MonoBehaviour, IPoolable {
    private AudioSource _audioSource;
    private AudioValue _currentAudioValue;
    private bool _isPaused;
    private bool _isPlayDelay;
    private Timer _timer;
    private SoundType _soundType;

    private void Awake() {
      _audioSource = GetComponent<AudioSource>();
      _timer = gameObject.AddComponent<Timer>();
      _timer.Init(TimerType.UpdateTick);
    }

    private void OnDestroy() {
      FinishPlayback();
    }

    public void Get() {
      gameObject.SetActive(true);
    }

    public void Return() {
      gameObject.SetActive(false);
      Game.ScenesManager.GetPool().Return(this);
    }

    public event Action<AudioObject> PlaybackFinished;
    public event Action<AudioObject, SoundType> SinglePlaybackFinished;

    public void SetAudioSoundType(SoundType soundType) {
      _soundType = soundType;
    }

    public void Pause() {
      _timer.Pause();
      _isPaused = _audioSource;
      if (_isPaused) {
        _audioSource.Pause();
      }
    }

    public void Resume() {
      _timer.Resume();
      if (_isPaused) {
        _audioSource.UnPause();
      }
    }
    
    public void PlaySoundOneShot(AudioClip audioClip) {
      _audioSource.PlayOneShot(audioClip);
      _timer.Start(audioClip.length);
      _timer.OnTimerFinishedEvent += FinishPlayback;
    }
    
    public void PlaySoundOneShot(AudioClip audioClip, Vector3 position) {
      _audioSource.transform.position = position;
      _audioSource.PlayOneShot(audioClip);
      _timer.Start(audioClip.length);
      _timer.OnTimerFinishedEvent += FinishPlayback;
    }

    public void PlaySound(AudioClip audioClip) {
      _audioSource.clip = audioClip;
      _audioSource.Play();
      _timer.Start(audioClip.length);
      _timer.OnTimerFinishedEvent += FinishPlayback;
    }
    
    public void PlaySound(AudioClip audioClip, Vector3 position) {
      _audioSource.clip = audioClip;
      _audioSource.transform.position = position;
      _audioSource.Play();
      _timer.Start(audioClip.length);
      _timer.OnTimerFinishedEvent += FinishPlayback;
    }

    private void FinishPlayback() {
      _timer.OnTimerFinishedEvent -= FinishPlayback;
      SinglePlaybackFinished?.Invoke(this, _soundType);
      PlaybackFinished?.Invoke(this);
    }
  }
}