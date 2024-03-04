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
  }
}