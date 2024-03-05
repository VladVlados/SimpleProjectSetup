using Architecture.Audio;
using Architecture.Constants;
using Architecture.GameCore;
using Architecture.UI;
using UnityEngine;

namespace _Temp.Scripts {
  public class TestLoadScene : MonoBehaviour {
    public void LoadScene() {
      Game.ScenesManager.LoadScene(Constants.SceneNames.MAIN_MENU);
    }

    public void LoadSceneSandBox() {
      GameUI.Controller.GetUIElement<Footer>().TestFooter();
      Game.ScenesManager.LoadScene("SandBox");
    }

    public void PlaySoundOneShot() {
      Game.AudioManager.PlaySoundOneShot(SoundType.TestSound1);
    }
    
    public void PlaySoundOneShotWithPosition() {
      Game.AudioManager.PlaySoundOneShot(SoundType.TestSound2, new Vector3(13f, 13f, 13f));
    }
    
    public void PlaySound() {
      Game.AudioManager.PlaySound(SoundType.TestSound3);
    }
    
    public void PlaySoundWithPosition() {
      Game.AudioManager.PlaySound(SoundType.TestSound4, new Vector3(15f, 15f, 15f));
    }
  }
}