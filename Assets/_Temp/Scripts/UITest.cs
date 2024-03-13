using Architecture;
using Architecture.CodeBase;
using Architecture.CodeBase.Services.Audio;
using Architecture.CodeBase.UI;
using UnityEngine;

namespace _Temp.Scripts {
  public class UITest : UIScreen {
    private IAudioService _audioService;
    private void Start() {
      _audioService= GlobalContainer.Container.Resolve<IAudioService>();
    }
    public void PlayOneShot() {
      _audioService.PlaySoundOneShot(SoundType.TestSound1);
    }
    public void PlayOneShotWithPos() {
      _audioService.PlaySoundOneShot(SoundType.TestSound2, new Vector3(13f,14f,24f));
    }
    public void PlaySound() {
      _audioService.PlaySound(SoundType.TestSound2);
    }
    public void PlaySoundWithPos() {
      _audioService.PlaySound(SoundType.TestSound1, new Vector3(13f,14f,24f));
    }
  }
}