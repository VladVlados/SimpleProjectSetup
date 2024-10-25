using Architecture.CodeBase;
using Architecture.CodeBase.Services.SceneLoader;
using Architecture.CodeBase.UI;
using Attributes.SceneNameAttribute;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class PausePanel : UIScreen {
    [SerializeField, SceneName]
    private string _sceneName;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _backToMenuButton;
    [SerializeField]
    private GameObject _pausePanel;
  
    private ISceneLoader _sceneLoader;

    private void Awake() {
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void OpenPausePanel() {
      _pausePanel.SetActive(true);
    }
  
    private void ClosePausePanel() {
      _pausePanel.SetActive(false);
    }

    private void BackToMenu() {
      _sceneLoader.Load(_sceneName);
    }

    private void AddListeners() {
      _backToMenuButton.onClick.AddListener(BackToMenu);
      _pauseButton.onClick.AddListener(OpenPausePanel);
      _closeButton.onClick.AddListener(ClosePausePanel);
    }

    private void RemoveListeners() {
      _backToMenuButton.onClick.RemoveListener(BackToMenu);
      _pauseButton.onClick.RemoveListener(OpenPausePanel);
      _closeButton.onClick.RemoveListener(ClosePausePanel);
    }
  }
}