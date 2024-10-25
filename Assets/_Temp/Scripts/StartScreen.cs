using Architecture.CodeBase;
using Architecture.CodeBase.Constants;
using Architecture.CodeBase.Services.SceneLoader;
using Architecture.CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class StartScreen : UIScreen {
    [SerializeField]
    private Button _playButton;

    private ISceneLoader _sceneLoader;

    private void Awake() {
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _playButton.onClick.AddListener(PlayGame);
    }

    private void RemoveListeners() {
      _playButton.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame() {
      _sceneLoader.Load(Constants.SceneNames.MAIN_MENU);
    }
  }
}