using System;
using System.Collections;
using System.Collections.Generic;
using Architecture.Tools;
using Architecture.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Architecture.Scene {
  public class ScenesManager : IScenesManager {
    private string _actualScene;
    private string _loadingScene;
    public event IScenesManager.SceneManagerHandler OnSceneLoadCompletedEvent;
    public event Action OnSceneLoadShownEvent;
    public ScenesManager() {
      ScenesConfigMap = new Dictionary<string, SceneConfig>();
      InitializeSceneConfigs();
    }

    private void InitializeSceneConfigs() {
      SceneConfig[] allSceneConfigs = Resources.LoadAll<SceneConfig>(Constants.Constants.ResourcesPath.CONFIG);
      foreach (SceneConfig sceneConfig in allSceneConfigs) {
        ScenesConfigMap[sceneConfig.SceneName] = sceneConfig;
      }
    }
    
    public Coroutine LoadScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback = null) {
      _loadingScene = sceneName;
      return LoadAndInitializeScene(sceneName, sceneLoadedCallback, true);
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
      
      SceneLoadCompleted = false;
      SceneLoadShown = false;
      OnSceneLoadStartedEvent?.Invoke(config);
      yield return null;

      if (loadNewScene) {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(config.SceneName);
        asyncOperation.allowSceneActivation = true;
      }
      
      _actualScene = _loadingScene;
      yield return null;
      BuildUI(config);
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
    public event IScenesManager.SceneManagerHandler OnSceneLoadStartedEvent;

    public Dictionary<string, SceneConfig> ScenesConfigMap { get; }
    public bool SceneLoadCompleted { get; private set; } = true;
    public bool SceneLoadShown { get; private set; } = true;
  }
}