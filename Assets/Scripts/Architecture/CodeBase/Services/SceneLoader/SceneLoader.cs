using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.CodeBase.Services.CoroutineHandler;
using Architecture.CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.CodeBase.Services.SceneLoader {
  public class SceneLoader : ISceneLoader {
    private readonly ICoroutineHandler _coroutineHandler;

    public SceneLoader(ICoroutineHandler coroutineHandler) {
      _coroutineHandler = coroutineHandler;
      InitializeSceneConfigs();
    }

    public Dictionary<string, SceneConfig> ScenesConfigMap { get; set; }

    public void Load(string sceneName, Action onSceneLoaded = null, Action<AsyncOperation> onSceneProgressLoad = null) {
      _coroutineHandler.StartCoroutine(LoadScene(sceneName, onSceneLoaded, onSceneProgressLoad));
    }

    public SceneConfig SceneConfig { get; set; }

    public void Load(string sceneName, Action onSceneLoaded = null, Action<System.ComponentModel.AsyncOperation> onSceneProgressLoad = null) {
      throw new NotImplementedException();
    }

    private void InitializeSceneConfigs() {
      ScenesConfigMap = new Dictionary<string, SceneConfig>();
      SceneConfig[] allSceneConfigs = Resources.LoadAll<SceneConfig>(Constants.Constants.Paths.CONFIG);
      foreach (SceneConfig sceneConfig in allSceneConfigs) {
        ScenesConfigMap[sceneConfig.SceneName] = sceneConfig;
      }
    }

    private IEnumerator LoadScene(string nextScene, Action onSceneLoaded, Action<AsyncOperation> onSceneProgressLoad) {
      if (SceneManager.GetActiveScene().name.Equals(nextScene)) {
        onSceneLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false) {
        onSceneProgressLoad?.Invoke(waitNextScene);
        yield return null;
      }

      ScenesConfigMap.TryGetValue(nextScene, out SceneConfig config);
      SceneConfig = config;
      GameUI.Build(config);
      onSceneLoaded?.Invoke();
    }
  }
}