using Architecture;
using Architecture.CodeBase;
using Architecture.CodeBase.Constants;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace _Temp.Scripts {
  public class Loader : MonoBehaviour {
    private ISceneLoader _sceneLoader;
    private ISavedData _savedData;

    private void Start() {
      _sceneLoader.Load(Constants.SceneNames.MAIN_MENU);
    }

    [Inject]
    private void Construct(DiContainer container, ISavedData savedData) {
      GlobalContainer.SetContainer(container);
      _sceneLoader = container.Resolve<ISceneLoader>();
      _savedData = container.Resolve<ISavedData>();
    }
  }
}