using Architecture.CodeBase.Services.SceneLoader;
using Architecture.UI;
using UnityEngine;
using Zenject;

namespace _Temp.Scripts {
  public class MainMenu : MonoBehaviour
  {
    private ISceneLoader _sceneLoader;

    private void Start() {
      var panel = GameUI.Controller.GetUIElement<UITest>();
      Debug.Log(panel.name);
    }

    [Inject]
    private void Construct(ISceneLoader sceneLoader) {
      _sceneLoader = sceneLoader;
    }
  }
}
