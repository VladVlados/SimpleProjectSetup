using Architecture;
using Architecture.Constants;
using Architecture.Game;
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
      GameUI.Build();
    }

    private void OnGameInitialized() {
      SceneManager.LoadScene(Constants.SceneNames.MAIN_MENU);
    }
  }
}