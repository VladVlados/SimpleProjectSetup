using Architecture.CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class MainMap : UIScreen {
    [SerializeField]
    private Button _baseScene;
    [SerializeField]
    private Button _backFromBaseScene;
    [SerializeField]
    private Button[] _sceneZoneButtons;
    [SerializeField]
    private Button[] _baseSceneButtons;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _backFromBaseScene.onClick.AddListener(MoveFromBaseScene);
      _baseScene.onClick.AddListener(MoveToBaseScene);
    }

    private void RemoveListeners() {
      _backFromBaseScene.onClick.RemoveListener(MoveFromBaseScene);
      _baseScene.onClick.RemoveListener(MoveToBaseScene);
    }

    private void MoveFromBaseScene() {
      for (var i = 0; i < _baseSceneButtons.Length; i++) {
        _baseSceneButtons[i].gameObject.SetActive(false);
      }

      for (var i = 0; i < _sceneZoneButtons.Length; i++) {
        _sceneZoneButtons[i].gameObject.SetActive(true);
      }
    }

    private void MoveToBaseScene() {
      for (var i = 0; i < _sceneZoneButtons.Length; i++) {
        _sceneZoneButtons[i].gameObject.SetActive(false);
      }

      for (var i = 0; i < _baseSceneButtons.Length; i++) {
        _baseSceneButtons[i].gameObject.SetActive(true);
      }
    }
  }
}