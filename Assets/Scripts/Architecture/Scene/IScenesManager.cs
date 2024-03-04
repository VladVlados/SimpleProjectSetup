using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Architecture.Scene {
  public interface IScenesManager {
    public delegate void SceneManagerHandler(SceneConfig config);

    public delegate void LoadSceneHandler(float progress);

    public interface ISceneManager {
      event LoadSceneHandler OnSceneLoadingEvent;
      event SceneManagerHandler OnSceneLoadStartedEvent;
      event SceneManagerHandler OnSceneLoadCompletedEvent;
      event Action OnSceneLoadShownEvent;
      Coroutine LoadScene(string sceneName, UnityAction<SceneConfig> sceneLoadedCallback = null);

      Coroutine InitializeCurrentScene(UnityAction<SceneConfig> sceneLoadedCallback = null);

      //IScene SceneActual { get; }
      Dictionary<string, SceneConfig> ScenesConfigMap { get; }
      bool SceneLoadCompleted { get; }
      bool SceneLoadShown { get; }
    }
  }
}