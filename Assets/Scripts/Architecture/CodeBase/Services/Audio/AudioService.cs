using System;
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
    private readonly IMonoEventService _monoEvent;
    public AudioService(IGameFactory gameFactory, IMonoEventService monoEvent, IGlobalDataService staticDataService) {
      //_audioPool = new AudioPool(gameFactory.Create<AudioObject>);
      _gameFactory = gameFactory;
      _audioFileStorage = staticDataService.GetStaticData<AudioFileStorage>();
      _monoEvent = monoEvent;
      Initialize();
    }
    
    public void Dispose() {
      throw new NotImplementedException();
    }

    public void Initialize() {
      throw new NotImplementedException();
    }

    public bool IsInitialized { get; }

    public void Play(AudioName audioName, AudioSettings data = null) {
      throw new NotImplementedException();
    }

    public void Play(AudioName audioName, Vector3 at, AudioSettings data = null) {
      throw new NotImplementedException();
    }

    public void Stop(AudioName audioName) {
      throw new NotImplementedException();
    }

    public void Mute() {
      throw new NotImplementedException();
    }

    public void Unmute() {
      throw new NotImplementedException();
    }
  }
}