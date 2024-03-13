using System;
using System.Collections.Generic;
using Architecture.Audio;
using Architecture.CodeBase.Pool;
using Architecture.CodeBase.Services.Events;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.GlobalData;
using UnityEngine;

namespace Architecture.CodeBase.Services.Audio {
  public class AudioService : IAudioService {
    private readonly AudioPool _audioPool;
    private readonly IGameFactory _gameFactory;
    private readonly AudioFileStorage _audioFileStorage;
    private readonly IGlobalDataService _globalDataService;
    private readonly IMonoEventService _monoEvent;
    private readonly List<AudioObject> _playingSounds = new();
    private readonly Dictionary<SoundType, AudioObject> _singleSounds = new();

    public AudioService(IGameFactory gameFactory,  IMonoEventService monoEvent, IGlobalDataService globalDataService) {
      _gameFactory = gameFactory;
      _monoEvent = monoEvent;
      _globalDataService = globalDataService;
      Initialize();
    }

    public void PlaySoundOneShot(SoundType soundType) {
      AudioClip audioClip = _globalDataService.GetGlobalData<AudioAsset>().GetAudioClip(soundType);
      var audioObject = _gameFactory.Create<AudioObject>();
      audioObject.PlaySoundOneShot(audioClip);
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);
    }

    public void PlaySoundOneShot(SoundType soundType, Vector3 position) {
      AudioClip audioClip = _globalDataService.GetGlobalData<AudioAsset>().GetAudioClip(soundType);
      var audioObject = _gameFactory.Create<AudioObject>();
      audioObject.PlaySoundOneShot(audioClip, position);
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);
    }

    public void PlaySound(SoundType soundType) {
      if (_singleSounds.ContainsKey(soundType)) {
        return;
      }

      AudioClip audioClip = _globalDataService.GetGlobalData<AudioAsset>().GetAudioClip(soundType);
      var audioObject = _gameFactory.Create<AudioObject>();
      audioObject.SetAudioSoundType(soundType);
      _singleSounds.Add(soundType, audioObject);
      audioObject.PlaySound(audioClip);
      audioObject.SinglePlaybackFinished += RemoveSingleSound;
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);
    }

    public void PlaySound(SoundType soundType, Vector3 position) {
      if (_singleSounds.ContainsKey(soundType)) {
        return;
      }

      AudioClip audioClip = _globalDataService.GetGlobalData<AudioAsset>().GetAudioClip(soundType);
      var audioObject = _gameFactory.Create<AudioObject>();
      audioObject.SetAudioSoundType(soundType);
      _singleSounds.Add(soundType, audioObject);
      audioObject.PlaySound(audioClip, position);
      audioObject.SinglePlaybackFinished += RemoveSingleSound;
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);
    }

    public void StopSound(AudioObject audioObject) {
      audioObject.PlaybackFinished -= StopSound;
      audioObject.Return();
      _playingSounds.Remove(audioObject);
    }

    public void Dispose() {
      _monoEvent.OnUpdate -= OnUpdate;
    }

    public void Initialize() {
      _monoEvent.OnUpdate += OnUpdate;
      IsInitialized = true;
    }
    
    private void OnUpdate(object sender, EventArgs e) {
      if (_playingSounds.Count <= 0) {
        return;
      }

      foreach (var activeAudio in _playingSounds) {
        if (activeAudio.IsPlaying()) {
          continue;
        }

        activeAudio.Return();
        _playingSounds.Remove(activeAudio);
        break;
      }
    }

    public bool IsInitialized { get; private set; }

    private void RemoveSingleSound(AudioObject audioObject, SoundType soundType) {
      _singleSounds.Remove(soundType);
      audioObject.SinglePlaybackFinished -= RemoveSingleSound;
    }
  }
}