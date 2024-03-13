using UnityEngine;

namespace Architecture.CodeBase.Services.Audio {
  public interface IAudioService : IInitializedService {
    void PlaySoundOneShot(SoundType soundType);

    void PlaySoundOneShot(SoundType soundType, Vector3 position);

    void PlaySound(SoundType soundType);

    void PlaySound(SoundType soundType, Vector3 position);

    void StopSound(AudioObject audioObject);
  }
}