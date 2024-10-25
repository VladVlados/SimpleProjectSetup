using Architecture.CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class SettingsPanel : UIScreen {
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
      AddListeners();
    }

    private void OnDestroy() {
      RemoveListeners();
    }

    public void OpenSettingsPanel() {
      gameObject.SetActive(true);
    }

    private void AddListeners() {
      _closeButton.onClick.AddListener(CloseSettingsPanel);
    }

    private void RemoveListeners() {
      _closeButton.onClick.RemoveListener(CloseSettingsPanel);
    }

    private void CloseSettingsPanel() {
      gameObject.SetActive(false);
    }
  }
}