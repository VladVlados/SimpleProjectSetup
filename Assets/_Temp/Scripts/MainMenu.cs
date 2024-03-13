using Architecture;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;

namespace _Temp.Scripts {
  public class MainMenu : MonoBehaviour {
    private IGameFactory _gameFactory;
    private ISceneLoader _sceneLoader;

    private void Start() {
      _gameFactory= GlobalContainer.Container.Resolve<IGameFactory>();
      _sceneLoader= GlobalContainer.Container.Resolve<ISceneLoader>();
    }

    public void CreateCapsule() {
      Debug.Log(_gameFactory);
      Debug.Log(_sceneLoader);
      var capsule = _gameFactory.Create<CapsuleTest>();
    }
  }
}