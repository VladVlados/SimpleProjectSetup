using System.Collections.Generic;
using Architecture.GameCore;
using UnityEngine;

namespace Architecture.Audio {
  public class AudioManager {
    private Dictionary<SoundType, AudioSource> _playingSounds;
    
    public void PlaySound(SoundType soundType) {
      GameObject soundGameObject = new GameObject(soundType.ToString());
      AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
      audioSource.PlayOneShot(Game.Settings.Values<AudioAsset>().GetAudioClip(soundType));
      _playingSounds.TryAdd(soundType, audioSource);
    }
    
    public void PlaySound(SoundType soundType, Vector3 position) {
      GameObject soundGameObject = new GameObject(soundType.ToString());
      soundGameObject.transform.position = position;
      AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
      audioSource.PlayOneShot(Game.Settings.Values<AudioAsset>().GetAudioClip(soundType));
      _playingSounds.TryAdd(soundType, audioSource);
    }

    public void StopSound(SoundType soundType) {
      _playingSounds.TryGetValue(soundType, out AudioSource audioSource);
      if (audioSource != null) {
        audioSource.Stop();
      }
    }

    private void RemoveSoundFromPlayingSounds(SoundType soundType) {
      _playingSounds.Remove(soundType);
    }
  }
}