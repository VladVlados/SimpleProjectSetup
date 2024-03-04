using Architecture;
using Architecture.Constants;
using Architecture.GameCore;
using Architecture.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loading {
  public class StartLoader : MonoBehaviour {
    private void Awake() {
      Game.OnGameInitializedEvent += OnGameInitialized;
    }

    private void Start() {
      InitializeMenu();
    }

    private void OnDestroy() {
      Game.OnGameInitializedEvent -= OnGameInitialized;
    }

    private void InitializeMenu() {
      Game.Run();
    }

    private void OnGameInitialized() {
      Game.ScenesManager.LoadScene(Constants.SceneNames.MAIN_MENU);
    }
  }
}