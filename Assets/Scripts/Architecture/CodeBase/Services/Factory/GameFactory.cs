using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture.CodeBase.Services.AssetManager;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Architecture.CodeBase.Services.Factory {
  public class GameFactory : IGameFactory {
    private readonly IAsset _asset;
    private readonly DiContainer _container;
    private Dictionary<Type, FactoryPrefab> _prefabMap;
    
    public GameFactory(DiContainer container, IAsset asset) {
      _asset = asset;
      _container = container;
      Init();
    }

    public T Create<T>() where T : FactoryPrefab {
      var returnedObject = Object.Instantiate(_prefabMap[typeof(T)]) as T;
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public async Task<T> CreateAsync<T>() {
      var returnedObject = Object.Instantiate(await _asset.Load<T>()).GetComponent<T>();
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public async Task<T> CreateAsync<T>(Vector3 at, Quaternion rotation) where T : FactoryPrefab {
      var returnedObject = Object.Instantiate(await _asset.Load<T>(), at, rotation).GetComponent<T>();
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public Task<T> CreateAsync<T>(RuleTile.TilingRuleOutput.Transform parent) where T : FactoryPrefab {
      throw new NotImplementedException();
    }

    public async Task<T> CreateAsync<T>(Transform parent) where T : FactoryPrefab {
      var returnedObject = Object.Instantiate(await _asset.Load<T>(), parent).GetComponent<T>();
      _container.Inject(returnedObject);
      return returnedObject;
    }

    public void Init() {
      _prefabMap = Resources.LoadAll<FactoryPrefab>("Poolable").ToDictionary(x => x.GetType(), x => x);
    }

    public void Cleanup() {
      _asset.Dispose();
    }
  }
}