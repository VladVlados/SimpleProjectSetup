using Architecture.CodeBase;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.Purchase;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;

namespace _Temp.Scripts {
  public class MainMenu : MonoBehaviour {
    private IGameFactory _gameFactory;
    private IIAPManager _iapManager;
    private ISavedData _savedData;
    private ISceneLoader _sceneLoader;

    private void Start() {
      _gameFactory = GlobalContainer.Container.Resolve<IGameFactory>();
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
      _savedData = GlobalContainer.Container.Resolve<ISavedData>();
      _iapManager = GlobalContainer.Container.Resolve<IIAPManager>();
    }
  }
}