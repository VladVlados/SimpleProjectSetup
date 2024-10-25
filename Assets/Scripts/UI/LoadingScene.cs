using System.Collections;
using Architecture.CodeBase;
using Architecture.CodeBase.Constants;
using Architecture.CodeBase.Services.CoroutineHandler;
using Architecture.CodeBase.Services.SceneLoader;
using Architecture.CodeBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
  public class LoadingScene : UIScreen {
    [SerializeField]
    private Image _loadingBarFill;

    private ISceneLoader _sceneLoader;
    private ICoroutineHandler _coroutineHandler;
    private void Awake() {
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
      _coroutineHandler = GlobalContainer.Container.Resolve<ICoroutineHandler>();
    }

    private void Start() {
      _coroutineHandler.StartCoroutine(StartLoadMainMenu());
    }

    private IEnumerator StartLoadMainMenu() {
      yield return new WaitForSeconds(1f);
      _sceneLoader.Load(Constants.SceneNames.START_SCREEN, SceneLoaded, SceneProgressLoad);
    }

    private void SceneLoaded() { }

    private void SceneProgressLoad(AsyncOperation operation) {
      float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
      _loadingBarFill.fillAmount = progressValue;
    }
  }
}