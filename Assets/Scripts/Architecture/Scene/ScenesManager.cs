using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Tools;
using Architecture.UI;
using ObjectPool.Scripts.PoolLogic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Architecture.Scene {
  public class ScenesManager : IScenesManager {
    public event IScenesManager.SceneManagerHandler OnSceneLoadCompletedEvent;
    public event Action OnSceneLoadShownEvent;
    public event IScenesManager.SceneManagerHandler OnSceneLoadStartedEvent;
    private string _actualScene;
    private string _loadingScene;
    private SceneConfig _sceneConfig;

    public ScenesManager() {
      ScenesConfigMap = new Dictionary<string, SceneConfig>();
      InitializeSceneConfigs();
    }
    
    public T GetSceneDataStorage<T>() where T : SceneDataStorage {
      return _sceneConfig.GetComponent<T>();
    }

    public Coroutine LoadScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback = null) {
      _loadingScene = sceneName;
      return LoadAndInitializeScene(sceneName, sceneLoadedCallback, true);
    }

    private void InitializeSceneConfigs() {
      SceneConfig[] allSceneConfigs = Resources.LoadAll<SceneConfig>(Constants.Constants.Paths.CONFIG);
      foreach (SceneConfig sceneConfig in allSceneConfigs) {
        ScenesConfigMap[sceneConfig.SceneName] = sceneConfig;
      }
    }

    private Coroutine LoadAndInitializeScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback, bool loadNewScene) {
      ScenesConfigMap.TryGetValue(sceneName, out SceneConfig config);

      if (config == null) {
        throw new NullReferenceException($"There is no scene ({sceneName}) in the scenes list. The name is wrong or you forget to add it to the list.");
      }

      return CoroutineHandler.StartRoutine(LoadSceneRoutine(config, sceneLoadedCallback, loadNewScene));
    }

    private IEnumerator LoadSceneRoutine(SceneConfig config, UnityAction<SceneConfig> sceneLoadedCallback, bool loadNewScene = true) {
      if (SceneLoadCompleted == false) {
        yield break;
      }

      _sceneConfig = config;
      SceneLoadCompleted = false;
      SceneLoadShown = false;
      OnSceneLoadStartedEvent?.Invoke(config);
      yield return null;

      if (loadNewScene) {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(config.SceneName);
        asyncOperation.allowSceneActivation = true;
      }

      _actualScene = _loadingScene;
      yield return null;
      BuildUI(config);
      config.CreateSceneDataStorage();
      yield return null;
      sceneLoadedCallback?.Invoke(config);
      SceneLoadCompleted = true;
      OnSceneLoadCompletedEvent?.Invoke(config);
    }

    private void BuildUI(SceneConfig config) {
      GameUI.Build(config);
    }

    private void OnSceneLoadCompleted() {
      OnSceneLoadShownEvent?.Invoke();
      SceneLoadShown = true;
    }

    public Dictionary<string, SceneConfig> ScenesConfigMap { get; }
    public bool SceneLoadCompleted { get; private set; } = true;
    public bool SceneLoadShown { get; private set; } = true;
  }
}