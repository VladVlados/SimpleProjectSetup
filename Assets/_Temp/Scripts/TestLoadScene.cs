using Architecture.Constants;
using Architecture.GameCore;
using UnityEngine;

namespace _Temp.Scripts {
    public class TestLoadScene : MonoBehaviour {
        public void LoadScene() {
            Game.ScenesManager.LoadScene(Constants.SceneNames.MAIN_MENU);
        }
        
        public void LoadSceneSandBox() {
            Game.ScenesManager.LoadScene("SandBox");
        }
    }
}
