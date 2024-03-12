using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace _Temp.Scripts {
  public class Test : MonoBehaviour {
    private ISceneLoader _sceneLoader;

    private void Start() {
      _sceneLoader.Load("MainMenu");
    }

    [Inject]
    private void Construct(ISceneLoader sceneLoader) {
      _sceneLoader = sceneLoader;
    }
  }
}