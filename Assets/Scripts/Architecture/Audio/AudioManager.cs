using System.Collections.Generic;
using Architecture.GameCore;
using ObjectPool.Scripts.PoolLogic;
using UnityEngine;

namespace Architecture.Audio {
  public class AudioManager {
    private List<AudioObject> _playingSounds = new();
    private Dictionary<SoundType, AudioObject> _singleSounds = new();

    public void PlaySoundOneShot(SoundType soundType) {
      /*var audioObject = Game.GetSceneDataStorage<Pool>().Get<AudioObject>();
      AudioClip audioClip = Game.Settings.Values<AudioAsset>().GetAudioClip(soundType);
      audioObject.PlaySoundOneShot(audioClip);
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);*/
    }

    public void PlaySoundOneShot(SoundType soundType, Vector3 position) {
      /*var audioObject = Game.GetSceneDataStorage<Pool>().Get<AudioObject>();
      AudioClip audioClip = Game.Settings.Values<AudioAsset>().GetAudioClip(soundType);
      audioObject.PlaySoundOneShot(audioClip, position);
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);*/
    }

    public void PlaySound(SoundType soundType) {
      if (_singleSounds.ContainsKey(soundType)) {
        return;
      }

      /*var audioObject = Game.GetSceneDataStorage<Pool>().Get<AudioObject>();
      AudioClip audioClip = Game.Settings.Values<AudioAsset>().GetAudioClip(soundType);
      audioObject.SetAudioSoundType(soundType);
      _singleSounds.Add(soundType, audioObject);
      audioObject.PlaySound(audioClip);
      audioObject.SinglePlaybackFinished += RemoveSingleSound;
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);*/
    }

    public void PlaySound(SoundType soundType, Vector3 position) {
      if (_singleSounds.ContainsKey(soundType)) {
        return;
      }

      /*var audioObject = Game.GetSceneDataStorage<Pool>().Get<AudioObject>();
      AudioClip audioClip = Game.Settings.Values<AudioAsset>().GetAudioClip(soundType);
      audioObject.SetAudioSoundType(soundType);
      _singleSounds.Add(soundType, audioObject);
      audioObject.PlaySound(audioClip, position);
      audioObject.SinglePlaybackFinished += RemoveSingleSound;
      audioObject.PlaybackFinished += StopSound;
      _playingSounds.Add(audioObject);*/
    }

    private void StopSound(AudioObject audioObject) {
      audioObject.PlaybackFinished -= StopSound;
      audioObject.Return();
      _playingSounds.Remove(audioObject);
    }

    private void RemoveSingleSound(AudioObject audioObject, SoundType soundType) {
      _singleSounds.Remove(soundType);
      audioObject.SinglePlaybackFinished -= RemoveSingleSound;
    }
  }
}