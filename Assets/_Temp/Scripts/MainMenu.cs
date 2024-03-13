using Architecture;
using Architecture.CodeBase;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;

namespace _Temp.Scripts {
  public class MainMenu : MonoBehaviour {
    private IGameFactory _gameFactory;
    private ISceneLoader _sceneLoader;
    private ISavedData _savedData;

    private void Start() {
      _gameFactory= GlobalContainer.Container.Resolve<IGameFactory>();
      _sceneLoader= GlobalContainer.Container.Resolve<ISceneLoader>();
      _savedData = GlobalContainer.Container.Resolve<ISavedData>();
      _savedData.GetSaveData().SetPurchasedClothing(ClothingDataType.Body,2 , true);
      _savedData.SaveGame();
    }
  }
}