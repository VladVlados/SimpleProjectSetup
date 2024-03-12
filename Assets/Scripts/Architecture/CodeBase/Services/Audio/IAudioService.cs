using Architecture.Audio;
using UnityEngine;

namespace Architecture.CodeBase.Services.Audio {
  public interface IAudioService : IInitializedService {
    void Play(AudioName audioName, AudioSettings data = null);

    void Play(AudioName audioName, Vector3 at, AudioSettings data = null);

    void Stop(AudioName audioName);

    void Mute();

    void Unmute();
  }
}