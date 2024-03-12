using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.CodeBase.Services.CoroutineHandler;
using Architecture.Scene;
using Architecture.UI;
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

    public void Load(string sceneName, Action onSceneLoaded = null) {
      ScenesConfigMap.TryGetValue(sceneName, out SceneConfig config);
      GameUI.Build(config);
      SceneConfig = config;
      _coroutineHandler.StartCoroutine(LoadScene(sceneName, onSceneLoaded));
    }

    private void InitializeSceneConfigs() {
      ScenesConfigMap = new Dictionary<string, SceneConfig>();
      SceneConfig[] allSceneConfigs = Resources.LoadAll<SceneConfig>(Constants.Constants.Paths.CONFIG);
      foreach (SceneConfig sceneConfig in allSceneConfigs) {
        ScenesConfigMap[sceneConfig.SceneName] = sceneConfig;
      }
    }

    private IEnumerator LoadScene(string nextScene, Action onSceneLoaded) {
      if (SceneManager.GetActiveScene().name.Equals(nextScene)) {
        onSceneLoaded?.Invoke();
        yield break;
      }

      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false) {
        yield return null;
      }

      onSceneLoaded?.Invoke();
    }

    public SceneConfig SceneConfig { get; set; }
  }
}