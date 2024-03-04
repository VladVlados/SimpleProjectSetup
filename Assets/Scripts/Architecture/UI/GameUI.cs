using Architecture.GameCore;
using Architecture.Scene;
using UnityEngine;

namespace Architecture.UI {
  public static class GameUI {
    public static UIController Controller { get; private set; }

    public static void Build(SceneConfig sceneConfig) {
      if (Controller == null) {
        Controller = CreateUIController();
      }

      Controller.Clear();
      Controller.BuildUI(sceneConfig);
    }

    private static UIController CreateUIController() {
      var pref = Resources.Load<UIController>(Constants.Constants.PrefabsNames.PATH_UI_CONTROLLER_PREFAB);
      UIController createdController = Object.Instantiate(pref);
      createdController.name = pref.name;
      Resources.UnloadUnusedAssets();
      return createdController;
    }
  }
}