using Architecture.CodeBase.Services.AssetManager;
using Architecture.CodeBase.Services.Audio;
using Architecture.CodeBase.Services.CoroutineHandler;
using Architecture.CodeBase.Services.Events;
using Architecture.CodeBase.Services.Factory;
using Architecture.CodeBase.Services.GlobalData;
using Architecture.CodeBase.Services.Random;
using Architecture.CodeBase.Services.Save;
using Architecture.CodeBase.Services.SceneLoader;
using UnityEngine;
using Zenject;

namespace Architecture.CodeBase {
  public class GameInstaller : MonoInstaller<GameInstaller> {
    public override void InstallBindings() {
      InstallGlobalData();
      InstallAssets();
      InstallEventService();
      InstallRandom();
      InstallSceneLoader();
      InstallAudio();
      InstallGameFactory();
      InstallCoroutineHandler();
      InstallSavedData();
      InstallIAPManager();
    }

    private void InstallGlobalData() {
      Container.Bind<IGlobalDataService>().To<GlobalDataService>().AsSingle();
    }

    private void InstallAssets() {
      Container.Bind<IAssetPath>().To<AssetPathProvider>().AsSingle();
      Container.Bind<IAsset>().To<AssetProvider>().AsSingle();
    }

    private void InstallAudio() {
      Container.Bind<IAudioService>().To<AudioService>().AsSingle();
    }

    private void InstallRandom() {
      Container.Bind<IRandomService>().To<RandomService>().AsSingle();
    }

    private void InstallEventService() {
      var monoEventsProvider = Container.InstantiateComponent<MonoEventsProvider>(new GameObject(nameof(MonoEventsProvider)));
      Container.Bind<IMonoEventService>().FromInstance(monoEventsProvider).AsSingle();
    }

    private void InstallSceneLoader() {
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void InstallGameFactory() {
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
    }

    private void InstallSavedData() {
      Container.Bind<ISavedData>().To<SavedData>().AsSingle();
    }

    private void InstallIAPManager() {
      Container.Bind<IIAPManager>().To<IAPManager>().AsSingle();
    }

    private void InstallCoroutineHandler() {
      var coroutineHandler = new GameObject(nameof(CoroutineHandler)).AddComponent<CoroutineHandler>();
      DontDestroyOnLoad(coroutineHandler);
      Container.Bind<ICoroutineHandler>().FromInstance(coroutineHandler).AsSingle();
    }
  }
}