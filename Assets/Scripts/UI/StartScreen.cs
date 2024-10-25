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
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _shopButton;
  
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
      _settingsButton.onClick.AddListener(OpenSettingsPanel);
      _shopButton.onClick.AddListener(OpenShopPanel);
    }

    private void RemoveListeners() {
      _playButton.onClick.RemoveListener(PlayGame);
      _settingsButton.onClick.RemoveListener(OpenSettingsPanel);
      _shopButton.onClick.RemoveListener(OpenShopPanel);
    }
  
    private void PlayGame() {
      _sceneLoader.Load(Constants.SceneNames.MAIN_MENU);
    }

    private void OpenSettingsPanel() {
      SettingsPanel settingsPanel = GameUI.Controller.GetUIElement<SettingsPanel>();
      settingsPanel.OpenSettingsPanel();
    }

    private void OpenShopPanel() {
      ShopPanel shopPanel = GameUI.Controller.GetUIElement<ShopPanel>();
      shopPanel.OpenShopPanel();
    }
  }
}