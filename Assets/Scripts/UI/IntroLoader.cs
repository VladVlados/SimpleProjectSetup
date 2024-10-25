using Architecture.CodeBase;
using Architecture.CodeBase.Constants;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace UI {
  public class IntroLoader : MonoBehaviour {
    private ISavedData _savedData;
    private ISceneLoader _sceneLoader;

    private void Awake() {
      _sceneLoader = GlobalContainer.Container.Resolve<ISceneLoader>();
    }

    private void Start() {
      _sceneLoader.Load(Constants.SceneNames.START_LOADER);
    }

    [Inject]
    private void Construct(DiContainer container, ISavedData savedData) {
      GlobalContainer.SetContainer(container);
      _savedData = savedData;
      _sceneLoader = container.Resolve<ISceneLoader>();
    }
  }
}