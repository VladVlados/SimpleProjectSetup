using System.Collections.Generic;
using Architecture.CodeBase.Services.Factory;

namespace Architecture.CodeBase.Services.Audio.Components {
  public class AudioObject : FactoryPoolablePrefab {
    private readonly Dictionary<AudioSourceActiveStatus, List<AudioSourceComponent>> _audioSourceMap = new() {
      [AudioSourceActiveStatus.Active] = new List<AudioSourceComponent>(),
      [AudioSourceActiveStatus.Inactive] = new List<AudioSourceComponent>()
    };

    private enum AudioSourceActiveStatus {
      Active,
      Inactive
    }
    private IGameFactory _gameFactory;
    private float _renewalTime;
    
    public void Construct(IGameFactory gameFactory) {
      _gameFactory = gameFactory;
      DontDestroyOnLoad(this);
    }
  }
}