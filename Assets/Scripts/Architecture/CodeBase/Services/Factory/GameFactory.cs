using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Architecture.CodeBase.Services.AssetManager;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Architecture.CodeBase.Services.Factory {
  public class GameFactory : IGameFactory {
    private readonly IAsset _asset;
    private readonly DiContainer _container;
    private Dictionary<Type, FactoryPrefab> _warmedUpMap;

    public T Create<T>() where T : FactoryPrefab {
      var returnedObject = Object.Instantiate(_warmedUpMap[typeof(T)]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public async Task<T> CreateAsync<T>() {
      var returnedObject = Object.Instantiate(await _asset.Load<T>()).GetComponent<T>();
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public Task<IList<T>> LoadAssetGroup<T>(string label) where T : class {
      throw new NotImplementedException();
    }

    public Task<T> CreateAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public Task<T> CreateAsync<T>(RuleTile.TilingRuleOutput.Transform parent) where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public Task<T> CreateAsync<T>(string assetPath) where T : class {
      throw new NotImplementedException();
    }

    public Task<T> CreateAddressableObjectAsync<T>(string assetPath, Vector3 at, Quaternion rotation) where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public Task<T> CreateProgressRegisteredAsync<T>() where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public Task<T> CreateProgressRegisteredAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public void WarmUp() {
      throw new NotImplementedException();
    }

    public void Cleanup() {
      throw new NotImplementedException();
    }
  }
}