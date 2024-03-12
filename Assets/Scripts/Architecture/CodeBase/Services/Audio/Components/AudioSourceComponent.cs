using Architecture.CodeBase.Services.Factory;
using UnityEngine;

namespace Architecture.CodeBase.Services.Audio.Components {
  public class AudioSourceComponent : FactoryPrefab {
    [SerializeField]
    private AudioSource _audioSource;
  }
}