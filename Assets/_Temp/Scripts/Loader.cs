using Architecture;
using Architecture.CodeBase.Services.SceneLoader;
using Architecture.Constants;
using UnityEngine;
using Zenject;

namespace _Temp.Scripts {
  public class Loader : MonoBehaviour {
    private ISceneLoader _sceneLoader;

    private void Start() {
      _sceneLoader.Load(Constants.SceneNames.MAIN_MENU);
    }

    [Inject]
    private void Construct(DiContainer container) {
      GlobalContainer.SetContainer(container);
      _sceneLoader =container.Resolve<ISceneLoader>();
    }
  }
}