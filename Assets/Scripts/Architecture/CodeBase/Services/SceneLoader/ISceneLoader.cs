using System;
using System.Collections.Generic;
using Architecture.Scene;

namespace Architecture.CodeBase.Services.SceneLoader {
  public interface ISceneLoader : IService {
    Dictionary<string, SceneConfig> ScenesConfigMap { get; set; }
    public SceneConfig SceneConfig { get; set; }
    public void Load(string sceneName, Action onSceneLoaded = null);
  }
}