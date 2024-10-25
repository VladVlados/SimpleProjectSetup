using Architecture.CodeBase;
using Architecture.CodeBase.Services.SceneLoader;
using Attributes.SceneNameAttribute;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class LocationButton : MonoBehaviour {
    [SerializeField, SceneName]
    private string _sceneName;
    private Button _button;
    private ISceneLoader _sceneLoader;

    private void Awake() {
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
      _button = GetComponent<Button>();
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    private void AddListeners() {
      _button.onClick.AddListener(LoadLocation);
    }

    private void RemoveListeners() {
      _button.onClick.RemoveListener(LoadLocation);
    }

    private void LoadLocation() {
      _sceneLoader.Load(_sceneName);
    }
  }
}